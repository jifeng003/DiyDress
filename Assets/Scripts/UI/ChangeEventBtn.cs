using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEventBtn : MonoBehaviour
{
    [Header("切换页面")]
    public GameObject changeEvent;
    public GameObject currentEvent;
    //public List<int> tag = new List<int>();
    //public titlePanel TitlePanel;
    public bool SceneChange;
    
    public void ChangeEventClick()
    {
        VibratorManager.Trigger(2);
        if (SceneChange)
        {
            GameManager.Instance.IdlePart.transform.parent.gameObject.SetActive(false);
            gameObject.SetActive(false);
            LevelManager.Instance.NextLevel();
            GameManager.Instance.IdleButton.SetActive(true);
            GameManager.Instance.LevelShow.SetActive(true);
            
            GameManager.Instance.BackButton.gameObject.SetActive(false);
            //changeEvent = GameManager.Instance.Levelstart[GameManager.Instance.level];
        }
        else
        {
            if (GameManager.Instance)
            {
                GameManager.Instance.IdleButton.SetActive(false);
                GameManager.Instance.LevelShow.SetActive(false);
            }
            
            currentEvent.SetActive(false);
            changeEvent.SetActive(true);
            GameManager.Instance.Canvas.peopleSet(false);
            GameManager.Instance.Canvas.IdelSet(false);
        }
        
    }
    //
    // public void ChoseOptionPanel()
    // {
    //     foreach (var VARIABLE in TitlePanel.Titles)
    //     {
    //         VARIABLE.gameObject.SetActive(false);
    //     }
    //     
    //     foreach (var VARIABLE in tag)
    //     {
    //         TitlePanel.Titles[VARIABLE].gameObject.SetActive(true);
    //     }
    //
    //     TitlePanel.titleSeq();
    // }
    //
}
