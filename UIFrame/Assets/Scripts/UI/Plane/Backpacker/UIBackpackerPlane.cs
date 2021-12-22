using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 背包界面：Controller
/// </summary>
public class UIBackpackerPlane : UIBasePlane
{
    private UIBackpackerView _backpackerView;
    private UIBackpackerModel _backpackerModel;

    protected override IUIView View
    {
        get
        {
            if (null == _view)
            {
                _view = new UIBackpackerView();
            }
            return _view;
        }
    }

    protected override IUIModel Model
    {
        get
        {
            if (null == _model)
            {
                _model = new UIBackpackerModel();
            }
            return _model;
        }
    }

    public override void OnEnter(IUIDataBase data)
    {
        base.OnEnter(data);

        _backpackerView = View as UIBackpackerView;
        _backpackerModel = Model as UIBackpackerModel;
    }

    public void CloseOnClick()
    {
        Close();
    }

    // 返回按钮
    public void BackOnClick()
    {
        Close();
        Back();
    }
}
