using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonMove : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    public ButtonTest ButtonTest;
    public Vector2 niukouCenter;
    [HideInInspector]
    public RectTransform rectTransform;

    public float offset;
    
    private Vector3 startScale;
    private float startAngle;

    private Vector3 startImgoff;


    private void OnMouseDown()
    {
        Debug.Log("anxia");
        ButtonTest.isdrag = true;
        
    }
    public Vector2 StartLine;
    private void OnMouseUp()
    {
        ButtonTest.isdrag = false;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        
        niukouCenter = ButtonTest.getButtonPos;
        Debug.Log("开始拖拽");
        Vector2 dir = ( rectTransform.anchoredPosition - niukouCenter);
        StartLine = dir;
        offset = dir.magnitude;
        startImgoff = ButtonTest.offset;
        Vector3 scale = ButtonTest.transform.localScale;
        startScale = scale;
    }

   
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 pos;
        //Vector2 screenPoint = new Vector2();
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position,
            eventData.pressEventCamera, out pos);
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.parent.GetComponent<RectTransform>(),eventData.position,eventData.pressEventCamera,
            out Vector2 point))
        {
                Debug.Log("local"+ point + "   "+ rectTransform.anchoredPosition + " "  + niukouCenter + " " + startScale.x * ((point - niukouCenter).magnitude/offset));
        };
        float off = startScale.x * ((point - niukouCenter).magnitude);//物体到鼠标的向量
        Vector3 scale = startScale * (off / offset);
        if (scale.x <= 2 && scale.x>=1)
        {
            ButtonTest.transform.localScale = scale;
            ButtonTest.CloseImg.anchoredPosition = 2*niukouCenter - rectTransform.anchoredPosition;
            ButtonTest.offset = off / offset * startImgoff;
            
            scale.z = 10;
            ButtonTest.CubeImg.localScale = scale;
            ButtonTest.CloseImg.localScale = scale*.6f;
            ButtonTest.moveImg.localScale = scale*.6f;
        }
        // float off = startScale.x * ((point - niukouCenter).magnitude);//物体到鼠标的向量
        // Vector3 scale = startScale * (off / offset);
        // Debug.Log(scale);
        //ButtonTest.transform.localScale = scale;
        
        // ButtonTest.transform.localScale = scale;
        

    }
 
    public void OnEndDrag(PointerEventData eventData)
    {
        
    }

}
