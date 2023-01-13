using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    public float i=0.01f;//颜色渐变速度
    public Color color_ = new Color(0, 1, 1); // 起始颜色
    void Update(){    
        
        if (Open)
        {
            
            Open = false;
            FideColor(transform.GetComponent<Renderer>(), .5f, .3f);
        }
        else if(Close)
        {
            Close = false;
            FideColor(transform.GetComponent<Renderer>(), 0f, .3f);
            Tool.Timer.Register(4f, () =>
            {
                isColorChange = true;
            });
        }
        else if(isColorChange)
        {
            //RGB颜色渐变效果
            //Color(0,1,1) 起始颜色
            //-G +R -B +G -R +B   //R G B 各个元素变换过程
            
                if (color_.r == 0 && color_.g > 0 && color_.b == 1)
                {
                    color_.g -= i;
                    if (color_.g < 0) {
                        color_.g = 0;
                    }
                }
                else if (color_.g == 0 && color_.r < 1 && color_.b == 1)
                {
                    color_.r += i;
                    if (color_.r > 1)
                    {
                        color_.r = 1;
                    }
                }
                else if (color_.r == 1 && color_.g == 0 && color_.b > 0)
                {
                    color_.b -= i;
                    if (color_.b < 0)
                    {
                        color_.b = 0;
                    }
                }
                else if (color_.b == 0 && color_.r == 1 && color_.g < 1)
                {
                    color_.g += i;
                    if (color_.g > 1)
                    {
                        color_.g = 1;
                    }
                }
                else if (color_.r > 0 && color_.g == 1 && color_.b == 0)
                {
                    color_.r -= i;
                    if (color_.r < 0)
                    {
                        color_.r = 0;
                    }
                }
                else if (color_.r == 0 && color_.g == 1 && color_.b < 1)
                {
                    color_.b += i;
                    if (color_.b > 1)
                    {
                        color_.b = 1;
                    }
                }
                
                color_.a = .5f;     
                transform.GetComponent<Renderer>().material.color = color_;//获取物体Material中的颜色并赋值 
        }
    }
    public void FideColor(Renderer renderer,float targetAphla,float duration)
    {
        Color startColor = renderer.sharedMaterial.GetColor("_BaseColor");
        
        Color targetColor = new Color(startColor.r,startColor.g,startColor.b,targetAphla);
        DOTween.To((value =>
        {
            targetColor.a = value;
            renderer.sharedMaterial.SetColor("_BaseColor",targetColor);
            
        }), startColor.a, targetAphla, duration).OnComplete((() =>
        {
            //callBack?.Invoke();
        }));
    }
    public bool Open;
    public bool Close;
    public bool isColorChange;
}
