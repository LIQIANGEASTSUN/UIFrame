using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIBasePlane
{
    protected UIPlaneType _planeType;
    protected Transform _tr;
    protected IUIDataBase _data;

    public void SetPlaneType(UIPlaneType type)
    {
        _planeType = type;
    }

    public virtual void OnEnter(IUIDataBase data)
    {
        _data = data;
    }

    public virtual void Exit()
    {
        Debug.LogError("Exit:" + _planeType);
    }

    public virtual void HangUp()
    {
        Debug.LogError("HangUp:" + _planeType);
    }

    public virtual void Resume()
    {
        Debug.LogError("Resume:" + _planeType);
    }

    public void Close()
    {
        UIManager.GetInstance().Close(_planeType);
    }

    protected void Back()
    {
        UIManager.GetInstance().Back();
    }

    public Transform Tr
    {
        get { return _tr; }
        set { _tr = value; }
    }

}
