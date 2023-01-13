using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using DG.Tweening;
using FluffyUnderware.Curvy.Utils;
using UnityEngine;

public class SizeMove : MonoBehaviour
{
    public List<DragSizePoint> dragSizePoint = new List<DragSizePoint>();
    public SkinnedMeshRenderer TargetCloth;
    public bool isFinish;
    private void Update()
    {
        if (!isFinish)
        {
            for (int i = 0; i < dragSizePoint.Count; i++)
            {
                TargetCloth.SetBlendShapeWeight(i,100-dragSizePoint[i].sizePoint.NowDis);
            }
            //dragSizePoint[CurrentNum].sizePoint.isEnable = true;
        }
    }

    private void Awake()
    {
        CurrentNum = dragSizePoint.Count - 1;
        dragSizePoint[CurrentNum].sizePoint.isEnable = true;
    }

    public int CurrentNum;

    public void NextPoint()
    {
        TargetCloth.SetBlendShapeWeight(CurrentNum,100);
        
        CurrentNum--;

        if (CurrentNum < 0)
        {
            isFinish = true;
            ZhazhenWinMove();
        }
        else
        {
            TargetCloth.SetBlendShapeWeight(Math.Abs(CurrentNum-2),100);
            dragSizePoint[CurrentNum].sizePoint.isEnable = true;
        }
        
    }

    public GameObject MatchPart;
    public GameObject zhen;
    public Animator Ren;
    public PanelAnim PanelAnim;
    public void ZhazhenWinMove()
    {
        Tool.Timer.Register(1.6f, delegate
        {
            Debug.Log("next");
            MatchPart.SetActive(true);
            
            zhen.SetActive(false);
            Ren.enabled = true;
            Ren.GetComponent<Showgril>().enabled = true;
            PanelAnim.ShowPanel();
        });
    }
}

