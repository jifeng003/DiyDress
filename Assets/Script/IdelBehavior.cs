using System;
using FluffyUnderware.Curvy.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using FluffyUnderware.Curvy;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

public class IdelBehavior : MonoBehaviour
{
   //[HideInInspector] public    CurvySpline spline;
    private ParticleSystem particleCreat;
    private GameManager gameManager;
    private Vector3 posCreatEffect;
    //public CreatTytpe creatTytpe;
    private MoneyManager moneyManager;
    private float   profitLevel;
    private int peopleLevel;
    private int profit;
    private int Showprofit;
    private Animator _animator;
    private void Awake()
    {
        transform.localScale = Vector3.zero;
        moneyManager = MoneyManager.Instance;
        gameManager = GameManager.Instance;
        //particleCreat = Instantiate(gameManager.particleCreatPeople,transform.position,quaternion.identity);
        
    }
    [HideInInspector] public  SplineController splineController;
    private void Start()
    {
        moneyManager.ProflieChange += ProflieChange;
        ProflieChange(moneyManager.ProfileLevel);
    }
    private void ProflieChange(float  obj)
    {
        profitLevel = obj;
        Showprofit = (int)((peopleLevel * 300 + 100)  * profitLevel);
        profit = (int)((peopleLevel * 300 + 50)  * profitLevel);
    }
    private void OnEnable()
    {
        InitializeInfo();
        
    }
    private void InitializeInfo()
    {
        
        
        _animator = transform.GetComponent<Animator>();
        
        
        splineController = transform.GetComponent<SplineController>();
        
        posCreatEffect = new Vector3(transform.position.x,2.5f,transform.position.z);
        //particleCreat.transform.position = posCreatEffect;
        //particleCreat.Play();
        
        transform.DOScale(1.1f, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            _animator.enabled = true;
            
            if (PlayerPrefs.GetFloat("Speed") < .8f)
            {
                _animator.speed = .8f;
                splineController.Speed = .8f;
            }
            else
            {
                _animator.speed = PlayerPrefs.GetFloat("Speed");
                splineController.Speed = PlayerPrefs.GetFloat("Speed");
            }
        });
        
    }

    private void Update()
    {
        if (PlayerPrefs.GetFloat("Speed") < .8f)
        {
            _animator.speed = .8f;
            splineController.Speed = .8f;
        }
        else
        {
            _animator.speed = PlayerPrefs.GetFloat("Speed");
            splineController.Speed = PlayerPrefs.GetFloat("Speed");
        }
    }

    public RuntimeAnimatorController Orianimator;
    public RuntimeAnimatorController Idelanimator;
    public float[] anim = new float[5] {0.2f, 0.4f, 0.6f, 0.8f, 1};
    private int i = 0;
    public bool IsMove;
    

    public  void EndPoint()
    {
        IsMove = true;
        Vector3 oriPos = transform.position;
        Quaternion oriRoatate = transform.localRotation;
        if (!StageManager.Instance.Tai2)
        {
            Debug.Log("tai2",gameObject);
            StageManager.Instance.Tai2 = true;
            splineController.enabled = false;
            transform.DORotateQuaternion(StageManager.Instance.IdelShowPos2.rotation, 1);
            transform.DOMove(StageManager.Instance.IdelShowPos2.position, 1f).OnComplete(()=>
            {
                _animator.runtimeAnimatorController = Idelanimator;
                _animator.SetLayerWeight(1,1);
                _animator.SetFloat("Blend",anim[Random.Range(0,4)]);
                Tool.Timer.Register(2f, delegate
                {
                    _animator.runtimeAnimatorController = Orianimator;
                    transform.DORotateQuaternion(oriRoatate, 1f);
                    moneyManager.AddShowMoney(Showprofit, transform.position);
                    transform.DOMove(oriPos, 1).OnComplete(() =>
                    {
                        splineController.enabled = true;
                        StageManager.Instance.Tai2 = false;
                        IsMove = false;
                    });
                });
            });
            
        }
        else
        {
            moneyManager.AddMoney(profit, transform.position);
        }
    }
    
}