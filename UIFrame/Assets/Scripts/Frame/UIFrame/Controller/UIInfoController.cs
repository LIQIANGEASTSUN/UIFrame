using System.Collections.Generic;
using UnityEngine;

namespace UIFrame
{
    public class UIInfoController
    {
        #region Use
        // 记录界面打开的顺序 A-B-C-D-E，关闭时从后往前依次关闭
        private Stack<UIPlaneType> _stack;
        // 每个界面只能存在一个实例，如依次打开了 A-B-C-D 想要再次打开A界面
        // 则从后往前依次关闭 D、C、B 界面，然后刷新A界面
        private List<UIPlaneInfo> _planeInfoList = new List<UIPlaneInfo>();
        #endregion

        #region UnUse
        // 关闭的界面暂时回收到这里，5分钟后还在这里，则删除
        private Dictionary<UIPlaneType, UIPlaneInfo> _unUseDic;
        #endregion

        public UIInfoController()
        {
            _stack = new Stack<UIPlaneType>();
            _planeInfoList = new List<UIPlaneInfo>();
            _unUseDic = new Dictionary<UIPlaneType, UIPlaneInfo>();
        }

        public UIPlaneInfo GetOpenPlaneInfo(UIPlaneType type)
        {
            UIPlaneInfo info = null;
            for (int i = _planeInfoList.Count - 1; i >= 0; --i)
            {
                if (_planeInfoList[i].Type == type)
                {
                    info = _planeInfoList[i];
                    break;
                }
            }
            return info;
        }

        public void AddInfo(UIPlaneInfo info)
        {
            _stack.Push(info.Type);
            _planeInfoList.Add(info);
        }

        public void RemoveInfo(UIPlaneInfo info)
        {
            if (_stack.Peek() == info.Type)
            {
                _stack.Pop();
                Debug.LogError("int 此处有问题，如果不是最后一个怎么办，就不移除了");
            }

            for (int i = _planeInfoList.Count - 1; i >= 0; i--)
            {
                if (_planeInfoList[i].Type == info.Type)
                {
                    _planeInfoList.RemoveAt(i);
                    break;
                }
            }

            _unUseDic.Add(info.Type, info);
        }

        public UIPlaneInfo GetRecyclePlaneInfo(UIPlaneType type)
        {
            UIPlaneInfo info = null;
            if (_unUseDic.TryGetValue(type, out info))
            {
                _unUseDic.Remove(type);
            }
            return info;
        }

        public UIPlaneInfo LastOpenPlaneInfo()
        {
            UIPlaneInfo info = null;
            while (_stack.Count > 0)
            {
                UIPlaneType type = _stack.Peek();
                info = GetOpenPlaneInfo(type);
                if (null != info)
                {
                    break;
                }
                _stack.Pop();
            }
            return info;
        }

        public List<UIPlaneInfo> PlaneInfoList
        {
            get
            {
                return _planeInfoList;
            }
        }

        private const int _destoryTime = 300;
        private const int _intervalTime = 10;
        private int _lastUpdateTime = 0;
        private List<UIPlaneType> _destoryList = new List<UIPlaneType>();
        public void Update()
        {
            if (Time.realtimeSinceStartup < (_lastUpdateTime + _intervalTime))
            {
                return;
            }

            foreach (var kv in _unUseDic)
            {
                UIPlaneInfo info = kv.Value;
                if (Time.realtimeSinceStartup - info.RecycleTime > _destoryTime)
                {
                    _destoryList.Add(info.Type);
                }
            }

            for (int i = _destoryList.Count - 1; i >= 0; --i)
            {
                UIPlaneType type = _destoryList[i];
                UIPlaneInfo info = _unUseDic[type];
                info.Plane.Destroy();
                GameObject.Destroy(info.Plane.Tr.gameObject);
                _unUseDic.Remove(type);
                _destoryList.RemoveAt(i);
            }
        }
    }
}
