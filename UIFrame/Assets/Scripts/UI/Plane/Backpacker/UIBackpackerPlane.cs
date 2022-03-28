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
        _backpackerView = View as UIBackpackerView;

        Model = new UIBackpackerModel();
        _backpackerModel = Model as UIBackpackerModel;
    }

    public override void Open(IUIDataBase data)
    {
        base.Open(data);
    }

    public void CloseOnClick()
    {
        CloseSelf();
    }

    // 返回按钮
    public void BackOnClick()
    {
        CloseSelf();
        Back();
    }
}
