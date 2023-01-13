using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;

public class ClickUpSpeed : MonoBehaviour
{
    public PeoplePosContainer PosContainer;
    public GameObject ClickUpTip;
    public bool ClickTime;
    public StartPanel startPanel;

    private void Awake()
    {
        ClickTime = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            VibratorManager.Trigger(2);
        }
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            
            //移动端
            if (Application.platform == RuntimePlatform.Android ||
                Application.platform == RuntimePlatform.IPhonePlayer)
            {
                int fingerId = Input.GetTouch(0).fingerId;
                if (!EventSystem.current.IsPointerOverGameObject(fingerId))
                {
                    
                    //
                    // Vector2 uipos = Vector3.one;
                    // RectTransformUtility.ScreenPointToLocalPointInRectangle(ui.GetComponent<RectTransform>(),
                    //     Input.mousePosition, uiCamera, out uipos);
                    startPanel.ShowShowEffect(Input.mousePosition,1,true);
                    

                    ClickTime = true;
                    markTime = Time.time;
                    PosContainer.Speedratio = 1.8f;
                    if (ClickUpTip.activeSelf)
                    {
                        ClickUpTip.SetActive(false);
                    }
                }
            }
            //其它平台
            else
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    startPanel.ShowShowEffect(Input.mousePosition,1,true);
                    MoneyManager.Instance.Money += 1;
                    ClickTime = true;
                    markTime = Time.time;
                    PosContainer.Speedratio = 1.5f;  
                    if (ClickUpTip.activeSelf)
                    {
                        ClickUpTip.SetActive(false);
                    }
                }
            }
        }
        
        if (ClickTime)
        {
            runTime = Time.time;
        }
        if (runTime - markTime >1){
            ClickTime = false;
            PosContainer.Speedratio = Mathf.Lerp(PosContainer.Speedratio, 1f, .5f);
        }
    }
    
    public float markTime = 0, runTime= 0;


    /// <summary>
    /// 点击屏幕坐标
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public GameObject GetFirstPickGameObject(Vector2 position)
    {
        EventSystem eventSystem = EventSystem.current;
        PointerEventData pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = position;
        //射线检测ui
        List<RaycastResult> uiRaycastResultCache = new List<RaycastResult>();
        eventSystem.RaycastAll(pointerEventData, uiRaycastResultCache);
        if (uiRaycastResultCache.Count > 0)
            return uiRaycastResultCache[0].gameObject;
        return null;
    }

    // private void OnMouseDown()
    // {
    //     if (ClickUp.activeSelf)
    //     {
    //         ClickUp.SetActive(false);
    //     }
    //     if (PosContainer.Speedratio == 1)
    //     {
    //         VibratorManager.Trigger(4);
    //     }
    //     PosContainer.Speedratio = 1.5f;
    // }
    //
    // private void OnMouseUp()
    // {
    //     VibratorManager.Trigger(4);
    //     PosContainer.Speedratio = 1f;
    //
    // }
    // public bool IsPointerOverGameObject(Vector2 screenPosition)
    // {
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         //实例化点击事件
    //         PointerEventData eventDataCurrentPosition = new PointerEventData(UnityEngine.EventSystems.EventSystem.current);
    //         //将点击位置的屏幕坐标赋值给点击事件
    //         eventDataCurrentPosition.position = new Vector2(screenPosition.x, screenPosition.y);
    //
    //         List<RaycastResult> results = new List<RaycastResult>();
    //         //向点击处发射射线
    //         EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
    //
    //         return results.Count > 0;
    //     }
    //     return false;
    // }
//     private bool IsTouchedUI()//是否点击在UI上
//     {
//         bool touchedUI = true;
// #if UNITY_ANDROID
//         // Debug.Log("安卓设备");
//         touchedUI = IsPointerOverGameObject(Input.mousePosition);
// #endif
// #if UNITY_STANDALONE_WIN
//         //  Debug.Log("从Windows的电脑上运行");
//         if (EventSystem.current.IsPointerOverGameObject())
//         {
//             touchedUI = true;
//             return touchedUI;
//         }
//
// #endif
//         return touchedUI = false;
//     }
}
