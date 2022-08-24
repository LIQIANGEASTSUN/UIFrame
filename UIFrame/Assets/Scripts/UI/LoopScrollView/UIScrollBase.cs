using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIScrollBase
{
    protected LoopScrollView _loopScrollView;
    protected int _count;

    public UIScrollBase(LoopScrollView loopScrollView)
    {
        _loopScrollView = loopScrollView;
    }

    public virtual void ItemCount(int count)
    {
        _count = count;

        for (int i = 0; i < count; ++i)
        {
            Transform itemTr = GetItem(i);
            Vector2 position = CalculateItemPosition(i);
            RectTransform rect = itemTr.GetComponent<RectTransform>();
            rect.sizeDelta = _loopScrollView._cellSize;
            rect.anchoredPosition = position;
        }
    }

    protected abstract void CalculateContent(int count);

    protected Vector2 CalculateItemPosition(int index)
    {
        int row = 0;
        int col = 0;
        IndexToRowCol(index, ref row, ref col);

        Vector2 position = new Vector2(_loopScrollView._left, -_loopScrollView._top);
        position.x += (col + 0.5f) * _loopScrollView._cellSize.x + col * _loopScrollView._spacing.x;
        position.y -= (row + 0.5f) * _loopScrollView._cellSize.y + row * _loopScrollView._spacing.y;
        return position;
    }
    protected abstract void IndexToRowCol(int index, ref int row, ref int col);

    protected Transform GetItem(int index)
    {
        Transform item = null;
        if (_loopScrollView.Rect.childCount > index)
        {
            item = _loopScrollView.Rect.GetChild(index);
        }
        else
        {
            Transform cloneTr = _loopScrollView.Rect.GetChild(0);
            GameObject go = GameObject.Instantiate(cloneTr.gameObject);
            item = go.transform;
            item.SetParent(_loopScrollView.Rect.transform);
            item.localScale = Vector3.one;
            item.localRotation = Quaternion.identity;
            item.localPosition = Vector3.zero;
        }

        SetActive(item.gameObject, true);
        return item;
    }

    protected void SetContentRect(Vector2 sizeDelta)
    {

    }

    protected void SetActive(GameObject go, bool value)
    {
        if (go.activeInHierarchy != value)
        {
            go.SetActive(value);
        }
    }

}
