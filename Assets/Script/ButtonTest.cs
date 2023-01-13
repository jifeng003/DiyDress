using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonTest : MonoBehaviour
{
    public  RaycastHit hit;
    public bool isonCloth;
    public Vector2 localPoint;
    public Camera uiCamera;
    public RectTransform ui;
    public bool isdrag;
    
    [HideInInspector]public RectTransform moveImg;
    [HideInInspector]public RectTransform CloseImg;
    [HideInInspector]public RectTransform CubeImg;
    
    public Vector2 offset;
    public PanelAnim PanelAnim;
    
    public bool hideplay;

    public UnityAction PanelHideEvent;

    public titlePanel TitlePanel;
    public GameObject NiukouTip;
    public Vector2 getButtonPos
    {
        get
        {
            Vector2 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                ui.GetComponent<RectTransform>(), screenPoint, uiCamera.GetComponent<Camera>(), out localPoint);
            return localPoint;
        }
    }
    private void Start()
    {
        offset = new Vector2(80, 80);
        moveImg = ui.transform.Find("move").GetComponent<RectTransform>();
        CloseImg = ui.transform.Find("close").GetComponent<RectTransform>();
        CubeImg = ui.transform.Find("cube").GetComponent<RectTransform>();
        ui = transform.parent.Find("Canvas").GetComponent<RectTransform>();
        CloseImg.transform.GetComponent<DecalClose>().PanelAnim = PanelAnim;
        moveImg.gameObject.SetActive(false);
        CloseImg.gameObject.SetActive(false);
        CubeImg.gameObject.SetActive(false);

        PanelHideEvent = new UnityAction(OnbuttonOn);
        transform.parent.GetComponentInChildren<Canvas>().worldCamera = uiCamera;
        NiukouTip = TitlePanel.niukouTips;
    }

    public bool isEnable;
    private RaycastHit hit1;
    private void Update()
    {
        if (TitlePanel && TitlePanel.eselecttitle == Eselecttitle.niukou && this.gameObject.activeSelf)
        {
            Vector2 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
            Vector2 Point;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                ui.GetComponent<RectTransform>(), screenPoint, uiCamera.GetComponent<Camera>(), out Point);
            
            if (!isdrag)
            {
                Ray ray1 = Camera.main.ScreenPointToRay(Input.mousePosition);
    
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (!Physics.Raycast(ray, out hit, 1000, (1 << LayerMask.NameToLayer("Niukou"))))
                    {
                        CloseImg.gameObject.SetActive(false);
                        moveImg.gameObject.SetActive(false);
                        CubeImg.gameObject.SetActive(false);
                    }
                    else
                    {
                        Physics.Raycast(ray, out hit);
                        if (NiukouTip)
                        {
                            NiukouTip.SetActive(false);
                        }
                        Debug.Log("按下",hit.transform.gameObject);
                        uiCamera.gameObject.SetActive(true);
                        isEnable = true;
                    }
                }
                if (Input.GetMouseButton(0) && isEnable)
                {
                    
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    
                        if (!hideplay)
                        {
                            PanelAnim.hidePanel();
                            hideplay = true;
                        }
                        if (Physics.Raycast(ray, out hit,1000,(1 << LayerMask.NameToLayer("Target"))))
                        {
                            if (hit.transform.gameObject.layer != 5)
                            {
                                isonCloth = true;
                                Debug.Log("检测点",hit.transform.gameObject);
                                CloseImg.gameObject.SetActive(false);
                                moveImg.gameObject.SetActive(false);
                                CubeImg.gameObject.SetActive(false);
                            
                                transform.position = hit.point; 
                                transform.up= hit.normal;
                            }
                        }
                    //}
                    
                
                    // if (transform.GetComponent<BoxCollider>())
                    // {
                    //     transform.GetComponent<BoxCollider>().size = CubeImg.localScale * 0.1f;
                    // }
                    // else
                    // {
                    //     transform.GetComponent<SphereCollider>().radius = CubeImg.localScale.x * 0.1f;
                    // }
                }
            }
            CloseImg.anchoredPosition = Point - offset;
            moveImg.anchoredPosition = Point + offset;
            CubeImg.anchoredPosition = Point;
            if (Input.GetMouseButtonUp(0))
            {
                CloseImg.gameObject.SetActive(true);
                moveImg.gameObject.SetActive(true);
                CubeImg.gameObject.SetActive(true);
                hideplay = false;
                isEnable = false;
            } 
        }
        else
        {
            CloseImg.gameObject.SetActive(false);
            moveImg.gameObject.SetActive(false);
            CubeImg.gameObject.SetActive(false);
        }
    }

    public void OnbuttonOn()
    {
        Debug.Log("隐藏");
        CloseImg.gameObject.SetActive(false);
        moveImg.gameObject.SetActive(false);
        CubeImg.gameObject.SetActive(false);
        
    }
}
