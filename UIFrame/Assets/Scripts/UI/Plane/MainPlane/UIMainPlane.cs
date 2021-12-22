using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainPlane : UIBasePlane
{
    private UIMainView _mainView;
    private UIMainModel _mainModel;

    protected override IUIView View
    {
        get
        {
            if (null == _view)
            {
                _view = new UIMainView();
            }
            return _view;
        }
    }

    protected override IUIModel Model
    {
        get
        {
            if (null == _model)
            {
                _model = new UIMainModel();
            }
            return _model;
        }
    }

    public override void OnEnter(IUIDataBase data)
    {
        base.OnEnter(data);

        _mainView = _view as UIMainView;
        _mainModel = _model as UIMainModel;
    }

    public void CloseOnClick()
    {
        Close();
    }

    public void ShopOnClick()
    {
        UIManager.GetInstance().Open(UIPlaneType.Shop, null);
    }

}
