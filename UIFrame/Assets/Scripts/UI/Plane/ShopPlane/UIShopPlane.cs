using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShopPlane : UIBasePlane
{
    private UIShopView _uiShopView;
    private UIShopModel _uiShopModel;

    public override void Init(UIPlaneType type)
    {
        base.Init(type);
        View = new UIShopView();
        _uiShopView = View as UIShopView;
        Model = new UIShopModel();
        _uiShopModel = Model as UIShopModel;
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
        UIManager.GetInstance().Close(UIPlaneType.Shop);
        Back();
    }

    // 打开背包界面，关闭商店界面
    public void BackpackerOnClick1()
    {
        UIManager.GetInstance().Close(UIPlaneType.Shop);
        UIManager.GetInstance().Open(UIPlaneType.Backpacker, null);
    }

    // 打开背包界面，不关闭商店界面
    public void BackpackerOnClick2()
    {
        UIManager.GetInstance().Open(UIPlaneType.Backpacker, null);
    }
}
