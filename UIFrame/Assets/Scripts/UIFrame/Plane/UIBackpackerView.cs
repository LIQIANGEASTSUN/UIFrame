using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 背包界面
/// </summary>
public class UIBackpackerView : UIBasePlane
{
    private Button _closeBtn;
    private Button _backspaceBtn;
    public override void Open(IUIDataBase data)
    {
        base.Open(data);

        _closeBtn = _tr.Find("CloseBtn").GetComponent<Button>();
        _closeBtn.onClick.AddListener(CloseOnClick);

        _backspaceBtn = _tr.Find("BackspaceBtn").GetComponent<Button>();
        _backspaceBtn.onClick.AddListener(BackspaceOnClick);
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
        UIManager.GetInstance().Close(UIPlaneType.Backpacker);
    }

    // 返回按钮
    private void BackspaceOnClick()
    {
        UIManager.GetInstance().Close(UIPlaneType.Backpacker);
        UIManager.GetInstance().BackspacePlane();
    }
}
