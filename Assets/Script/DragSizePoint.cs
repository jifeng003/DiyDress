using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Runtime.CompilerServices;
using System.Timers;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum Axis
{
    X,
    Y
}
[Serializable]
public class SizePoint
{
    public bool isEnable;
    public bool isFinish;
    public GameObject TargetPoint;
    public Transform WorldTransform1;
    public GameObject NowPoint;
    public Transform WorldTransform2;
    public pinAnim pinanim;
    public float OriDis;
    public float NowDis
    {
        get
        {
            Vector2 pos1 = TargetPoint.transform.position;
            Vector2 pos2 = NowPoint.transform.position;
            return  (pos1 - pos2).magnitude / OriDis * 100;
        }
    }

    public void Ini(RectTransform parentRect,Camera UIcamera)
    {
        pinanim.gameObject.SetActive(false);
        Debug.Log("初始化");
        Vector2 ScreenPos1 = Camera.main.WorldToScreenPoint(WorldTransform1.position);
        Vector2 ScreenPos2 = Camera.main.WorldToScreenPoint(WorldTransform2.position);
        Debug.Log(ScreenPos1+"    " +ScreenPos2);
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRect, ScreenPos1, 
                UIcamera.GetComponent<Camera>(), out Vector2 localPoint1)
        )
        {
            //Debug.Log("1");
            TargetPoint.transform.GetComponent<RectTransform>().anchoredPosition = localPoint1;
        }
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRect,
            ScreenPos2, UIcamera.GetComponent<Camera>(), out Vector2 localPoint2))
        {
            //Debug.Log("2");
            NowPoint.transform.GetComponent<RectTransform>().anchoredPosition = localPoint2;
        }
    }

    public void PosUpdate(RectTransform parentRect,Camera UIcamera)
    {
        Vector2 ScreenPos1 = Camera.main.WorldToScreenPoint(WorldTransform1.position);
        Vector2 ScreenPos2 = Camera.main.WorldToScreenPoint(WorldTransform2.position);
        //Debug.Log(ScreenPos1+"    " +ScreenPos2);
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRect, ScreenPos1, 
            UIcamera.GetComponent<Camera>(), out Vector2 localPoint1)
        )
        {
            //Debug.Log("1");
            TargetPoint.transform.GetComponent<RectTransform>().anchoredPosition = localPoint1;
        }
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRect,
            ScreenPos2, UIcamera.GetComponent<Camera>(), out Vector2 localPoint2))
        {
            //Debug.Log("2");
            NowPoint.transform.GetComponent<RectTransform>().anchoredPosition = localPoint2;
        }
    }
    public void Alpha(float alpha)
    {
        TargetPoint.transform.parent.GetComponent<CanvasGroup>().alpha = alpha;
    }
    
    public void Finish()
    {
        Debug.Log("daoda");
        NowPoint.GetComponent<RectTransform>().anchoredPosition = TargetPoint.GetComponent<RectTransform>().anchoredPosition;
        isFinish = true;
        
        TargetPoint.transform.GetComponent<Image>().color = Color.green;
        NowPoint.transform.GetComponent<Image>().color = Color.green;
        
        Tool.Timer.Register(.8f, delegate
        {
            Alpha(0);
            pinanim.gameObject.SetActive(true);
            pinanim.AnimPlay();
        });
    }
}
public class DragSizePoint : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    public SizePoint sizePoint;
    public Axis axis;
    private RectTransform m_RT;

    public SizeMove sizeMove;
    public Camera UIcamera;
    public float nowDis;
    public Vector3 Trans;
    public bool anim;
    public Animator animator;
    private void Start()
    {
        
        sizePoint.Ini(transform.parent.GetComponent<RectTransform>(),UIcamera);
        animator = transform.GetComponent<Animator>();
        animator.enabled = false;
        Trans = transform.GetComponent<RectTransform>().transform.position;
        Vector2 pos1 = sizePoint.TargetPoint.transform.position;
        Vector2 pos2 = sizePoint.NowPoint.transform.position;
        Vector2 pos3 = pos1;
        Vector2 pos4 = pos2;
        sizePoint.OriDis = (pos3 - pos4).magnitude;
        sizeMove = transform.parent.parent.GetComponent<SizeMove>();
        sizePoint.TargetPoint.GetComponent<Image>().enabled = false;
        
        nowDis = sizePoint.NowDis;
    }

    private void Update()
    {
        
        if (sizePoint.isEnable && !sizePoint.isFinish)
        {
            //Debug.Log("原始距离:"+sizePoint.OriDis + "实时距离:" + sizePoint.NowDis);
        }

        if (sizePoint.isEnable && !anim)
        {
            animator.enabled = true;
            
            
        }

        if (!sizePoint.isEnable)
        {
            sizePoint.Alpha(0.5f);
        }else if(sizePoint.isEnable && !sizePoint.isFinish)
        {
            sizePoint.Alpha(1f);
            if (Input.GetMouseButtonDown(0))
            {
                sizePoint.TargetPoint.GetComponent<Image>().enabled = true;
            }
        }
        if (sizePoint.NowDis >= 0 && sizePoint.NowDis <= 10 && sizePoint.isFinish != true)
        {
            VibratorManager.UpdateTrigger(4);
            sizePoint.Finish();
            Tool.Timer.Register(1.5f, delegate
            {
                sizeMove.NextPoint();
            });
        }
    }
    //Vector3 offPos;//存储按下鼠标时的图片-鼠标位置差
    public float offset;
    public Vector3 globalMousePos;
    //public Vector2 arragedPos;
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        if (sizePoint.isEnable && !sizePoint.isFinish)
        {
            anim = true;
            animator.enabled = false;
            transform.localScale = Vector3.one;
            
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(transform.parent.parent.GetComponent<RectTransform>(), eventData.position
                , eventData.pressEventCamera, out globalMousePos))
            {
                Debug.Log(transform.GetComponent<RectTransform>().position);
            
                switch (axis)
                {
                    case Axis.X:
                        offset = (transform.GetComponent<RectTransform>().position - globalMousePos).x;
                    
                        break;
                    case Axis.Y:
                        offset = (transform.GetComponent<RectTransform>().position - globalMousePos).y;
                    
                        break;
                }
            }
            
        }


        //offPos = transform.position- Input.mousePosition ;
    }
    public Vector3 DragposX;
    public Vector2 RangeX;
    
    public Vector3 DragposY;
    public Vector2 RangeY;
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if (sizePoint.isEnable && !sizePoint.isFinish)
        {
            VibratorManager.UpdateTrigger(1);
            Debug.Log("移动");
            switch (axis)
            {
                case Axis.X:
                    if (RectTransformUtility.ScreenPointToWorldPointInRectangle(transform.parent.parent.GetComponent<RectTransform>(), eventData.position,
                        eventData.pressEventCamera, out DragposX))
                    {
                        transform.GetComponent<RectTransform>().position = new Vector3(Mathf.Clamp(DragposX.x+offset,RangeX.x,RangeX.y),transform.GetComponent<RectTransform>().position.y,transform.GetComponent<RectTransform>().position.z);
                        
                    }
                    
                    break;
                case Axis.Y:
                    if (RectTransformUtility.ScreenPointToWorldPointInRectangle(transform.parent.parent.GetComponent<RectTransform>(), eventData.position,
                        eventData.pressEventCamera, out DragposY))
                    {
                        transform.GetComponent<RectTransform>().position = new Vector3(transform.GetComponent<RectTransform>().position.x,   Mathf.Clamp(DragposY.y+offset,RangeY.x,RangeY.y) ,transform.GetComponent<RectTransform>().position.z);
                        
                    }
                    
                    break;
            }
        
            //print("拖拽中……");
        }
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        if (sizePoint.isEnable && !sizePoint.isFinish)
        {
            sizePoint.TargetPoint.GetComponent<Image>().enabled = false;
        }
        VibratorManager.UpdateTrigger(5);
        //offset = new Vector2();
        
    }
}
