using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using DG.Tweening;
using PaintIn3D;
using UnityEngine;
using UnityEngine.Events;

public class Pen : MonoBehaviour
{
    public GameObject ui;
    public Camera uiCamera;
    

    public bool isdrag;
    private RaycastHit hit1;
    public Vector2 localPoint;
    public P3dPaintDecal penDecal;
    
    
    public RectTransform move;
    public RectTransform Close;
    public RectTransform Cube;
    public RectTransform TieShow;
    public Vector2 offset;


    public bool isEnable;
    public PanelAnim panelAnim;
    public bool hideplay;
    private void Awake()
    {
        penDecal = transform.GetComponentInChildren<P3dPaintDecal>();
    }

    private void Start()
    {
        ui.GetComponent<Canvas>().worldCamera = uiCamera;
        offset = new Vector2(80, 80);
        move.gameObject.SetActive(false);
        Close.gameObject.SetActive(false);
        Cube.gameObject.SetActive(false);
        TieShow.gameObject.SetActive(false);
    }


    public Vector2 getAnPos
    {
        get
        {
            Vector2 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                ui.GetComponent<RectTransform>(), screenPoint, uiCamera.GetComponent<Camera>(), out localPoint);
            return localPoint;
        }
    }

    public bool TieshowFinish;
    private void Update()
    {
        
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            ui.GetComponent<RectTransform>(), screenPoint, uiCamera.GetComponent<Camera>(), out localPoint);
        
        
        Close.anchoredPosition = getAnPos - offset;
        move.anchoredPosition = getAnPos + offset;
        Cube.anchoredPosition = getAnPos;
        
        // Vector3 worldPos=Camera.main.ScreenToWorldPoint(Input.mousePosition);//屏幕坐标转换世界坐标
        // Vector2 uiPos = ui.transform.InverseTransformPoint(worldPos);//世界坐标转换位本地坐标
        Vector2 uipos = Vector3.one;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(ui.GetComponent<RectTransform>(),
            Input.mousePosition, uiCamera, out uipos);
        //将屏幕空间点转换为RectTransform的局部空间中位于其矩形平面上的位置。
        //cam参数是与屏幕点关联的摄像机。对于Canvas中设置为Screen Space - Overlay模式的RectTransform，cam参数应为null。
        //当在提供PointerEventData对象的事件处理程序中使用ScreenPointToLocalPointInRectangle时，可以使用PointerEventData.enterEventData（用于悬停功能）或PointerEventData.pressEventCamera（用于单击功能）获取正确的摄像头。这将自动为给定事件使用正确的相机（或null）。

        
        
        //Debug.Log(TieShow.anchoredPosition);
        TieShow.anchoredPosition = uipos;
        TieShow.localScale = Cube.localScale;
        transform.GetComponent<BoxCollider>().size = Cube.localScale * 0.15f;
        
        if (!isdrag)
        {
            if (Input.GetMouseButtonDown(0))
            {
                
                Ray ray1 = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray1, out hit1, 1000, (1 << LayerMask.NameToLayer("Decal"))))
                {
                    Close.gameObject.SetActive(true);
                    move.gameObject.SetActive(true);
                    Cube.gameObject.SetActive(true);
                }
                else
                {
                    Close.gameObject.SetActive(false);
                    move.gameObject.SetActive(false);
                    Cube.gameObject.SetActive(false);
                }
            }
            if (Input.GetMouseButton(0))
            {
                
                Ray ray1 = Camera.main.ScreenPointToRay(Input.mousePosition);
                Debug.DrawRay(ray1.origin, ray1.direction, Color.blue);
                if (isEnable)
                {
                    if (Physics.Raycast(ray1, out hit1,1000,(1 << LayerMask.NameToLayer("Target"))))
                    {
                        if (!TieshowFinish)
                        {
                            TieShow.gameObject.SetActive(false);
                            TieshowFinish = true;
                        }
                        
                        if (hit1.transform.gameObject.tag == "TargetCloth")
                        {
                            VibratorManager.Trigger(1);
                            
                            move.gameObject.SetActive(false);
                            Cube.gameObject.SetActive(false);
                            Close.gameObject.SetActive(false);
                            
                            transform.position = hit1.point;
                            if (!hideplay)
                            {
                                panelAnim.hidePanel();
                                hideplay = true;
                            }
                            
                        }
                    }
                    else
                    {
                        VibratorManager.Trigger(0);
                        if (!TieshowFinish)
                        {
                            TieShow.gameObject.SetActive(true);
                        }
                        
                    }
                }
                else
                {
                    if (Physics.Raycast(ray1, out hit1,1000,(1 << LayerMask.NameToLayer("Decal"))))
                    {
                        Debug.Log("在物体上");
                        if (Physics.Raycast(ray1, out hit1,1000,(1 << LayerMask.NameToLayer("Target"))))
                        {
                            Debug.Log("在衣服上");
                            if (hit1.transform.gameObject.tag == "TargetCloth")
                            {
                                move.gameObject.SetActive(false);
                                Cube.gameObject.SetActive(false);
                                Close.gameObject.SetActive(false);
                                
                                VibratorManager.Trigger(1);
                                transform.position = hit1.point;
                            }
                        }
                    }
                    
                }
                
            }
            if(Input.GetMouseButtonUp(0))
            {
                if (isEnable)
                {
                    Ray ray1 = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if(TieshowFinish)
                    {
                        isEnable = false;
                        //Debug.Log("抬起时检测到在衣服上");
                        
                        TieShow.gameObject.SetActive(false);
                    }
                    else
                    {
                        distory();
                        TieShow.gameObject.SetActive(false);
                    }
                }
                Close.gameObject.SetActive(true);
                move.gameObject.SetActive(true);
                Cube.gameObject.SetActive(true);
            };
        }
        
    }

    private void closeImg()
    {
        move.gameObject.SetActive(false);
        Cube.gameObject.SetActive(false);
        Close.gameObject.SetActive(false);
    }
    public void distory()
    {
        Destroy(gameObject);
        panelAnim.ShowPanel();
    }
}