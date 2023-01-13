using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceInfo : MonoBehaviour
{
    public bool isTie;
    public float n;
    void Awake()
    {
        
        float targetHight = 1334f;
        if (750 * Screen.height > 1334f * Screen.width)
        {
            targetHight = 750 * Screen.height / Screen.width;
            n = targetHight / 1334f;
        }

        if (n != 0)
        {
            if (isTie)
            {
                transform.GetComponent<RectTransform>().sizeDelta *= n;
            }
            else
            {
                Camera.main.fieldOfView *= n;
            }
        }
        
        
    }
}

