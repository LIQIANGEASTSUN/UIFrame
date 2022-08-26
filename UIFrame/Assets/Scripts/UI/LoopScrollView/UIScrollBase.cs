using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIScrollBase
{
    protected LoopScrollView _loopScrollView;
    protected List<UIScrollItem> _itemList = new List<UIScrollItem>();
    protected Dictionary<int, UIScrollItem> _dic = new Dictionary<int, UIScrollItem>();
    protected Transform _cloneItem;
    protected int _totalCount;

    public UIScrollBase(LoopScrollView loopScrollView)
    {
        _loopScrollView = loopScrollView;
        _cloneItem = _loopScrollView.Rect.GetChild(0);
    }

    public virtual void ItemCount(int count)
    {
        _totalCount = count;
        int min = 0;
        int max = 0;
        PageMinMax(ref min, ref max);
        Create( min, max);
    }

    protected void Create(int min, int max)
    {
        min = Mathf.Max(0, min);
        max = Mathf.Min(_totalCount - 1, max);

        List<int> invalidKeyList = new List<int>();
        foreach(var kv in _dic)
        {
            int key = kv.Key;
            if (min > key || key > max)
            {
                invalidKeyList.Add(key);
            }
        }

        for (int i = min; i <= max; ++i)
        {
            UIScrollItem item = null;
            if (_dic.TryGetValue(i, out item))
            {
                continue;
            }

            if (invalidKeyList.Count > 0)
            {
                int key = invalidKeyList[invalidKeyList.Count - 1];
                invalidKeyList.RemoveAt(invalidKeyList.Count - 1);
                _dic.TryGetValue(key, out item);
                _dic.Remove(key);
            }
            else
            {
                item = new UIScrollItem();
                item._index = i;
                Transform tr = CreateItem();
                item.SetItemTr(tr);
            }

            _dic[i] = item;
            Vector2 position = CalculateItemPosition(i);
            item.SetPosition(position);
            RefreshItem(item._itemTr, i);
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

    protected Transform CreateItem()
    {
        GameObject go = GameObject.Instantiate(_cloneItem.gameObject);
        Transform item = go.transform;
        item.SetParent(_loopScrollView.Rect.transform);
        item.localScale = Vector3.one;
        item.localRotation = Quaternion.identity;
        item.localPosition = Vector3.zero;
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

    private void RefreshItem(Transform itemTr, int index)
    {
        Text text = itemTr.Find("Text").GetComponent<Text>();
        text.text = index.ToString();
    }

    private UIScrollItem InsertSearch(List<UIScrollItem> list, int value)
    {
        int left = 0;
        int right = list.Count - 1;
        int index = 0;
        while (left < right)
        {
            int mid = (left + right) / 2;
            if (list[mid]._index > value)
            {
                right = mid - 1;
            }
            else if (list[mid]._index == value)
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

        return index < list.Count ? list[index] : null;
    }

    private void InsertIndex(List<UIScrollItem> list, int value)
    {
        int left = 0;
        int right = list.Count - 1;
        int index = 0;
        while (left < right)
        {
            int mid = (left + right) / 2;
            if (list[mid]._index > value)
            {
                right = mid - 1;
            }
            else if (list[mid]._index == value)
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

        if (index >= list.Count)
        {

        }
        else
        {

        }

    }

}
