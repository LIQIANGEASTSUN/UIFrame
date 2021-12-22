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

    public override void Init(UIPlaneType type)
    {
        base.Init(type);
        View = new UIBackpackerView();
        Model = new UIBackpackerModel();
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
