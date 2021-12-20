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
    public override void Open(IUIDataBase data)
    {
        base.Open(data);

        _closeBtn = _tr.Find("CloseBtn").GetComponent<Button>();
        _closeBtn.onClick.AddListener(CloseOnClick);

        _backspaceBtn = _tr.Find("BackspaceBtn").GetComponent<Button>();
        _backspaceBtn.onClick.AddListener(BackspaceOnClick);

        _backpackerBtn1 = _tr.Find("BackpackerBtn1").GetComponent<Button>();
        _backpackerBtn1.onClick.AddListener(BackpackerOnClick1);

        _backpackerBtn2 = _tr.Find("BackpackerBtn2").GetComponent<Button>();
        _backpackerBtn2.onClick.AddListener(BackpackerOnClick2);
    }

    public override void Close()
    {
        base.Close();
    }

    public override void HangUp()
    {
        base.HangUp();
    }

    public override void Resume()
    {
        base.Resume();
    }

    private void CloseOnClick()
    {
        UIManager.GetInstance().Close(UIPlaneType.Shop);
    }

    // 返回按钮
    private void BackspaceOnClick()
    {
        UIManager.GetInstance().Close(UIPlaneType.Shop);
        UIManager.GetInstance().BackspacePlane();
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
