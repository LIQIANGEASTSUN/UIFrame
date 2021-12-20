using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 主界面
/// </summary>
public class UIMainView : UIBasePlane
{
    private Button _closeBtn;
    private Button _shopBtn;
    public override void Open(IUIDataBase data)
    {
        base.Open(data);

        _closeBtn = _tr.Find("CloseBtn").GetComponent<Button>();
        _closeBtn.onClick.AddListener(CloseOnClick);

        _shopBtn = _tr.Find("ShopBtn").GetComponent<Button>();
        _shopBtn.onClick.AddListener(ShopOnClick);
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
        UIManager.GetInstance().Close(UIPlaneType.Main);
    }

    private void ShopOnClick()
    {
        UIManager.GetInstance().Open(UIPlaneType.Shop, null);
    }
}
