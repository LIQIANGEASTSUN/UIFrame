
namespace UIFrame
{
    public class UIPlaneInfo
    {
        private UIPlaneType _type;
        private UIBasePlane _plane;
        private int _recycleTime;
        private bool _isRecycle;

        public UIPlaneInfo(UIPlaneType type, UIBasePlane plane)
        {
            _type = type;
            _plane = plane;
        }

        public UIPlaneType Type
        {
            get { return _type; }
        }

        public UIBasePlane Plane
        {
            get { return _plane; }
        }

        public bool IsRecycle
        {
            get { return _isRecycle; }
            set { _isRecycle = true; }
        }

        public int RecycleTime
        {
            get { return _recycleTime; }
            set { _recycleTime = value; }
        }
    }
}