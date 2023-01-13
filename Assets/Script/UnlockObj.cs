using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UnlockObj : MonoBehaviour
{
    public int tag;
    public bool Unlock;
    public bool IsInsLock = false;
    private void OnEnable()
    {
        if (Data.GetObjLock(tag))
        {
            Unlock = true;
        }
        else
        {
            if (!IsInsLock)
            {
                Debug.Log("上锁");
                Unlock = false;
                IsInsLock = true;
                GameObject lOCKOBJ = Instantiate(GameManager.Instance.LockImg, transform);
                lOCKOBJ.GetComponent<Lock>().TextReflash(tag);
            }
            
            transform.GetComponent<Image>().raycastTarget = false;
        }
    }
}
