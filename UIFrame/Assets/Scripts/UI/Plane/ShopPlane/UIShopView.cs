using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 商店界面
/// </summary>
public class UIShopView : IUIView
{
    private Transform _tr;
    private IUIController _uiController;
    private UIShopPlane UIShopPlane;

    private Button _closeBtn;
    private Button _backspaceBtn;
    private Button _backpackerBtn1;
    private Button _backpackerBtn2;

    public void Init(Transform tr, IUIController controller)
    {
        _tr = tr;
        _uiController = controller;
        UIShopPlane = controller as UIShopPlane;

        _closeBtn = _tr.Find("CloseBtn").GetComponent<Button>();
        _closeBtn.onClick.RemoveAllListeners();
        _closeBtn.onClick.AddListener(UIShopPlane.CloseOnClick);

        _backspaceBtn = _tr.Find("BackBtn").GetComponent<Button>();
        _backspaceBtn.onClick.RemoveAllListeners();
        _backspaceBtn.onClick.AddListener(UIShopPlane.BackOnClick);

        _backpackerBtn1 = _tr.Find("BackpackerBtn1").GetComponent<Button>();
        _backpackerBtn1.onClick.RemoveAllListeners();
        _backpackerBtn1.onClick.AddListener(UIShopPlane.BackpackerOnClick1);

        _backpackerBtn2 = _tr.Find("BackpackerBtn2").GetComponent<Button>();
        _backpackerBtn2.onClick.RemoveAllListeners();
        _backpackerBtn2.onClick.AddListener(UIShopPlane.BackpackerOnClick2);
    }
}
