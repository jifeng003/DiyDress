using System;
using System.IO.Pipes;
using UnityEngine;
using UnityEngine.EventSystems;
public class DecalMove : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    public Pen pen;
    public Vector2 DecalCenter;
    public Vector2 StartLine;
    public float offset;
    
    private RectTransform rectTransform;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        DecalCenter = pen.localPoint;
    }

    private void OnMouseDown()
    {
        Debug.Log("anxia");
        pen.isdrag = true;
    }

    private void OnMouseUp()
    {
        pen.isdrag = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        DecalCenter = pen.getAnPos;
        Debug.Log("开始拖拽");
        Vector2 dir = ( rectTransform.anchoredPosition -DecalCenter);
        
        offset = dir.magnitude;
        StartLine = dir;
        startAngle = 45;
        Vector3 scale = pen.penDecal.Scale;
        startScale = scale;
    }

    private float startAngle;
    private Vector3 startScale;

   
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 pos;
        Vector2 screenPoint = new Vector2();
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position,
            eventData.pressEventCamera, out pos))
        {
        }
        
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.parent.GetComponent<RectTransform>(),eventData.position,eventData.pressEventCamera,
            out Vector2 point))
        {
                Debug.Log("local"+ point + "   "+ rectTransform.anchoredPosition + " "  + DecalCenter);
        };
        
        float off = (rectTransform.anchoredPosition - DecalCenter).magnitude;//物体到鼠标的向量
        
        Vector3 scale = startScale * (off / offset);
        if (startScale.x * ((point - DecalCenter).magnitude/offset) <= 4)
        {
            rectTransform.position = pos;
            pen.Close.anchoredPosition = 2*DecalCenter - rectTransform.anchoredPosition;
        }
        scale.z = 10;
        pen.penDecal.Scale = scale;
        pen.Cube.localScale = pen.penDecal.Scale;
        pen.Close.localScale = pen.penDecal.Scale*.8f;
        pen.move.localScale = pen.penDecal.Scale*.8f;
        
        
        //旋转
        Vector3 dir = rectTransform.anchoredPosition - DecalCenter;
        pen.offset =dir;
        float angle = Vector3.SignedAngle(rectTransform.up, dir, rectTransform.forward);
        
        
        if (angle > 180)
        {
            pen.penDecal.Angle = angle + startAngle -180;
            pen.Cube.rotation = Quaternion.Euler(0,0,angle + startAngle -180);
        }
        else
        {
            pen.penDecal.Angle = angle + startAngle;
            pen.Cube.rotation = Quaternion.Euler(0,0,angle + startAngle);

        }
        
    }
 
    public void OnEndDrag(PointerEventData eventData)
    {
        
    }
}