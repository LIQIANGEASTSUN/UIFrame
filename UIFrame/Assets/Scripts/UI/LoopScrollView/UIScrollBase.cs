using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIScrollBase
{
    protected LoopScrollView _loopScrollView;

    public UIScrollBase(LoopScrollView loopScrollView)
    {
        _loopScrollView = loopScrollView;
    }

    public virtual void ItemCount(int count)
    {
        int min = 0;
        int max = 0;
        PageMinMax(ref min, ref max);
        Create( min, max);
    }

    protected void Create(int min, int max)
    {
        int count = 10;
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

        Text text = item.Find("Text").GetComponent<Text>();
        text.text = index.ToString();

        SetActive(item.gameObject, true);
        return item;
    }

    protected void SetActive(GameObject go, bool value)
    {
        if (go.activeInHierarchy != value)
        {
            go.SetActive(value);
        }
    }

    public abstract void ScrollChange(Vector2 pos);

    public abstract void GoToIndex(int index);

    protected abstract void PageMinMax(ref int min, ref int max);

    private void InsertIndex(List<int> list, int value)
    {
        int left = 0;
        int right = list.Count - 1;
        int index = 0;
        while (left < right)
        {
            int mid = (left + right) / 2;
            if (list[mid] > value)
            {
                right = mid - 1;
            }
            else if (list[mid] == value)
            {
                index = mid;
                break;
            }
            else
            {
                left = mid + 1;
                index = left;
            }
        }


    }

}
