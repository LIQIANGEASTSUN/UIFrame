using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    public static UIManager Instance;

    private int _instanceID = 0;
    private UIInfoDataController _uiInfoDataController;
    private UIConfigController _uiConfigController;

    private Transform _root;
    private Dictionary<string, Transform> _layerDic;

    public static UIManager GetInstance()
    {
        if (null == Instance)
        {
            Instance = new UIManager();
        }
        return Instance;
    }

    private UIManager()
    {
        _uiInfoDataController = new UIInfoDataController();
        _uiConfigController = new UIConfigController();
        _root = GameObject.Find("Canvas").transform;
        _layerDic = new Dictionary<string, Transform>();
    }

    public void Open(UIPlaneType type, IUIDataBase data)
    {
        UIPlaneInfo info = _uiInfoDataController.GetPlaneInfo(type);
        if (null == info)
        {
            UIBasePlane plane = LoadPanel(type);
            info = new UIPlaneInfo(type, InstanceID(), plane);
            _uiInfoDataController.AddInfo(info);
        }
        info.Plane.Open(data);
    }

    public void Close(UIPlaneType type)
    {
        UIPlaneInfo info = _uiInfoDataController.GetPlaneInfo(type);
        if (null != info)
        {
            GameObject.Destroy(info.Plane.Tr.gameObject);
            info.Plane.Close();
            _uiInfoDataController.Remove(info);
        }
    }

    // 返回上一个界面
    public void BackspacePlane()
    {
        UIPlaneInfo info = _uiInfoDataController.LastOpenPlaneInfo();
        if (null != info)
        {
            info.Plane.Resume();
        }
    }

    public bool IsOpen(UIPlaneType type)
    {
        UIPlaneInfo info = _uiInfoDataController.LastOpenPlaneInfo();
        return null != info;
    }

    private UIBasePlane LoadPanel(UIPlaneType type)
    {
        UIConfig uiConfig = _uiConfigController.GetConfig(type);
        if (null == uiConfig.BasePlane.Tr)
        {
            GameObject go = Resources.Load<GameObject>(uiConfig.AssetName);
            Transform layerTr = LayerTransform(uiConfig.Layer);
            GameObject instance = GameObject.Instantiate(go, layerTr);
            instance.transform.SetAsLastSibling();
            instance.transform.localScale = Vector3.one;
            instance.transform.rotation = Quaternion.identity;
            instance.transform.localPosition = Vector3.zero;
            uiConfig.BasePlane.Tr = instance.transform;
        }

        return uiConfig.BasePlane;
    }

    private Transform LayerTransform(string layerName)
    {
        Transform tr = null;
        if (!_layerDic.TryGetValue(layerName, out tr))
        {
            tr = _root.Find(layerName);
            _layerDic[layerName] = tr;
        }
        return tr;
    }

    private int InstanceID()
    {
        return ++_instanceID;
    }

}
