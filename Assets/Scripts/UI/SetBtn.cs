using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SetBtn : MonoBehaviour
{
    bool isShow = false;
    public GameObject VibratorBtn;
    // Start is called before the first frame update
    private void Start()
    {
        VibratorBtn.transform.DOScaleX(0f, 0);
        VibratorBtn.transform.DOScaleY(0f, 0);
    }
    public void Show()
    {
        VibratorManager.Trigger(1);
        isShow = !isShow;
        if (isShow)
        {
            VibratorBtn.transform.DOScaleX(1f, 0.3f);
            VibratorBtn.transform.DOScaleY(1f, 0.3f);
        }
        else
        {
            VibratorBtn.transform.DOScaleX(0f, 0.3f);
            VibratorBtn.transform.DOScaleY(0f, 0.3f);
        }
    }
}