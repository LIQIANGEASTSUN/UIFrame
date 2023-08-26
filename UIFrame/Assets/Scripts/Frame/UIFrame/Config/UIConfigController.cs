using System.Collections.Generic;

namespace UIFrame
{
    public class UIConfigController
    {
        private Dictionary<UIPlaneType, UIConfig> _configDic;
        public UIConfigController()
        {
            _configDic = new Dictionary<UIPlaneType, UIConfig>();
            UIPlaneRegister.Register(this);
        }

        public UIConfig GetConfig(UIPlaneType type)
        {
            return _configDic[type];
        }

        /// <summary>
        /// 注册界面
        /// </summary>
        /// <param name="type">界面枚举</param>
        /// <param name="basePlane">界面实例</param>
        /// <param name="assetName">界面预制体资源名</param>
        /// <param name="layerName">界面加载后放在哪个layer</param>
        public void AddPlaneInfo(UIPlaneType type, UIBasePlane basePlane, string assetName, string layerName)
        {
            UIConfig uiConfig = new UIConfig(type, basePlane, assetName, layerName);
            _configDic.Add(type, uiConfig);
        }
    }
}
