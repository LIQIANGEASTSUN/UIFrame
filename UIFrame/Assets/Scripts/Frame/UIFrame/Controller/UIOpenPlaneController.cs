using System.Collections.Generic;

namespace UIFrame
{
    public class UIOpenPlaneController
    {
        // 记录界面打开的顺序 A-B-C-D-E，关闭时从后往前依次关闭
        // 每个界面只能存在一个实例，如依次打开了 A-B-C-D 想要再次打开A界面
        // 则从后往前依次关闭 D、C、B 界面，然后刷新A界面
        private Stack<UIPlaneType> _stack = new Stack<UIPlaneType>();
        private HashSet<UIPlaneType> _openHash = new HashSet<UIPlaneType>();

        public bool IsOpen(UIPlaneType planeType)
        {
            return _openHash.Contains(planeType);
        }

        public void AddInfo(UIConfig info)
        {
            _stack.Push(info.Type);
            _openHash.Add(info.Type);
        }

        public void RemoveInfo(UIConfig info)
        {
            Remove(info.Type);
            if (_openHash.Contains(info.Type))
            {
                _openHash.Remove(info.Type);
            }
        }

        private Stack<UIPlaneType> _temp = new Stack<UIPlaneType>();
        private void Remove(UIPlaneType planeType)
        {
            if (_stack.Peek() == planeType)
            {
                _stack.Pop();
                return;
            }

            _temp.Clear();
            while (_stack.Count > 0)
            {
                UIPlaneType type = _stack.Pop();
                if (type != planeType)
                {
                    _temp.Push(type);
                }
            }

            while (_temp.Count > 0)
            {
                UIPlaneType type = _temp.Pop();
                _stack.Push(type);
            }
        }

        public UIConfig LastOpenPlaneInfo()
        {
            UIConfig info = null;
            if (_stack.Count > 0)
            {
                UIPlaneType type = _stack.Peek();
                info = UIManager.GetInstance().UIConfigController.GetConfig(type);
            }
            
            return info;
        }

        public HashSet<UIPlaneType> OpenHash
        {
            get
            {
                return _openHash;
            }
        }
    }
}