using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIBasePlane
{
    protected Transform _tr;
    protected IUIDataBase _data;
    public virtual void Open(IUIDataBase data)
    {
        _data = data;
    }

    public virtual void Close()
    {

    }

    public virtual void HangUp()
    {

    }

    public virtual void Resume()
    {

    }

    public Transform Tr
    {
        get { return _tr; }
        set { _tr = value; }
    }

}
