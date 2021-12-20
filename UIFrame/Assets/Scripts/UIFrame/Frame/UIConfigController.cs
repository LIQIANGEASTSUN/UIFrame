using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIConfigController
{
    private Dictionary<UIPlaneType, UIConfig> _configDic;
    public UIConfigController()
    {
        _configDic = new Dictionary<UIPlaneType, UIConfig>();
        Register();
    }

    private void Add(UIPlaneType type, UIBasePlane basePlane, string assetName)
    {
        UIConfig uiConfig = new UIConfig(type, basePlane, assetName, "MainLayer");
        _configDic.Add(type, uiConfig);
    }

    public UIConfig GetConfig(UIPlaneType type)
    {
        return _configDic[type];
    }

    private void Register()
    {
        Add(UIPlaneType.Main, new UIMainView(), "UIMainView");
        Add(UIPlaneType.Shop, new UIShopView(), "UIShopView");
        Add(UIPlaneType.Backpacker, new UIBackpackerView(), "UIBackpackerView");
    }
}
