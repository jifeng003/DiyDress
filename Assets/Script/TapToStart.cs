using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TapToStart : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) )
        {
            transform.gameObject.SetActive(false);
            Debug.Log("动画结束");
        }
    }
}
