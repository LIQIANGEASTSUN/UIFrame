using UnityEngine;

/*
offsetMin ： 对应Left、Bottom
offsetMax ： 对应Right、Top
*/

/// <summary>
/// 垂直滚动
/// </summary>
public class UIScrollRow : UIScrollBase
{
    private int _totalCol;
    public UIScrollRow(LoopScrollView loopScrollView) : base(loopScrollView)
    {

    }

    public override void ItemCount(int count)
    {
        CalculateContent(count);
        base.ItemCount(count);
        _loopScrollView.SetScrollRectPos(new Vector2(0, 0));
    }

    protected override void CalculateContent(int count)
    {
        _totalCol = TotalCol(count);

        float width = _loopScrollView._cellSize.x * _totalCol + _loopScrollView._spacing.x * (_totalCol - 1);
        float height = _loopScrollView.ScrollTR.sizeDelta.y;

        Vector2 sizeDelta = new Vector2(width, height);
        _loopScrollView.Rect.anchorMin = new Vector2(0, 0);
        _loopScrollView.Rect.anchorMax = new Vector2(0, 1);
        _loopScrollView.Rect.pivot = new Vector2(0, 1);
        _loopScrollView.Rect.sizeDelta = sizeDelta;
        _loopScrollView.Rect.anchoredPosition3D = new Vector3(0, 0, 0);

        Vector2 offsetMin = _loopScrollView.Rect.anchorMin;
        offsetMin.y = 0;
        _loopScrollView.Rect.offsetMin = offsetMin;

        Vector2 offsetMax = _loopScrollView.Rect.offsetMax;
        offsetMax.y = 0;
        _loopScrollView.Rect.offsetMax = offsetMax;
    }

    private int TotalCol(int count)
    {
        int totalCol = Mathf.CeilToInt(count * 1.0f / _loopScrollView._fixedCount);
        return totalCol;
    }

    protected override void IndexToRowCol(int index, ref int row, ref int col)
    {
        row = index % _loopScrollView._fixedCount;
        col = index / _loopScrollView._fixedCount;
    }

    public override void ScrollChange(Vector2 pos)
    {
        float startX = _loopScrollView.Rect.anchoredPosition.x;
        float endX = startX - _loopScrollView.ScrollTR.sizeDelta.x;

        float startCol = OffsetXToCol(startX);
        float endCol = OffsetXToCol(endX);

        Debug.LogError(pos + "  " + startCol + "    " + endCol);
    }

    private float OffsetXToCol(float x)
    {
        float col = (x) / (_loopScrollView._cellSize.x + _loopScrollView._spacing.x);
        col *= -1;
        return col;
    }

    public override void GoToIndex(int index)
    {
        int row = 0;
        int col = 0;
        IndexToRowCol(index, ref row, ref col);

        float value = 1 - row * 1.0f / _totalCol;
        Vector2 position = new Vector2(1, value);
        _loopScrollView.SetScrollRectPos(position);
    }

}
