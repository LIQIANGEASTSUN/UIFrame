using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 商店界面
/// </summary>
public class UIShopView : UIBasePlane
{
    private Button _closeBtn;
    private Button _backspaceBtn;
    private Button _backpackerBtn1;
    private Button _backpackerBtn2;
    public override void OnEnter(IUIDataBase data)
    {
        base.OnEnter(data);

        _closeBtn = _tr.Find("CloseBtn").GetComponent<Button>();
        _closeBtn.onClick.AddListener(CloseOnClick);

        _backspaceBtn = _tr.Find("BackBtn").GetComponent<Button>();
        _backspaceBtn.onClick.AddListener(BackOnClick);

        _backpackerBtn1 = _tr.Find("BackpackerBtn1").GetComponent<Button>();
        _backpackerBtn1.onClick.AddListener(BackpackerOnClick1);

        _backpackerBtn2 = _tr.Find("BackpackerBtn2").GetComponent<Button>();
        _backpackerBtn2.onClick.AddListener(BackpackerOnClick2);
    }

    private void CloseOnClick()
    {
        Close();
    }

    // 返回按钮
    private void BackOnClick()
    {
        UIManager.GetInstance().Close(UIPlaneType.Shop);
        Back();
    }

    // 打开背包界面，关闭商店界面
    private void BackpackerOnClick1()
    {
        UIManager.GetInstance().Close(UIPlaneType.Shop);
        UIManager.GetInstance().Open(UIPlaneType.Backpacker, null);
    }

    // 打开背包界面，不关闭商店界面
    private void BackpackerOnClick2()
    {
        UIManager.GetInstance().Open(UIPlaneType.Backpacker, null);
    }
}
