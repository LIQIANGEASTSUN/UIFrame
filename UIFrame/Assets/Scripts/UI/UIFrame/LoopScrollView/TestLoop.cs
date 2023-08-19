using UnityEngine;
using UnityEngine.UI;

public class TestLoop : MonoBehaviour
{
    private LoopScrollView _loopScrollView;
    public int goToIndex = 0;

    void Start()
    {
        // 获取脚本
        LoopScrollView _loopScrollView = GetComponent<LoopScrollView>();
        // 初始化设置子项刷新回调方法
        _loopScrollView.Init(RefreshItem);
        // 设置子项个数
        _loopScrollView.ItemCount(130);
        // 自动跳转到第多少个子项
        _loopScrollView.GoToIndex(goToIndex);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _loopScrollView.GoToIndex(goToIndex);
        }
    }

    private void RefreshItem(Transform itemTr, int index)
    {
        Text text = itemTr.Find("Text").GetComponent<Text>();
        text.text = index.ToString();

        CellShowType cellShowType = _loopScrollView.GetCellShowType(50);
        Debug.LogError(index + "    " + cellShowType);
    }
}
