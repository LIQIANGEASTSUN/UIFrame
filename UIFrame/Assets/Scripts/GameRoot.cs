using UnityEngine;
using UIFrame;

public class GameRoot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // UIPlaneType.Main  界面枚举值
        // null 需要传递给界面的数据，需要是 IUIDataBase 类型
        UIManager.GetInstance().Open(UIPlaneType.Main, null);
        
    }

    // Update is called once per frame
    void Update()
    {
        UIManager.GetInstance().Update();

        if (Input.GetKeyDown(KeyCode.A))
        {
            // 打开或刷新界面
            UIManager.GetInstance().OpenOrRefresh(UIPlaneType.Main, null);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            UIManager.GetInstance().Close(UIPlaneType.Shop);
        }
    }
}
