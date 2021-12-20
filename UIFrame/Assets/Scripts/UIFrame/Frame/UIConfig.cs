using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIConfig
{
    private UIPlaneType _type;
    private UIBasePlane _basePlane;
    private string _assetName;
    private string _layer;
    private HashSet<UIPlaneType> _mutualHash;
    public UIConfig(UIPlaneType type, UIBasePlane basePlane, string assetName, string layer)
    {
        _type = type;
        _basePlane = basePlane;
        _assetName = assetName;
        _layer = layer;
    }

    public void SetMutualHash(HashSet<UIPlaneType> hash)
    {
        _mutualHash = hash;
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

    public HashSet<UIPlaneType> MutualHash
    {
        get { return _mutualHash; }
    }
}

public enum UIPlaneType
{
    Main,        // 主界面
    Shop,        // 商店界面
    Backpacker,  // 背包界面
}