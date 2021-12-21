﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIConfigController
{
    private Dictionary<UIPlaneType, UIConfig> _configDic;
    public UIConfigController()
    {
        _configDic = new Dictionary<UIPlaneType, UIConfig>();
        RegisterPlaneInfo();
        RegisterMutual();
        RegisterHungup();
    }

    public UIConfig GetConfig(UIPlaneType type)
    {
        return _configDic[type];
    }

    private void AddPlaneInfo(UIPlaneType type, UIBasePlane basePlane, string assetName)
    {
        UIConfig uiConfig = new UIConfig(type, basePlane, assetName, "MainLayer");
        _configDic.Add(type, uiConfig);
    }

    private void AddMutual(UIPlaneType type, HashSet<UIPlaneType> hash)
    {
        UIConfig uiConfig = null;
        if (_configDic.TryGetValue(type, out uiConfig))
        {
            uiConfig.SetMutualHash(hash);
        }
    }
    
    private void AddHungup(UIPlaneType type, HashSet<UIPlaneType> hash)
    {
        UIConfig uiConfig = null;
        if (_configDic.TryGetValue(type, out uiConfig))
        {
            uiConfig.SetHungupHash(hash);
        }
    }

    // 注册面板信息
    private void RegisterPlaneInfo()
    {
        AddPlaneInfo(UIPlaneType.Main, new UIMainView(), "UIMainView");
        AddPlaneInfo(UIPlaneType.Shop, new UIShopView(), "UIShopView");
        AddPlaneInfo(UIPlaneType.Backpacker, new UIBackpackerView(), "UIBackpackerView");
    }

    // 注册互斥面板
    private void RegisterMutual()
    {
        //AddMutual(UIPlaneType.Backpacker, new HashSet<UIPlaneType>() { UIPlaneType.Shop });
    }

    private void RegisterHungup()
    {
        AddHungup(UIPlaneType.Backpacker, new HashSet<UIPlaneType>() { UIPlaneType.Shop });
    }
}
