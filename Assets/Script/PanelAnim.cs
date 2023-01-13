using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class PanelAnim : MonoBehaviour
{
    public Vector3 OriPos;
    public Vector3 lowPos;
    private Vector3 RectPos;
    public GameObject button;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ShowPanel();
        }
    }

    private void OnEnable()
    {
        ShowPanel();
    }

    public void ShowPanel()
    {
        VibratorManager.Trigger(2);
        RectPos = transform.GetComponent<RectTransform>().anchoredPosition;
        Vector3 pos = RectPos;
        OriPos = pos;
        Debug.Log("展示");
        transform.DOMoveY(0, 1f).SetEase(Ease.InOutBack);
        if (button != null)
        {
            button.SetActive(false);
            
        }
    }

    public void hidePanel()
    {
        VibratorManager.Trigger(2);
        RectPos = transform.GetComponent<RectTransform>().anchoredPosition;
        Debug.Log("展示");
        transform.DOMoveY(-Screen.height, 1f).SetEase(Ease.InOutBack).OnComplete(() =>
        {
            if (button != null)
            {
                button.SetActive(true);
            }
        });

    }
}
