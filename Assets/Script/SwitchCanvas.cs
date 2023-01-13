using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class SwitchCanvas : MonoBehaviour
{
    public CanvasScaler canvasScaler;
    public static DeviceType CurrentDevice
    {
        get
        {
            float hdw = Screen.height * 1.0f / Screen.width;
            if (hdw > 1.3f && hdw < 1.4f)//4:3
            {
                //Debug.Log("iPad");
                return DeviceType.iPad;
            }

            if (hdw > 1.7f && hdw < 1.8f)//16:9
            {
                //Debug.Log("iPhone6/7/8");
                return DeviceType.Normal;
            }

            if (hdw > 1.8f && hdw < 2.1f)//18:37
            {
                //Debug.Log("AndroidNarrow");
                return DeviceType.AndroidNarrow;
            }

            if (hdw > 2.1f && hdw < 2.2f)//6:13
            {
                return DeviceType.iPhoneX;
            }

            if (hdw > 2.2f)//>2.2
            {
                //Debug.Log("SuperNarrow");
                return DeviceType.SuperNarrow;
            }

            return DeviceType.Normal;
        }
    }
    private void Start()
    {
        canvasScaler = transform.GetComponent<CanvasScaler>();
        switch (CurrentDevice)
        {
            case DeviceType.iPad:
                canvasScaler.matchWidthOrHeight = 1;
                break;
            case DeviceType.Normal:
                canvasScaler.matchWidthOrHeight = 0;

                break;
            case DeviceType.AndroidNarrow:
                canvasScaler.matchWidthOrHeight = 0;

                break;
            case DeviceType.iPhoneX:
                canvasScaler.matchWidthOrHeight = 0;

                break;
            case DeviceType.SuperNarrow:
                canvasScaler.matchWidthOrHeight = 0;

                break;
        }
    }
}
