using UnityEngine;
using UnityEngine.UI;

public enum FixedRowColType
{
    FixedRow,
    FixedCol,
}

public class LoopScrollView : MonoBehaviour
{
    private UIScrollBase _uIScrollBase;
    private RectTransform _rect;
    public ScrollRect _scrollRect;
    private RectTransform _scrollTR;

    public float _left;
    public float _top;
    public Vector2 _cellSize = new Vector2(100, 100);
    public Vector2 _spacing = Vector2.zero;

    public FixedRowColType _fixedRowCol = FixedRowColType.FixedCol;
    public int _fixedCount = 1;

    public void Init()
    {
        _rect = GetComponent<RectTransform>();
        _scrollRect.onValueChanged.AddListener(ScrollChange);
        _scrollTR = _scrollRect.transform.GetComponent<RectTransform>();

        CreateScroll();
    }

    public void ItemCount(int count)
    {
        Init();
        _uIScrollBase.ItemCount(count);
    }

    private void CreateScroll()
    {
        if (_fixedRowCol == FixedRowColType.FixedRow)
        {
            _uIScrollBase = new UIScrollRow(this);
        }
        else
        {
            _uIScrollBase = new UIScrollCol(this);
        }
    }

    private void ScrollChange(Vector2 pos)
    {
        _uIScrollBase.ScrollChange(pos);
    }

    public RectTransform Rect {
        get { return _rect; }
    }

    public RectTransform ScrollTR
    {
        get { return _scrollTR; }
    }

    public void SetScrollRectPos(Vector2 position)
    {
        _scrollRect.normalizedPosition = position;
    }

    /// <summary>
    /// 跳转到第 index 个
    /// 横向的第 index 显示在最左侧
    /// 竖向的第 index 显示在最上方
    /// </summary>
    /// <param name="index"></param>
    public void GoToIndex(int index)
    {
        _uIScrollBase.GoToIndex(index);
    }
}
