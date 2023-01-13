using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggirl : MonoBehaviour
{
    public SkinnedMeshRenderer eyeSMR;
    public SkinnedMeshRenderer jiemaoSMR;
    public bool isDrag;                 //是否可以拖拽

    float blendValue=0;                 //眼皮或睫毛改变后的值
    float currentVelocity=0;            //当前速度
    float targetValue;                  //要改变为的目标值
    float blinkTime;                    //随机隔多少秒眨眼
    float doTime = 0;                   //计时
    bool isblink=false;                 //是否眨眼
    private void Start()
    {
        targetValue = 99.9f;
        blinkTime = Random.Range(3, 6);
    }
    void Update()
    {
        Blink();
        if(isDrag)
        {
#if UNITY_EDITOR
            if (Input.GetMouseButton(0))
            {
                //得到手指在这一帧的移动距离
                //float OffseX = Input.GetAxis("Mouse X");
                float OffseY = Input.GetAxis("Mouse Y");
                //在y轴上旋转物体
                transform.Rotate(0, -OffseY * 50f, 0);
            }
#elif UNITY_IOS || UNITY_ANDROID
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                //得到手指在这一帧的移动距离
                Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                //在y轴上旋转物体
                transform.Rotate(0, -touchDeltaPosition.x * 0.2f, 0);
            }
#endif
        }

    }

    //眨眼
    void Blink()
    {
        doTime += Time.deltaTime * 1;
        if (doTime > blinkTime)
        {
            isblink = true;
        }

        if (isblink)
        {
            blendValue = Mathf.SmoothDamp(blendValue, targetValue, ref currentVelocity, 0.15f);
            eyeSMR.SetBlendShapeWeight(0, blendValue);
            jiemaoSMR.SetBlendShapeWeight(0, blendValue);
            if (blendValue >= 99)
            {
                targetValue = 0.1f;
            }
            if (blendValue <= 0.2f)
            {
                doTime = 0;
                targetValue = 99.9f;
                blinkTime = Random.Range(3, 6);
                isblink = false;
            }
        }
    }
}
