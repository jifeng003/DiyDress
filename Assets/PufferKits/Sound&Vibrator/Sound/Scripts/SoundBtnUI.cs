using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundBtnUI : MonoBehaviour
{
    public Button btn;
    public Image btnImg;
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;
    private bool firstload;

    private void Awake()
    {
        btn.GetComponent<Button>().onClick.AddListener(OnClick);
        firstload = true;
    }

    private void Start()
    {
        if (!firstload)
            return;
        firstload = false;
        AudioManager.BindEvent(UpdateInfo);
    }

    private void OnClick()
    {
        AudioManager.Switch();
        AudioManager.PlaySFX(0);
    }

    private void UpdateInfo(bool isSoundOn)
    {
        btnImg.sprite = isSoundOn ? soundOnSprite : soundOffSprite;
    }
}