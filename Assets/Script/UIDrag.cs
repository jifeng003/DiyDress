using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class UIDrag : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    RectTransform canvasRect; 
    public  RectTransform imgRect;        //得到图片的ugui坐标
    Vector2 offset = new Vector3();    //用来得到鼠标和图片的差值
    public bool isDrag;
    void OnEnable(){
    
        //imgRect = this.GetComponent<RectTransform>();
    
        canvasRect = imgRect.parent as RectTransform;
    
    }

    

    /// <summary>
    
    /// 按下按钮，对应接口 IPointerDownHandler
    
    /// </summary>
    
    /// <param name="eventData">Event data.</param>
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("按下");
        isDrag = true;
    
        Vector2 mouseDown = eventData.position;    //记录鼠标按下时的屏幕坐标
    
        Vector2 mouseUguiPos = new Vector2();   //定义一个接收返回的ugui坐标
    
        //RectTransformUtility.ScreenPointToLocalPointInRectangle()：把屏幕坐标转化成ugui坐标
    
        //canvas：坐标要转换到哪一个物体上，这里img父类是Canvas，我们就用Canvas
    
        //eventData.enterEventCamera：这个事件是由哪个摄像机执行的
    
        //out mouseUguiPos：返回转换后的ugui坐标
    
        //isRect：方法返回一个bool值，判断鼠标按下的点是否在要转换的物体上
    
        bool isRect = RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, mouseDown, eventData.enterEventCamera, out mouseUguiPos);
    
        if (isRect)   //如果在
    
        {
    
            //计算图片中心和鼠标点的差值
    
            offset = imgRect.anchoredPosition - mouseUguiPos;
        }
    
    }
    
    [Header("范围")]
    public Vector2 clamp;
    
    
    /// <summary>
    
    /// 当鼠标拖动时调用   对应接口 IDragHandler
    
    /// </summary>
    
    /// <param name="eventData">Event data.</param>
    
    public void OnDrag(PointerEventData eventData)
    
    {
        if (offset == Vector2.zero)
        {
            OnBeginDrag(eventData);
        }
        //Debug.Log("拖动");
    
        if (eventData.button != PointerEventData.InputButton.Left)
    
            return;
        Vector2 mouseDrag = eventData.position;   //当鼠标拖动时的屏幕坐标
    
        Vector2 uguiPos = new Vector2();   //用来接收转换后的拖动坐标
    
        //和上面类似
    
        bool isRect = RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, mouseDrag, eventData.enterEventCamera, out uguiPos);
    
        if (isRect)
    
        {            //设置图片的ugui坐标与鼠标的ugui坐标保持不变
            float PosX = (offset + uguiPos).x;
            PosX = Mathf.Clamp(PosX, clamp.x, clamp.y);
    
            imgRect.anchoredPosition = new Vector2(PosX,imgRect.anchoredPosition.y);
            
        }
    
    }
    
    
    
    /// <summary>
    
    /// 当鼠标抬起时调用  对应接口  IPointerUpHandler
    
    /// </summary>
    
    /// <param name="eventData">Event data.</param>
    
    public void OnEndDrag(PointerEventData eventData)
    
    {
    
        offset = Vector2.zero;

        isDrag = false;
    }


}
