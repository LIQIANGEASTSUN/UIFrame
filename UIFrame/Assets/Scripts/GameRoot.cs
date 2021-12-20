using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIManager.GetInstance().Open(UIPlaneType.Main, null);
    }

    // Update is called once per frame
    void Update()
    {
        UIManager.GetInstance().Update();
    }
}
