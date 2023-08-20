using UIFrame;

public class UIShopModel : IUIModel
{
    private IUIDataBase _data;
    public void Open(IUIDataBase data)
    {
        _data = data;
    }
}
