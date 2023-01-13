using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Awake()
    {
        if (Data.GetCurStageLevel() != 0)
        {
            transform.gameObject.SetActive(false);
        }
    }

    public void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            gameObject.SetActive(false);
        }
    }

}
