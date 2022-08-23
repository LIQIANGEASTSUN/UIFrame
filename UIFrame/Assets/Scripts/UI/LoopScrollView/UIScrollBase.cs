using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScrollBase
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
    }

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
        _loopScrollView.Rect.anchorMin = new Vector2(0, 1);
        _loopScrollView.Rect.anchorMax = Vector2.one;
        _loopScrollView.Rect.pivot = new Vector2(0, 1);
        _loopScrollView.Rect.sizeDelta = sizeDelta;
        _loopScrollView.Rect.anchoredPosition3D = new Vector3(0, 0, 0);
    }

    protected void SetActive(GameObject go, bool value)
    {
        if (go.activeInHierarchy != value)
        {
            go.SetActive(value);
        }
    }

}
