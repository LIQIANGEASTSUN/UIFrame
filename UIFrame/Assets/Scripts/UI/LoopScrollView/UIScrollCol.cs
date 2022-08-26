using UnityEngine;

/// <summary>
/// 竖直滚动
/// </summary>
public class UIScrollCol : UIScrollBase
{
    /// <summary>
    /// 总行数
    /// </summary>
    private int _totalRow;
    /// <summary>
    /// 显示区域能显示几行
    /// </summary>
    private float _pageRow;
    public UIScrollCol(LoopScrollView loopScrollView) : base(loopScrollView)
    {

    }

    public override void ItemCount(int count)
    {
        CalculateContent(count);
        base.ItemCount(count);
        _loopScrollView.SetScrollRectPos(new Vector2(0, 1));
    }

    protected override void CalculateContent(int count)
    {
        _totalRow = TotalRow(count);
        PageRow();

        float width = _loopScrollView.ScrollTR.sizeDelta.x;
        float height = _loopScrollView._cellSize.y * _totalRow + _loopScrollView._spacing.y * (_totalRow - 1);

        Vector2 sizeDelta = new Vector2(width, height);
        _loopScrollView.Rect.anchorMin = new Vector2(0, 1);
        _loopScrollView.Rect.anchorMax = Vector2.one;
        _loopScrollView.Rect.pivot = new Vector2(0, 1);
        _loopScrollView.Rect.sizeDelta = sizeDelta;
        _loopScrollView.Rect.anchoredPosition3D = new Vector3(0, 0, 0);

        Vector2 offsetMax = _loopScrollView.Rect.offsetMax;
        offsetMax.x = 0;
        _loopScrollView.Rect.offsetMax = offsetMax;
    }

    protected override void IndexToRowCol(int index, ref int row, ref int col)
    {
        row = index / _loopScrollView._fixedCount;
        col = index % _loopScrollView._fixedCount;
    }

    protected void PageRow()
    {
        _pageRow = _loopScrollView.ScrollTR.sizeDelta.y / (_loopScrollView._cellSize.y + _loopScrollView._spacing.y);
    }

    private int TotalRow(int count)
    {
        int totalRow = Mathf.CeilToInt(count * 1.0f / _loopScrollView._fixedCount);
        return totalRow;
    }

    public override void ScrollChange(Vector2 pos)
    {
        float startY = _loopScrollView.Rect.anchoredPosition.y;
        int min = 0;
        int max = 0;
        PageMinMax(startY, ref min, ref max);
        Create(min, max);
    }

    private float OffsetYToRow(float y)
    {
        float row = (y ) / (_loopScrollView._cellSize.y + _loopScrollView._spacing.y);
        return row;
    }

    public override void GoToIndex(int index)
    {
        int row = 0;
        int col = 0;
        IndexToRowCol(index, ref row, ref col);

        float value = 1 - row * 1.0f / (_totalRow - _pageRow);
        value = Mathf.Clamp01(value);
        Vector2 position = new Vector2(1, value);
        _loopScrollView.SetScrollRectPos(position);
    }

    protected void PageMinMax(float y, ref int min, ref int max)
    {
        float endY = y + _loopScrollView.ScrollTR.sizeDelta.y;
        float startRow = OffsetYToRow(y);
        float endRow = OffsetYToRow(endY);

        startRow = Mathf.FloorToInt(startRow);
        endRow = Mathf.CeilToInt(endRow);

        min = Mathf.FloorToInt(startRow * _loopScrollView._fixedCount);
        max = Mathf.CeilToInt(endRow * _loopScrollView._fixedCount);
    }

    protected override void PageMinMax(ref int min, ref int max)
    {
        PageMinMax(0, ref min, ref max);
    }

}
