using System.Collections.Generic;
using UnityEngine;

namespace UIFrame
{
    public class UIInfoController
    {
        #region Use
        // 记录界面打开的顺序 A-B-C-D-E，关闭时从后往前依次关闭
        private Stack<UIPlaneType> _stack = new Stack<UIPlaneType>();
        // 每个界面只能存在一个实例，如依次打开了 A-B-C-D 想要再次打开A界面
        // 则从后往前依次关闭 D、C、B 界面，然后刷新A界面
        private Dictionary<UIPlaneType, UIPlaneInfo> _openPanelDic = new Dictionary<UIPlaneType, UIPlaneInfo>();
        #endregion

        public UIInfoController()
        {
        }

        public UIPlaneInfo GetOpenPlaneInfo(UIPlaneType type)
        {
            UIPlaneInfo info = null;
            OpenPanelDic.TryGetValue(type, out info);
            return info;
        }

        public void AddInfo(UIPlaneInfo info)
        {
            _stack.Push(info.Type);
            OpenPanelDic[info.Type] = info;
        }

        public void RemoveInfo(UIPlaneInfo info)
        {
            if (_stack.Peek() == info.Type)
            {
                _stack.Pop();
            }

            if (OpenPanelDic.ContainsKey(info.Type))
            {
                OpenPanelDic.Remove(info.Type);
            }

            info.Plane.Destroy();
            GameObject.Destroy(info.Plane.Tr.gameObject);
        }

        public UIPlaneInfo LastOpenPlaneInfo()
        {
            UIPlaneInfo info = null;
            if (_stack.Count > 0)
            {
                UIPlaneType type = _stack.Peek();
                info = GetOpenPlaneInfo(type);
            }
            
            return info;
        }

        public Dictionary<UIPlaneType, UIPlaneInfo> OpenPanelDic
        {
            get
            {
                return _openPanelDic;
            }
        }

        public void Update()
        {

        }
    }
}
