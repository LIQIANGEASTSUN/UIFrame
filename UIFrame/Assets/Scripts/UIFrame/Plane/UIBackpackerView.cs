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
    public override void OnEnter(IUIDataBase data)
    {
        base.OnEnter(data);

        _closeBtn = _tr.Find("CloseBtn").GetComponent<Button>();
        _closeBtn.onClick.AddListener(CloseOnClick);

        _backspaceBtn = _tr.Find("BackBtn").GetComponent<Button>();
        _backspaceBtn.onClick.AddListener(BackOnClick);
    }

    private void CloseOnClick()
    {
        Close();
    }

    // 返回按钮
    private void BackOnClick()
    {
        Close();
        Back();
    }
}
