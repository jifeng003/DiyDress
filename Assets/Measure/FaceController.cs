using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceController : MonoBehaviour
{
    float blendValue = 0;                        //眼皮或睫毛改变后的值
    float correctblendValue = 0;                 //合适的刻度值
    float longblendValue = 0;                    //不合适的刻度值
    float currentVelocity = 0;                   //当前速度
    float currentVelocity01 = 0;                   //当前速度
    float targetValue;                           //要改变为的目标值
    float blinkTime;                             //随机隔多少秒眨眼
    float doTime = 0;                            //计时
    bool isblink = false;                        //是否眨眼

    public SkinnedMeshRenderer headmeshRenderer;
    public SkinnedMeshRenderer jiemaomeshRenderer;
    public ruler rulerScript;

    private void Start()
    {
        targetValue = 99.9f;
    }
    private void Update()
    {
        if(rulerScript.isChangeEstage)
        {
            correctblendValue = 0;
            longblendValue = 0;
        }
        //Blink();
        switch (rulerScript.estage)
        {
            case ruler.Estage.xiong:
                ChangeFace(0.6f, 0.7f, 0.9f);
                break;
            case ruler.Estage.yao:
                ChangeFace(0.55f, 0.65f, 0.75f);
                break;
            case ruler.Estage.tun:
                ChangeFace(0.65f, 0.75f, 0.95f);
                break;
            default:
                break;

        }

        //headmeshRenderer.SetBlendShapeWeight(2, 100);
    }

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
            headmeshRenderer.SetBlendShapeWeight(0, blendValue);
            jiemaomeshRenderer.SetBlendShapeWeight(0, blendValue);
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

    public void ChangeFace(float num01,float num02,float num03)
    {
        if (rulerScript.rulerLength > num01 && rulerScript.rulerLength < num02)
        {
            correctblendValue = Mathf.SmoothDamp(correctblendValue, targetValue, ref currentVelocity, 0.5f);
            headmeshRenderer.SetBlendShapeWeight(1, correctblendValue);
            headmeshRenderer.SetBlendShapeWeight(2, 0);
        }
        if (rulerScript.rulerLength > num03)
        {
            longblendValue = Mathf.SmoothDamp(longblendValue, targetValue, ref currentVelocity01, 0.5f);
            headmeshRenderer.SetBlendShapeWeight(1, 0);
            headmeshRenderer.SetBlendShapeWeight(2, longblendValue);
        }
        if (rulerScript.rulerLength == 0.5)
        {
            headmeshRenderer.SetBlendShapeWeight(1, 0);
            headmeshRenderer.SetBlendShapeWeight(2, 0);
        }
    }

}
