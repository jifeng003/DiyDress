using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class ObjBoxManager : MonoBehaviour
{
    
    public float[] Scales;
    public int CurrentObjTag;
    
    //public Collider Tai;
    public bool IsEnable;

    public List<bool> lockObjs = new List<bool>();
    
    
    public ObjBox[] ObjBoxs;
    public int UnlockObjNumber;
    public GameObject[] BoxGroup;

    public List<GameObject> Shadow;
    public Transform ShadowP;
    private void Awake()
    {
        
        Shadow.Clear();
        UnlockObjNumber = Data.GetUnlockObjUnmber();
        UnlockCanvas.blocksRaycasts = false;
        showObjBox();
    }

    public void showObjBox()
    {
        
        foreach (var VARIABLE in BoxGroup)
        {
            VARIABLE.SetActive(false);
        }
        BoxGroup[UnlockObjNumber].SetActive(true);
        
        lockObjs = new List<bool>();
        for (int i = 0; i < 3; i++)
        {
            if (BoxGroup[UnlockObjNumber].transform.GetChild(i).GetComponent<ObjBox>().UnLock)
            {
                Debug.Log(transform.name + "生成阴影");
                GameObject ShadowObj = Instantiate(Under, BoxGroup[UnlockObjNumber].transform.GetChild(i).transform.position, Quaternion.Euler(90,0,0),ShadowP);
                Shadow.Add(ShadowObj);
            }
        }
        
        
    }

    public GameObject Under;
    public void OpenBox(int TAG)
    {
        ObjBoxs[TAG].Unlock();
        GameObject ShadowObj = Instantiate(Under, ObjBoxs[TAG].transform.position, Quaternion.Euler(90,0,0),ShadowP);
        Shadow.Add(ShadowObj);
        Debug.Log("解锁" );
        Show(null,.5f,UnlockCanvas,TAG);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit,100,1 << LayerMask.NameToLayer("ObjBox"))  
                &&  IsEnable )
                //&& Clame 
            {
                if (MoneyManager.Instance.CanBuy(hit.transform.GetComponent<ObjBox>().Price))
                {
                    Debug.Log("足够购买");
                    hit.transform.GetComponent<ObjBox>().UnLock = true;
                    OpenBox(hit.transform.GetComponent<ObjBox>().Tag);
                }
            }
        }
    }

    public CanvasGroup UnlockCanvas;
    public GameObject CanvasObjs;
    private GameObject CanvasObj;
    //public float CanvasObjSCALE;
    public Button DragBack;
    public Button IdleBack;
    public bool isDrag;
    /// <summary>
    /// 展示界面
    /// </summary>
    /// <param name="callBack"></param>
    /// <param name="duration"></param>
    public void Show(Action callBack ,float duration,CanvasGroup canvasGroup,int tag)
    {
        if (DragBack.gameObject.activeSelf)
        {
            DragBack.gameObject.SetActive(false);
            isDrag = true;
        }
        if (IdleBack.gameObject.activeSelf)
        {
            IdleBack.gameObject.SetActive(false);
            isDrag = false;
        }
        
        CanvasObj = CanvasObjs.transform.GetChild(tag).gameObject; 
        CanvasObj.transform.localScale = Vector3.zero;
        
        CanvasObj.SetActive(true);
        CanvasObj.transform.DOScale(Scales[tag],duration);
        Debug.Log("BIANDA");
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.DOFade(1, duration/2).OnComplete(delegate
        {
            canvasGroup.blocksRaycasts = true;
            
            lockObjs = new List<bool>();
            for (int i = 0; i < 3; i++)
            {
                lockObjs.Add(BoxGroup[UnlockObjNumber].transform.GetChild(i).GetComponent<ObjBox>().UnLock); 
            }
            
            if (!lockObjs.Contains(false))
            {
                if (UnlockObjNumber != 3)
                {
                    Data.SetUnlockObjUnmber();
                    UnlockObjNumber = Data.GetUnlockObjUnmber();
                    
                }
                showObjBox();
                foreach (var VARIABLE in Shadow)
                {
                    Destroy(VARIABLE);
                }
            }
        });

        CurrentObjTag = tag;
    }

    public void ButtonHide()
    {
        VibratorManager.Trigger(2);
        Hide(null,.3f,UnlockCanvas);
    }
    /// <summary>
    /// 隐藏界面
    /// </summary>
    /// <param name="callBack"></param>
    /// <param name="duration"></param>
    public void Hide(Action callBack ,float duration,CanvasGroup canvasGroup)
    {

        CanvasObj = CanvasObjs.transform.GetChild(CurrentObjTag).gameObject;
        CanvasObj.transform.DOScale(Vector3.zero,duration/2);
        CanvasObj.transform.DOLocalRotate(new Vector3(0,0,720),3f);
        Data.GetObjUnlock(CurrentObjTag);
        canvasGroup.blocksRaycasts = false;
        canvasGroup.DOFade(0, duration).OnComplete(delegate
        {
            canvasGroup.blocksRaycasts = false;
            CanvasObj.SetActive(false);
            Debug.Log(CanvasObj.name);
            
            
            if (isDrag)
            {
                DragBack.gameObject.SetActive(true);
            }
            else
            {
                IdleBack.gameObject.SetActive(true);
            }

        });
    }
    


    

}
