﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShopModel : IUIModel
{
    private IUIDataBase _data;
    public void Init(IUIDataBase data)
    {
        _data = data;
    }
}
