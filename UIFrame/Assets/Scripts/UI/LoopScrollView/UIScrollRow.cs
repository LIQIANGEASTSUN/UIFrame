﻿using UnityEngine;

/*
offsetMin ： 对应Left、Bottom
offsetMax ： 对应Right、Top
*/

/// <summary>
/// 垂直滚动
/// </summary>
public class UIScrollRow : UIScrollBase
{
    /// <summary>
    /// 总列数
    /// </summary>
    private int _totalCol;
    /// <summary>
    /// 显示区域能显示几列
    /// </summary>
    private float _pageCol;
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
        PageCol();

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

    private void PageCol()
    {
        _pageCol = _loopScrollView.ScrollTR.sizeDelta.x / (_loopScrollView._cellSize.x + _loopScrollView._spacing.x);
    }

    public override void ScrollChange(Vector2 pos)
    {
        float startX = _loopScrollView.Rect.anchoredPosition.x;
        int min = 0;
        int max = 0;
        PageMinMax(startX, ref min, ref max);
        Create(min, max);
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

        float value = col * 1.0f / (_totalCol - _pageCol);
        value = Mathf.Clamp01(value);
        Vector2 position = new Vector2(value, 0);
        _loopScrollView.SetScrollRectPos(position);
    }

    protected void PageMinMax(float x, ref int min, ref int max)
    {
        float endX = x - _loopScrollView.ScrollTR.sizeDelta.x;
        float startCol = OffsetXToCol(x);
        float endCol = OffsetXToCol(endX);

        startCol = Mathf.FloorToInt(startCol);
        endCol = Mathf.CeilToInt(endCol);

        min = Mathf.FloorToInt(startCol * _loopScrollView._fixedCount);
        max = Mathf.CeilToInt(endCol * _loopScrollView._fixedCount);
    }

    protected override void PageMinMax(ref int min, ref int max)
    {
        PageMinMax(0, ref min, ref max);
    }

}
