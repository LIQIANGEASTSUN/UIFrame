﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInfoController
{
    #region Use
    // 记录界面打开的顺序 A-B-C-D-E，关闭时从后往前依次关闭
    private Stack<int> _stack;
    // 每个界面只能存在一个实例，如依次打开了 A-B-C-D 想要再次打开A界面
    // 则从后往前依次关闭 D、C、B 界面，然后刷新A界面
    private List<UIPlaneInfo> _planeInfoList = new List<UIPlaneInfo>();
    #endregion

    #region UnUse
    // 关闭的界面暂时回收到这里，5分钟后还在这里，则删除
    private Dictionary<UIPlaneType, UIPlaneInfo> _unUseDic;
    #endregion

    public UIInfoController()
    {
        _stack = new Stack<int>();
        _planeInfoList = new List<UIPlaneInfo>();
        _unUseDic = new Dictionary<UIPlaneType, UIPlaneInfo>();
    }

    public UIPlaneInfo GetOpenPlaneInfo(UIPlaneType type)
    {
        UIPlaneInfo info = null;
        for (int i = _planeInfoList.Count - 1; i >= 0; --i)
        {
            if (_planeInfoList[i].Type == type)
            {
                info = _planeInfoList[i];
                break;
            }
        }
        return info;
    }

    public UIPlaneInfo GetOpenPlaneInfo(int instanceID)
    {
        UIPlaneInfo info = null;
        for (int i = _planeInfoList.Count - 1; i >= 0; i--)
        {
            if (_planeInfoList[i].InstanceID == instanceID)
            {
                info = _planeInfoList[i];
                break;
            }
        }
        return info;
    }

    public void AddOpenInfo(UIPlaneInfo info)
    {
        _stack.Push(info.InstanceID);
        _planeInfoList.Add(info);
    }

    public void RemoveCloseInfo(UIPlaneInfo info)
    {
        if (_stack.Peek() == info.InstanceID)
        {
            _stack.Pop();
        }

        for (int i = _planeInfoList.Count - 1; i >= 0; i--)
        {
            if (_planeInfoList[i].InstanceID == info.InstanceID)
            {
                _planeInfoList.RemoveAt(i);
                break;
            }
        }

        _unUseDic.Add(info.Type, info);
    }

    public UIPlaneInfo GetRecyclePlaneInfo(UIPlaneType type)
    {
        UIPlaneInfo info = null;
        if (_unUseDic.TryGetValue(type, out info))
        {
            _unUseDic.Remove(type);
        }
        return info;
    }

    public UIPlaneInfo LastOpenPlaneInfo()
    {
        UIPlaneInfo info = null;
        while (_stack.Count > 0)
        {
            int instanceID = _stack.Peek();
            info = GetOpenPlaneInfo(instanceID);
            if (null != info)
            {
                break;
            }
            _stack.Pop();
        }
        return info;
    }
 
    public List<UIPlaneInfo> PlaneInfoList
    {
        get
        {
            return _planeInfoList;
        }
    }

    private const int _destoryTime = 300;
    private const int _intervalTime = 10;
    private int _lastUpdateTime = 0;
    private List<UIPlaneType> _destoryList = new List<UIPlaneType>(); 
    public void Update()
    {
        if (Time.realtimeSinceStartup < (_lastUpdateTime + _intervalTime))
        {
            return;
        }

        foreach(var kv in _unUseDic)
        {
            UIPlaneInfo info = kv.Value;
            if (Time.realtimeSinceStartup - info.RecycleTime > _destoryTime)
            {
                _destoryList.Add(info.Type);
            }
        }

        for (int i = _destoryList.Count - 1; i >= 0; --i)
        {
            UIPlaneType type = _destoryList[i];
            UIPlaneInfo info = _unUseDic[type];
            info.Plane.Destroy();
            GameObject.Destroy(info.Plane.Tr.gameObject);
            _unUseDic.Remove(type);
            _destoryList.RemoveAt(i);
        }
    }
}