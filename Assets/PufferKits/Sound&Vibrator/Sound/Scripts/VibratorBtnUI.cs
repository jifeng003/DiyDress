using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VibratorBtnUI : MonoBehaviour
{
    public Button btn;

    public Image btnImg;
    // public Image vibratorOn;
    //public Image vibratorOff;

    public Sprite vibratorOnSprite;
    public Sprite vibratorOffSprite;

    bool firstload;


    private void Awake()
    {
        if (VibratorManager.IsiPadOriPod())
        {
            gameObject.SetActive(false);
        }
        else
        {
            btn.GetComponent<Button>().onClick.AddListener(OnClick);
            firstload = true;
        }
    }

    private void Start()
    {
        if (!firstload)
            return;
        firstload = false;
        VibratorManager.BindEvent(UpdateInfo);
    }

    private void OnClick()
    {
        VibratorManager.Switch();
        VibratorManager.Trigger(1);
    }

    private void UpdateInfo(bool isVibratorEnable)
    {
        btnImg.sprite = null;
        btnImg.sprite = isVibratorEnable ? vibratorOnSprite : vibratorOffSprite;
        // vibratorOn.gameObject.SetActive(vibratoron);
        //vibratorOff.gameObject.SetActive(!vibratoron);
    }
}