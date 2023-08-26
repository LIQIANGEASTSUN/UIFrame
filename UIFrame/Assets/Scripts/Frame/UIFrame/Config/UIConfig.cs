
namespace UIFrame
{
    public class UIConfig
    {
        private UIPlaneType _type;
        private UIBasePlane _plane;
        private string _assetName;
        private string _layer;

        public UIConfig(UIPlaneType type, UIBasePlane basePlane, string assetName, string layer)
        {
            _type = type;
            _plane = basePlane;
            _assetName = assetName;
            _layer = layer;
        }

        public UIPlaneType Type
        {
            get { return _type; }
        }

        public UIBasePlane Plane
        {
            get { return _plane; }
        }

        public string AssetName
        {
            get { return _assetName; }
        }

        public string Layer
        {
            get { return _layer; }
        }
    }
}
