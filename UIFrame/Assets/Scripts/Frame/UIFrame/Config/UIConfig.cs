using System.Collections.Generic;

namespace UIFrame
{
    public class UIConfig
    {
        private UIPlaneType _type;
        private UIBasePlane _basePlane;
        private string _assetName;
        private string _layer;
        public UIConfig(UIPlaneType type, UIBasePlane basePlane, string assetName, string layer)
        {
            _type = type;
            _basePlane = basePlane;
            _assetName = assetName;
            _layer = layer;
        }

        public UIPlaneType Type
        {
            get { return _type; }
        }

        public UIBasePlane BasePlane
        {
            get { return _basePlane; }
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
