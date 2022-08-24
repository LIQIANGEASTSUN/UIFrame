using UnityEngine;

/// <summary>
/// 竖直滚动
/// </summary>
public class UIScrollCol : UIScrollBase
{
    private int _totalRow;
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

    private int TotalRow(int count)
    {
        int totalRow = Mathf.CeilToInt(count * 1.0f / _loopScrollView._fixedCount);
        return totalRow;
    }

    public override void ScrollChange(Vector2 pos)
    {
        float startY = _loopScrollView.Rect.anchoredPosition.y;
        float endY = startY + _loopScrollView.ScrollTR.sizeDelta.y;

        float startRow = OffsetYToRow(startY);
        float endRow = OffsetYToRow(endY);

        Debug.LogError(pos + "   " + startRow + "    " + endRow);
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

        float value = 1 - row * 1.0f / _totalRow;
        Vector2 position = new Vector2(1, value);
        _loopScrollView.SetScrollRectPos(position);
    }

}
