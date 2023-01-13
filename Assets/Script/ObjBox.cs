using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class ObjBox : MonoBehaviour
{
    public int Tag;
    public Transform Gai;
    public TextMeshPro PriceShow;
    public TextMeshPro TagShow;
    public int Price;

    public bool UnLock;
    // public ObjBoxManager Manager;
    // public GameObject Under;
    private void Awake()
    {
        // Manager = transform.parent.parent.GetComponent<ObjBoxManager>();
        UnLock = Data.GetObjLock(Tag);
        Gai = transform.GetChild(0);
        Price = (Tag+1)*200;
        PriceShow.text = "$ "+Price.ToString();
        TagShow.text = (Tag+1).ToString("D2");
        gameObject.SetActive(true);
        
    }


    private void OnEnable()
    {
        
        if (UnLock)
        {
            gameObject.SetActive(false);
        }
    }

    public void Unlock()
    {
        Data.GetObjUnlock(Tag);
        Gai.DOLocalMoveY(0, 1f).OnComplete(() =>
        {
            gameObject.SetActive(false);
            UnLock = Data.GetObjLock(Tag);
            
        });
    }
    
}

// public void ShowBox(int i)
// {
//     switch (i)
//     {
//         case 0:
//             transform.localPosition = new Vector3(transform.localPosition.x,transform.localPosition.y,-3f);
//             break;
//         case 1:
//             transform.localPosition = new Vector3(transform.localPosition.x,transform.localPosition.y, -0.5f);
//             break;
//         case 2:
//             transform.localPosition = new Vector3(transform.localPosition.x,transform.localPosition.y, 2.5f);
//
//             break;
//     }
//     
// }