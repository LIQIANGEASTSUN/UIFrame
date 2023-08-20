using UIFrame;

public class UIMainPlane : UIBasePlane
{
    private UIMainView _mainView;
    private UIMainModel _mainModel;

    // 界面初始化，界面创建的时候在 UIBasePlane 中自动调用
    public override void Init(UIPlaneType type)
    {
        base.Init(type);
        // 初始化 View
        View = new UIMainView();
        _mainView = View as UIMainView;

        // 初始化 Model
        Model = new UIMainModel();
        _mainModel = Model as UIMainModel;
    }

    // 界面打开时，UIManager 自动调用
    public override void Open(IUIDataBase data)
    {
        // UIBasePlane.Open 中调用了 Model.Open、View.Open
        base.Open(data);
    }

    public void CloseOnClick()
    {
        CloseSelf();
    }

    public void ShopOnClick()
    {
        UIManager.GetInstance().Open(UIPlaneType.Shop, null);
    }
}
