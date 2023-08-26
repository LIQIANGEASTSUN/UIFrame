using UnityEngine;

namespace UIFrame
{
    /// <summary>
    /// 界面基类
    /// 界面不提供 Update 方法，需要实时刷新的，自己在界面中添加计时器
    /// </summary>
    public abstract class UIBasePlane : IUIController
    {
        protected UIPlaneType _planeType;
        protected Transform _tr;
        protected bool _trLoadComplete;
        protected IUIDataBase _data;
        private IUIView _view;
        private IUIModel _model;
        protected bool _isHungUp = false;


        public Transform Tr
        {
            get { return _tr; }
        }

        protected IUIView View
        {
            get
            {
                return _view;
            }
            set { _view = value; }
        }

        protected IUIModel Model
        {
            get { return _model; }
            set { _model = value; }
        }

        public bool IsHungUp
        {
            get { return _isHungUp; }
            private set { _isHungUp = value; }
        }

        public virtual void Init(UIPlaneType type)
        {
            _planeType = type;
        }

        /// <summary>
        /// 界面打开时触发
        /// </summary>
        /// <param name="data"></param>
        public virtual void Open(IUIDataBase data)
        {
            _data = data;
            if (!_tr.gameObject.activeInHierarchy)
            {
                _tr.gameObject.SetActive(true);
            }

            Model.Open(data);
            View.Open(_tr, this);
        }

        // 界面关闭时触发
        public virtual void Close()
        {
        }

        /// <summary>
        /// 挂起：当前打开 A 界面，打开B 界面的时候，调用 A 界面的 HangUp 挂起函数
        /// HangUp 可以执行一个界面出屏幕的动画，或者 SetActive(false)
        /// </summary>
        public virtual void HangUp()
        {
            UnityEngine.Debug.LogError("HangUp:" + _planeType);
            IsHungUp = true;
        }

        /// <summary>
        /// 恢复:当界面挂起后，恢复时
        /// </summary>
        public virtual void Resume()
        {
            IsHungUp = false;
            UnityEngine.Debug.LogError("Resume:" + _planeType);
        }

        public void CloseSelf()
        {
            // _planeType 界面枚举值 
            UIManager.GetInstance().Close(_planeType);
        }

        protected void Back()
        {
            UIManager.GetInstance().Back();
        }

        public void SetTransform(Transform tr)
        {
            _tr = tr;
            _trLoadComplete = true;
        }

        public bool LoadComplete()
        {
            return _trLoadComplete;
        }

        public virtual void Destroy()
        {

        }
    }
}