using UnityEngine;
using UnityEngine.UI;
using UIFrame;

/// <summary>
/// 背包界面：View
/// </summary>
public class UIBackpackerView : IUIView
{
    private Transform _tr;
    private IUIController _uiController;
    private UIBackpackerPlane UIBackpackerPlane;

    private Button _closeBtn;
    private Button _backspaceBtn;

    public void Open(Transform tr, IUIController controller)
    {
        _tr = tr;
        _uiController = controller;
        UIBackpackerPlane = controller as UIBackpackerPlane;

        _closeBtn = _tr.Find("CloseBtn").GetComponent<Button>();
        _closeBtn.onClick.RemoveAllListeners();
        _closeBtn.onClick.AddListener(UIBackpackerPlane.CloseOnClick);

        _backspaceBtn = _tr.Find("BackBtn").GetComponent<Button>();
        _backspaceBtn.onClick.RemoveAllListeners();
        _backspaceBtn.onClick.AddListener(UIBackpackerPlane.BackOnClick);
    }
}
