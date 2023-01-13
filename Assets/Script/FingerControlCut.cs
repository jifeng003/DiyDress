using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using DG.Tweening;
using FluffyUnderware.Curvy;
using FluffyUnderware.Curvy.Controllers;
using Pathfinding.Util;
using Sirenix.OdinInspector;
using UnityEngine;

public class FingerControlCut : MonoBehaviour
{
    public List<Transform> CutNodes = new List<Transform>();
    public SplineController Cut;
    public bool CutFinish;
    private void Awake()
    {
        CurvySplineSegment[] curvySplineSegments =  transform.GetComponentsInChildren<CurvySplineSegment>();
        foreach (var VARIABLE in curvySplineSegments)
        {
            CutNodes.Add(VARIABLE.transform);
        }

        foreach (var VARIABLE in ClothCut)
        {
            VARIABLE.GetComponent<Renderer>().material.CopyPropertiesFromMaterial(VARIABLE.GetComponent<Renderer>().materials[0]);
        }

        NeedNotePointsNumber = 0;
    }

    public int currentNum;
    private float MoveLenth;
    private Vector2 OldMousePos;
    private Vector2 NewMousePos;
    private Vector2 MouseDir;
    private Vector2 LineDir;
    public Transform[] ClothCut;
    public ParticleSystem particleSystem;
    public int[] NeedNotePoints;
    public int[] SpecialNotePoints;
    public int NeedNotePointsNumber;
    private void Update()
    {
        if (!CutFinish)
        {
            
            if (Input.GetMouseButtonDown(0))
            {
                Cut.GetComponent<Animator>().enabled = true;
                Vector3 pos = Input.mousePosition;
                OldMousePos = pos;
            }
            if (Input.GetMouseButton(0))
            {
                VibratorManager.Trigger(0);
                NewMousePos = Input.mousePosition;
        
                MouseDir = NewMousePos - OldMousePos;
                if (SpecialNotePoints.Contains(currentNum))
                {
                    Cut.Speed = 10;
                }
                else
                {
                    if (currentNum + 1 == CutNodes.Count)
                    {
                        LineDir = Camera.main.WorldToScreenPoint(CutNodes[0].position)
                                  -Camera.main.WorldToScreenPoint(CutNodes[currentNum].position);
                    }
                    else
                    {
                        LineDir = Camera.main.WorldToScreenPoint(CutNodes[currentNum+1].position)
                                  -Camera.main.WorldToScreenPoint(CutNodes[currentNum].position);
                    }
                    
                    MoveLenth = MouseDir.magnitude / LineDir.magnitude;
                    checkDir();
                }
                OldMousePos = NewMousePos;

            }

            if (Input.GetMouseButtonUp(0))
            {
                OldMousePos = Vector2.zero;
                NewMousePos = Vector2.zero;
                Cut.Speed = 0f;
                Cut.GetComponent<Animator>().enabled = false;
            }
        }
        
    }

    

    public void checkDir()
    {
        //Vector3.Angle(MouseDir, LineDir) < 120 &&
        if ( MouseDir.magnitude > 0.01f)
        {
            Cut.Speed = 0.5f + 3f * MoveLenth;
            particleSystem.Play();
        }
        else
        {
            Cut.Speed = 0f;
        }

    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     Debug.Log("到达");
    //     currentNum++;
    // }
    [ShowInInspector]
    private Transform ClothModel;
    [ShowInInspector]
    private ShowClothAnim ShowClothAnim;

    private void Start()
    {
        ShowClothAnim = Selectcloth.Ins.CurrentShowGirl.transform.GetComponent<ShowClothAnim>();
        ClothModel = ShowClothAnim.Cloth;
        particleSystem.GetComponent<Renderer>().material = ClothModel.GetComponent<Renderer>().material;
    }
    public void nextNode()
    {
        Debug.Log("到达");
        currentNum++;
        if (NeedNotePoints.Contains(currentNum))
        {
            ClothCut[NeedNotePointsNumber].DOLocalMoveZ(0.5f, 0.5f).OnComplete(() =>
            {
                FideColor(ClothCut[NeedNotePointsNumber].GetComponent<MeshRenderer>(), 0, 0.3f);
                NeedNotePointsNumber++;
            });
        }
        if (currentNum == CutNodes.Count)
        {
            Debug.Log("动画");
            VibratorManager.Trigger(4);
            CutFinish = true;
            Tool.Timer.Register(.5f, delegate
            {
                Cut.GetComponent<Animator>().enabled = false;
                ClothCut[0].parent.DOScale(.5f, .5f);
                ClothModel.DOLocalMoveY(0.25f, .5f).SetEase(Ease.InOutBounce).OnComplete((() =>
                {
                    ClothCut[0].parent.gameObject.SetActive(false);
                    Debug.Log("开始动画");
                    ShowClothAnim.BeginAnim();
                }));
            });
            
        }
        Cut.Speed = 0f;
        
    }
    public void FideColor(Renderer renderer,float targetAphla,float duration)
    {
        Color startColor = renderer.sharedMaterial.GetColor("_BaseColor");
        
        Color targetColor = new Color(startColor.r,startColor.g,startColor.b,targetAphla);
        DOTween.To((value =>
        {
            targetColor.a = value;
            renderer.sharedMaterial.SetColor("_BaseColor",targetColor);
            
        })
            , startColor.a, targetAphla, duration).OnComplete((() =>
        {
            //callBack?.Invoke();
        })
            );
    }
}
