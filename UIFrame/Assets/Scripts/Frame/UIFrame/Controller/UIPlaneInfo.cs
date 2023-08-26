
namespace UIFrame
{
    public class UIPlaneInfo
    {
        private UIPlaneType _type;
        private UIBasePlane _plane;
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
    }
}