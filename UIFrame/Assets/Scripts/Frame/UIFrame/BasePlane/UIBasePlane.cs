using UnityEngine;

namespace UIFrame
{
    public abstract class UIBasePlane : IUIController
    {
        protected UIPlaneType _planeType;
        protected Transform _tr;
        protected bool _trLoadComplete;
        protected IUIDataBase _data;
        private IUIView _view;
        private IUIModel _model;

        public virtual void Init(UIPlaneType type)
        {
            _planeType = type;
        }

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

        public virtual void Close()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void HangUp()
        {
        }

        public virtual void Resume()
        {
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

        public virtual void Destroy()
        {

        }

        public void SetTransform(Transform tr)
        {
            _tr = tr;
            _trLoadComplete = true;
        }

        public Transform Tr
        {
            get { return _tr; }
        }

        public bool LoadComplete()
        {
            return _trLoadComplete;
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
    }
}