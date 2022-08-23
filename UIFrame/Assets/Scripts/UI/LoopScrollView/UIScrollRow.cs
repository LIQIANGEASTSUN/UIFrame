using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 垂直滚动
/// </summary>
public class UIScrollRow : UIScrollBase
{
    public UIScrollRow(LoopScrollView loopScrollView) : base(loopScrollView)
    {

    }

    public override void ItemCount(int count)
    {
        base.ItemCount(count);

        int totalCol = Mathf.CeilToInt(count * 1.0f / _loopScrollView._fixedCount);

        float width = _loopScrollView._cellSize.x * totalCol + _loopScrollView._spacing.x * (totalCol - 1);
        float height = _loopScrollView.ScrollTR.sizeDelta.y;

        Vector2 sizeDelta = new Vector2(width, height);
        SetContentRect(sizeDelta);
    }

}
