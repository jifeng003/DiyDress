using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using DG.Tweening;
using FluffyUnderware.Curvy.Controllers;
using PaintIn3D;
using RootMotion;
using UnityEngine;
using UnityEngine.EventSystems;
using ZYB;


public class DragIns : MonoBehaviour
{
    Vector2 offset;
    public Camera UIcamera;
    public CanvasGroup uibg;
    public CanvasGroup ShowPanel;
    private Animator _animator;

    public PeoplePosContainer PosContainer;
    public Vector3 oriPos = new Vector3(0, -4.5f, 98);

    public ObjBoxManager ObjBoxManager;

    private void OnEnable()
    {
        ObjBoxManager.IsEnable = false;
    }

    private void OnMouseDown()
    {
        Debug.Log("按下");
        if (Data.GetCurLevel == 0)
        {
            GameManager.Instance.IdleGuide.SetActive(false);
        }
        offset = transform.position - MouseWorldPosition();
    }

    public Vector2 RangeX;
    public Vector2 RangeY;
    private void OnMouseDrag()
    {
        
        //移动限制
        float clampX  = Mathf.Clamp(MouseWorldPosition().x + offset.x,RangeX.x,RangeX.y) ;
        float clampY  = Mathf.Clamp(MouseWorldPosition().y + offset.y,RangeY.x,RangeY.y) ;
        
        transform.position = new Vector3(clampX, clampY, transform.position.z);

    }

    [HideInInspector] public  SplineController splineController;
    
    public Showgril showgril;
    public JsonSave Json;
    public GameObject ClickTip;
    private void OnMouseUp()
    {
        Debug.Log("抬起");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.name == "Tai1")
            {
                Canvas(uibg,false);
                Canvas(ShowPanel,true);

                transform.gameObject.SetActive(false);
                showgril.TieCloth[showgril.CurrentTieCloth].GetComponentInChildren<P3dPaintableTexture>().Deactivate();
                Json.datas.Models.Add(showgril.model);
                if (Data.GetCurLevel == 0 && Data.GetCurStageLevel() == 0)
                {
                    Debug.Log("增加初始观众");
                    GameObject Audience = PoolManager.instance.SpawnFromPool("00");
                    Audience.GetComponent<audience>().first = true;
                    Data.UpAudienceNumber();
                    if (Data.GetCurStageLevel() == 0)
                    {
                        ClickTip.SetActive(true);
                    }
                }
                PosContainer.AddPeople(showgril.model);
                GameManager.Instance.BackButton.gameObject.SetActive(true);
                transform.DOLocalMove(oriPos, .5f);
                transform.DOLocalRotate(new Vector3(0,180,0), .5f);
                ObjBoxManager.IsEnable = true;
                showgril.enabled = false;
                Data.UpLevel();
            }
        }
        Json.SaveDatasJson();
        Json.ReadDatasJson();
        transform.DOLocalMove(oriPos, .5f);
        transform.DOLocalRotate(new Vector3(0,180,0), .5f);
    }

    private void OnDisable()
    {
        ObjBoxManager.IsEnable = true;
    }

    /// <summary>
    /// 鼠标位置转3D位置
    /// </summary>
    /// <returns> 转换后位置</returns>
    private Vector3 MouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = UIcamera.WorldToScreenPoint(transform.position).z;
        return UIcamera.ScreenToWorldPoint(mouseScreenPos);
    }
    
    public void Canvas(CanvasGroup canvasGroup,bool Open)
    {
        if (Open)
        {
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
        else
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }
}