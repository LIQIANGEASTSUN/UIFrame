using UnityEngine;
using UnityEngine.UI;

public enum FixedRowColType
{
    FixedRow,
    FixedCol,
}

public class LoopScrollView : MonoBehaviour
{
    private UIScrollBase _uIScrollBase;
    private RectTransform _rect;
    public ScrollRect _scrollRect;
    private RectTransform _scrollTR;

    public float _left;
    public float _top;
    public Vector2 _cellSize = new Vector2(100, 100);
    public Vector2 _spacing = Vector2.zero;

    public FixedRowColType _fixedRowCol = FixedRowColType.FixedCol;
    public int _fixedCount = 1;

    public void Init()
    {
        _rect = GetComponent<RectTransform>();
        _scrollRect.onValueChanged.AddListener(ScrollChange);
        _scrollTR = _scrollRect.transform.GetComponent<RectTransform>();

        CreateScroll();
    }

    void Update()
    {
        
    }

    public void ItemCount(int count)
    {
        Init();
        _uIScrollBase.ItemCount(count);
    }

    private void CreateScroll()
    {
        if (_fixedRowCol == FixedRowColType.FixedRow)
        {
            _uIScrollBase = new UIScrollRow(this);
        }
        else
        {
            _uIScrollBase = new UIScrollCol(this);
        }
    }

    //private Vector2 lastPoint = Vector2.zero;
    private void ScrollChange(Vector2 pos)
    {
        //for (int i = 0; i < _itemList.Count; ++i)
        //{
        //    //Transform child = _itemList[i];
        //    //Vector2 screenPoint = PositionConvert.UIPointToScreenPoint(child.position);
        //    //Vector2 localPos = PositionConvert.ScreenPointToUILocalPoint(_rectTransform, screenPoint);
        //    //if (pos.y < lastPoint.y)  // 向上
        //    //{
        //    //    Up(child, localPos);
        //    //}
        //    //else   // 向下
        //    //{
        //    //    Down(child, localPos);
        //    //}
        //}

        //lastPoint = pos;
    }

    public RectTransform Rect {
        get { return _rect; }
    }

    public ScrollRect ScrollRect
    {
        get { return _scrollRect; }
    }

    public RectTransform ScrollTR
    {
        get { return _scrollTR; }
    }

    /*
         private Vector2 lastPoint = Vector2.zero;
    private void ScrollChange(Vector2 pos)
    {
        for (int i = 0; i < _itemList.Count; ++i)
        {
            //Transform child = _itemList[i];
            //Vector2 screenPoint = PositionConvert.UIPointToScreenPoint(child.position);
            //Vector2 localPos = PositionConvert.ScreenPointToUILocalPoint(_rectTransform, screenPoint);
            //if (pos.y < lastPoint.y)  // 向上
            //{
            //    Up(child, localPos);
            //}
            //else   // 向下
            //{
            //    Down(child, localPos);
            //}
        }

        lastPoint = pos;
    }

    private void Up(Transform child, Vector2 localPos)
    {
        float y = _rect.sizeDelta.y * 0.5f + _itemSize.y * 0.5f;
        if (localPos.y <= y)
        {
            return;
        }
        Debug.LogError("向上:" + child.name + "    " + localPos.y + "    y:" + y);
        child.SetAsLastSibling();
        int index = int.Parse(child.name);
        index += 5;
        ResetPoint(child, index);
    }

    private void Down(Transform child, Vector2 localPos)
    {
        float y = _rect.sizeDelta.y * -0.5f - _itemSize.y;
        if (localPos.y >= y)
        {
            return;
        }

        Debug.LogError("向下");
        int index = int.Parse(child.name);
        index -= 5;
        if (index < 0)
        {
            return;
        }
        child.SetAsFirstSibling();
        ResetPoint(child, index);
    }

    private void ResetPoint(Transform child, int index)
    {
        RectTransform rt = child.GetComponent<RectTransform>();
        Debug.LogError("1:" + child.name + "   " + rt.anchoredPosition);

        child.name = index.ToString();
        Vector2 point = new Vector2(50, -50 - index * 110);
        rt.anchoredPosition = point;

        Debug.LogError("2:" + child.name + "    " + rt.anchoredPosition);
        //Vector2 screenPoint = PositionConvert.UIPointToScreenPoint(child.position);
        //Vector2 localPos = PositionConvert.ScreenPointToUILocalPoint(_rectTransform, screenPoint);

        //Debug.LogError("3:" + child.name + "     " + localPos);
    }
    */

}
