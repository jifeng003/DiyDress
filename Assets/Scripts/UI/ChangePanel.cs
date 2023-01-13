using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using PaintIn3D;
public class ChangePanel : MonoBehaviour
{

    public List<int> panels = new List<int>();

    public Showgril currentGirl;
    public GameObject currentEvent;
    
    public Showgril changeGirl;
    public GameObject changeEvent;
    

    public titlePanel titlePanel;

    public bool isIdelScene;

    // Update is called once per frame
    private void Start()
    {
        foreach (var VARIABLE in titlePanel.TitleEvents)
        {
            panels.Add(VARIABLE.Tag);
        }

        if (GameManager.Instance)
        {
            changeGirl = GameManager.Instance.IdlePart.transform.GetComponent<Showgril>();
            changeGirl.TitlePanel = titlePanel;
            changeEvent = GameManager.Instance.IdlePart.transform.parent.gameObject;
            currentGirl = Selectcloth.Ins.CurrentShowGirl;
        }
        
    }

    public void DownClick()
    {
        for (int i = 0; i < panels.Count; i++)
        {
            if( i == panels.Count-1)
            {
                VibratorManager.Trigger(2);
                Debug.Log("next------------->");
                
                changeGirl.isTiePart = currentGirl.isTiePart;
                changeGirl.CurrentTieCloth = currentGirl.CurrentTieCloth;
                changeGirl.iscaijianPart = currentGirl.iscaijianPart;
                changeGirl.Currentzz = currentGirl.Currentzz;
                changeGirl.iszhazhenPart = currentGirl.iszhazhenPart;
                changeGirl.CurrentS1 = currentGirl.CurrentS1;
                changeGirl.TitlePanel = currentGirl.TitlePanel;
                changeGirl.NiukouPos = currentGirl.NiukouPos;
                changeGirl.NiukouRoatate = currentGirl.NiukouRoatate;
                changeGirl.NiukouScale = currentGirl.NiukouScale;

                currentEvent.SetActive(false);
                changeEvent.SetActive(true);
                
                //changeGirl.baobao
                changeGirl.shangyi = currentGirl.shangyi;
                changeGirl.xiazhuang = currentGirl.xiazhuang;
                changeGirl.xiezi = currentGirl.xiezi;
                changeGirl.faxin = currentGirl.faxin;
                changeGirl.baobao = currentGirl.baobao;
                changeGirl.toushi = currentGirl.toushi;
                changeGirl.shoushi = currentGirl.shoushi;
                changeGirl.niukou = currentGirl.niukou;

                changeGirl.enabled = true;
                GameManager.Instance.Canvas.peopleSet(true);
                GameManager.Instance.Canvas.IdelSet(false);

                if (Data.GetCurLevel == 0)
                {
                    GameManager.Instance.IdleGuide.SetActive(true);
                }
                break;
            }
            if (titlePanel.optionObjs[panels[i]].activeSelf)
            {
                VibratorManager.Trigger(1);

                titlePanel.SetSelecttitle(panels[i+1]);
                break;
            }

        }
        
    }
}
