using UnityEngine;

/// <summary>
/// 竖直滚动
/// </summary>
public class UIScrollCol : UIScrollBase
{

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
        int totalRow = Mathf.CeilToInt(count * 1.0f / _loopScrollView._fixedCount);

        float width = _loopScrollView.ScrollTR.sizeDelta.x;
        float height = _loopScrollView._cellSize.y * totalRow + _loopScrollView._spacing.y * (totalRow - 1);

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

    public override void ScrollChange(Vector2 pos)
    {
        float startY = _loopScrollView.Rect.anchoredPosition.y;
        float endY = startY + _loopScrollView.ScrollTR.sizeDelta.y;

        float startRow = OffsetYToRow(startY);
        float endRow = OffsetYToRow(endY);

        Debug.LogError(startRow + "    " + endRow);
    }

    private float OffsetYToRow(float y)
    {
        float row = (y ) / (_loopScrollView._cellSize.y + _loopScrollView._spacing.y);
        return row;
    }

}
