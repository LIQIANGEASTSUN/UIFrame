
namespace UIFrame
{
    public class UIPlaneRegister
    {
        // 注册面板信息
        public static void Register(UIConfigController controller)
        {
            controller.AddPlaneInfo(UIPlaneType.Main, new UIMainPlane(), "UIMainView", "MainLayer");
            controller.AddPlaneInfo(UIPlaneType.Shop, new UIShopPlane(), "UIShopView", "MainLayer");
            controller.AddPlaneInfo(UIPlaneType.Backpacker, new UIBackpackerPlane(), "UIBackpackerView", "MainLayer");
        }
    }
}
