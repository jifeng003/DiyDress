using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using FluffyUnderware.Curvy.Controllers;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

public class PeoplePos : MonoBehaviour
{
    public SplineController _splineController;
    //public Collider[] OnColliders;
    public PeoplePosContainer PosContainer;
    public idelShowGirl model;
    public ModelBeahviour modelBeahviour;
    public Animator animator;

    public bool enable = false;
    private MoneyManager moneyManager;

    public TrailRenderer Trail;
    private void Awake()
    {
        Trail = transform.GetComponent<TrailRenderer>();
        PosContainer = transform.parent.GetComponent<PeoplePosContainer>();
        
        _splineController = transform.GetComponent<SplineController>();
        _splineController.enabled = false;
        
        moneyManager = MoneyManager.Instance;
        
        _splineController.OnEndReached.AddListener(arg0 =>
        {
            EndPoint();
            //Debug.Log("执行了");
        });
    }
    public void Ini(Model modelInformation)
    {
        model = transform.GetComponentInChildren<idelShowGirl>();
        model.ShowCloth(modelInformation);
        
        animator = model.transform.GetComponent<Animator>();
        animator.speed = PosContainer.peoplespeed;
        
        modelBeahviour = model.transform.GetComponent<ModelBeahviour>();
        
        enable = true;
    }
    
    private void Update()
    {
        //Physics.OverlapSphereNonAlloc(transform.position, PosContainer.radius, OnColliders,(1 << LayerMask.NameToLayer("Tai")));
        if (!enable && model != null)
        {
            //model.gameObject.SetActive(false);
        }

        if (PosContainer.Speedratio != 1 && enable)
        {
            Trail.time = 1.4f;
            
        }else
        {
            Trail.time = Mathf.Lerp(Trail.time, 0f, .25f);
        }

        if (!enable)
        {
            Trail.enabled = false;
        }
        else
        {
            Trail.enabled = true;
        }
    }

    
    
    private int profit;
    private int Showprofit;
    private float   profitLevel;

    private void Start()
    {
        moneyManager.ProflieChange += ProflieChange;
        ProflieChange(moneyManager.ProfileLevel);
    }

    private void ProflieChange(float  obj)
    {
        profitLevel = obj;
        profit = (int)(40  * profitLevel);
    }
    
    public  void EndPoint()
    {
        if (enable)
        {
            Debug.Log("启用");
            if (!StageManager.Instance.Tai2)
            {
                Debug.Log("有位置可离开");

                LeaveList();
                
            }
            else
            {
                Debug.Log("加钱：" + profit );
                moneyManager.AddMoney(profit, transform.position);
            }
        }
        else
        {
            //Debug.Log("未启用");
            if (StageManager.Instance.Tai2 && StageManager.Instance.Tai2Stand)
            {
                //Debug.Log("有人可回归");
                BackList();
                StageManager.Instance.Tai2 = false;
                StageManager.Instance.Tai2Stand = false;
            }
        }
        
    }

    public void LeaveList()
    {
        Debug.Log("离开队伍");
        model = null;
        animator = null;
        modelBeahviour.LeaveList();
        modelBeahviour = null;
        enable = false;
        
    }
    
    
    public void BackList()
    {
        Debug.Log("返回队伍");
        StageManager.Instance.Tai2Pos.GetChild(0).GetComponent<ModelBeahviour>().BackList(transform);
        enable = true;
    }
}
