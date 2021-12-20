using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    public static UIManager Instance;

    private int _instanceID = 0;
    private UIInfoController _uiInfoController;
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
        _uiInfoController = new UIInfoController();
        _uiConfigController = new UIConfigController();
        _root = GameObject.Find("Canvas").transform;
        _layerDic = new Dictionary<string, Transform>();
    }

    public void Update()
    {
        IEnumerable<UIBasePlane> ie = _uiInfoController.GetIEnumerable();
        foreach(var plane in ie)
        {
            plane.Update();
        }
    }

    public void Open(UIPlaneType type, IUIDataBase data)
    {
        Mutual(type);
        UIPlaneInfo info = _uiInfoController.GetPlaneInfo(type);
        if (null == info)
        {
            UIBasePlane plane = LoadPanel(type);
            plane.SetPlaneType(type);
            info = new UIPlaneInfo(type, InstanceID(), plane);
            _uiInfoController.AddInfo(info);
        }
        info.Plane.OnEnter(data);
    }

    public void Close(UIPlaneType type)
    {
        UIPlaneInfo info = _uiInfoController.GetPlaneInfo(type);
        if (null != info)
        {
            GameObject.Destroy(info.Plane.Tr.gameObject);
            info.Plane.Exit();
            _uiInfoController.Remove(info);
        }
    }

    // 返回上一个界面
    public void Back()
    {
        UIPlaneInfo info = _uiInfoController.LastOpenPlaneInfo();
        if (null != info)
        {
            info.Plane.Resume();
        }
    }

    // 互斥面板，打开一个面板从后往前关掉互斥面板
    private void Mutual(UIPlaneType type)
    {
        UIConfig uiConfig = _uiConfigController.GetConfig(type);
        if (null == uiConfig || null == uiConfig.MutualHash)
        {
            return;
        }

        UIPlaneInfo info = _uiInfoController.LastOpenPlaneInfo();
        while (null != info)
        {
            if (!uiConfig.MutualHash.Contains(info.Type))
            {
                break;
            }
            Close(info.Type);
            info = _uiInfoController.LastOpenPlaneInfo();
        }
    }

    // 打开一个界面，挂起最后一个打开的界面
    public void HangUp()
    {
        UIPlaneInfo info = _uiInfoController.LastOpenPlaneInfo();
        if (null != info)
        {
            info.Plane.HangUp();
        }
    }

    public bool IsOpen(UIPlaneType type)
    {
        UIPlaneInfo info = _uiInfoController.LastOpenPlaneInfo();
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
