using System.Collections.Generic;

namespace UIFrame
{
    public class UIConfigController
    {
        private Dictionary<UIPlaneType, UIConfig> _configDic;
        public UIConfigController()
        {
            _configDic = new Dictionary<UIPlaneType, UIConfig>();
            RegisterPlaneInfo();
        }

        public UIConfig GetConfig(UIPlaneType type)
        {
            return _configDic[type];
        }

        private void AddPlaneInfo(UIPlaneType type, UIBasePlane basePlane, string assetName, string layerName)
        {
            UIConfig uiConfig = new UIConfig(type, basePlane, assetName, layerName);
            _configDic.Add(type, uiConfig);
        }

        // 注册面板信息
        private void RegisterPlaneInfo()
        {
            //UIPlaneType.Main,   界面枚举
            //new UIMainPlane(),  界面C实例
            //"UIMainView",       界面预制体路径
            //"MainLayer"         界面创建出来挂在那个物体下
            AddPlaneInfo(UIPlaneType.Main, new UIMainPlane(), "UIMainView", "MainLayer");
            AddPlaneInfo(UIPlaneType.Shop, new UIShopPlane(), "UIShopView", "MainLayer");
            AddPlaneInfo(UIPlaneType.Backpacker, new UIBackpackerPlane(), "UIBackpackerView", "MainLayer");
        }
    }
}
