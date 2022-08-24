using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLoop : MonoBehaviour
{

    private LoopScrollView _loopScrollView;

    // Start is called before the first frame update
    void Start()
    {
        _loopScrollView = GetComponent<LoopScrollView>();

        _loopScrollView.ItemCount(130);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
