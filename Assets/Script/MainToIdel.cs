using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainToIdel : MonoBehaviour
{
    public GameObject level;
    public GameObject IdelPart;
    public CanvasGroup peopleStage;
    public CanvasGroup IdelStage;
    public Button Back;
    public Button Stage;
    public GameObject levelUI;
    public GameObject People;
    private void Awake()
    {
        Back.gameObject.SetActive(false);
        Stage.gameObject.SetActive(true);
        levelUI.SetActive(true);
    }

    public void ButtonClick()
    {
        VibratorManager.Trigger(2);

        peopleSet(false);
        IdelSet(true);
        
        level.SetActive(false);
        IdelPart.SetActive(true);
        
        
        Back.gameObject.SetActive(true);
        Stage.gameObject.SetActive(false);
        levelUI.SetActive(false);

        People.SetActive(false);
    }

    public void backButtion()
    { 
        
        level.SetActive(true);
        IdelPart.SetActive(false);
        
        peopleSet(false);
        IdelSet(false);
        
        Back.gameObject.SetActive(false);
        Stage.gameObject.SetActive(true);
        levelUI.SetActive(true);
        People.SetActive(true);

    }
    public void IdelSet(bool Open)
    {
        if (Open)
        {
            CanvasSet(IdelStage,true);
        }
        else
        {
            CanvasSet(IdelStage,false);

        }
    }
    
    public void peopleSet(bool Open)
    {
        if (Open)
        {
            CanvasSet(peopleStage,true);
        }
        else
        {
            CanvasSet(peopleStage,false);

        }
    } 
    public void CanvasSet(CanvasGroup canvasGroup,bool Open)
    {
        if (Open)
        {
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
        else
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }
}
