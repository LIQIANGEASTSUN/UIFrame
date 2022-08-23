using System.Collections;
using System.Collections.Generic;
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
        base.ItemCount(count);

        int totalRow = Mathf.CeilToInt(count * 1.0f / _loopScrollView._fixedCount);

        float width = _loopScrollView.ScrollTR.sizeDelta.x;
        float height = _loopScrollView._cellSize.y * totalRow + _loopScrollView._spacing.y * (totalRow - 1);

        Vector2 sizeDelta = new Vector2(width, height);
        SetContentRect(sizeDelta);
    }



}
