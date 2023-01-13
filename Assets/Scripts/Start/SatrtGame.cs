using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatrtGame : MonoBehaviour
{
    public GameObject downEvent;
    public GameObject currentEvent;
    public Animator animator;
    int rang;
    float playTime;                    //随机隔多少秒换动画
    float doTime = 0;                   //计时
    bool isPlay;               //是否换动画

    // Update is called once per frame
    void Update()
    {
        PlayClip();
    }

    public void DownEventClick()
    {
        VibratorManager.Trigger(1);
        downEvent.SetActive(true);
        currentEvent.SetActive(false);
    }

    public void PlayClip()
    {
        doTime += Time.deltaTime * 1f;
        if (doTime > playTime)
        {
            isPlay = true;
        }
        if(isPlay)
        {
            if (rang == 0)
            {
                animator.Play("mixamo_com");
            }
            if (rang == 1)
            {
                animator.Play("DwarfIdle");
            }
            rang = Random.Range(0, 2);
            doTime = 0;
            playTime = Random.Range(5, 10);
            isPlay = false;
        }
    }
}
