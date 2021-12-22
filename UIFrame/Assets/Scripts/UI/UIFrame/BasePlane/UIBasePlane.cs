﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIBasePlane : IUIController
{
    protected UIPlaneType _planeType;
    protected Transform _tr;
    protected IUIDataBase _data;
    private IUIView _view;
    private IUIModel _model;

    public void SetPlaneType(UIPlaneType type)
    {
        _planeType = type;
    }

    public abstract void Init();

    public virtual void OnEnter(IUIDataBase data)
    {
        _data = data;
        View.Init(_tr, this);
        Model.Init(data);
        Debug.LogError("OnEnter:" + _planeType);
    }

    public virtual void Exit()
    {
        Debug.LogError("Exit:" + _planeType);
    }

    public virtual void Update()
    {
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

    protected IUIView View
    {
        get { return _view; }
        set { _view = value; }
    }

    protected IUIModel Model
    {
        get { return _model; }
        set { _model = value; }
    }
}
