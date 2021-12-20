using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInfoDataController
{
    private Stack<int> _stack;
    private Dictionary<UIPlaneType, int> _planeDic;
    private Dictionary<int, UIPlaneInfo> _planeInfoDic;

    public UIInfoDataController()
    {
        _stack = new Stack<int>();
        _planeDic = new Dictionary<UIPlaneType, int>();
        _planeInfoDic = new Dictionary<int, UIPlaneInfo>();
    }

    public UIPlaneInfo GetPlaneInfo(UIPlaneType type)
    {
        UIPlaneInfo info = null;
        int instanceID = -1;
        if (   !_planeDic.TryGetValue(type, out instanceID) 
            || !_planeInfoDic.TryGetValue(instanceID, out info))
        {
            return info;
        }
        return info;
    }

    public void AddInfo(UIPlaneInfo info)
    {
        _stack.Push(info.InstanceID);
        _planeDic.Add(info.Type, info.InstanceID);
        _planeInfoDic.Add(info.InstanceID, info);
    }

    public void Remove(UIPlaneInfo info)
    {
        if (_stack.Peek() != info.InstanceID)
        {
            Debug.LogError("stack peek not info:" + info.Type + "  " + info.InstanceID);
            return;
        }
        _stack.Pop();
        _planeDic.Remove(info.Type);
        _planeInfoDic.Remove(info.InstanceID);
    }

    public UIPlaneInfo LastOpenPlaneInfo()
    {
        UIPlaneInfo info = null;
        if (_stack.Count > 0)
        {
            int instanceID = _stack.Peek();
            _planeInfoDic.TryGetValue(instanceID, out info);
            return info;
        }
        return info;
    }
}
