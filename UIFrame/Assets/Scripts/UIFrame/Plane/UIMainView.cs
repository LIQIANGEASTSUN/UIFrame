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
    public override void OnEnter(IUIDataBase data)
    {
        base.OnEnter(data);

        _closeBtn = _tr.Find("CloseBtn").GetComponent<Button>();
        _closeBtn.onClick.AddListener(CloseOnClick);

        _shopBtn = _tr.Find("ShopBtn").GetComponent<Button>();
        _shopBtn.onClick.AddListener(ShopOnClick);
    }

    private void CloseOnClick()
    {
        Close();
    }

    private void ShopOnClick()
    {
        UIManager.GetInstance().Open(UIPlaneType.Shop, null);
    }
}
