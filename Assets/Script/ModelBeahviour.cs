using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.Mathematics;
using Random = UnityEngine.Random;

public class ModelBeahviour : MonoBehaviour
{
    public Animator animator;
    public PeoplePosContainer PosContainer;
    private bool isUnder;
    public bool NeedHandUp;
    private void Awake()
    {
        animator = transform.GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isUnder)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            transform.localPosition = Vector3.zero;
        }

        
    }

    private float[] anim = new float[5] {0.2f, 0.4f, 0.6f, 0.8f, 1};
    public void LeaveList()
    {
        StageManager.Instance.Tai2 = true;
        isUnder = true;
        transform.DORotateQuaternion(StageManager.Instance.IdelShowPos2.rotation, 1.8f/(PosContainer.peoplespeed*PosContainer.Speedratio));
        transform.parent = StageManager.Instance.Tai2Pos;
        // if (_coroutine != null)
        // {
        //     StopCoroutine(_coroutine);
        // }
        coroutine =StartCoroutine("EarnMoney");
        
        animator.SetLayerWeight(1,1);
        animator.SetFloat("Blend",anim[Random.Range(0,4)]);
    }

    private Coroutine coroutine;

    
    public void BackList(Transform parent)
    {
        StopCoroutine(coroutine);
        Debug.Log("离开成功");
        isEarnMoney = false;
        transform.parent = parent;
        transform.DOLocalRotate(Vector3.zero, 1f/(PosContainer.peoplespeed*PosContainer.Speedratio));
        if (NeedHandUp)
        {
            animator.runtimeAnimatorController = PosContainer.ShowBaoanimator;
        }
        else
        {
            animator.runtimeAnimatorController = PosContainer.Orianimator;
        }
        
        parent.GetComponent<PeoplePos>().model = transform.GetComponentInChildren<idelShowGirl>();
        parent.GetComponent<PeoplePos>().animator = animator;
        parent.GetComponent<PeoplePos>().modelBeahviour = transform.GetComponent<ModelBeahviour>();
        
        transform.DOLocalMove(Vector3.zero, 1.8f/(PosContainer.peoplespeed*PosContainer.Speedratio)).OnComplete(() =>
        {
            isUnder = false;
        });
        
    }

    public bool isEarnMoney;
    IEnumerator EarnMoney()
    {
        
        transform.DOMove(StageManager.Instance.IdelShowPos2.position, 1.8f/(PosContainer.peoplespeed*PosContainer.Speedratio)).OnComplete(() =>
        {
            animator.runtimeAnimatorController = PosContainer.Idelanimator;
            isEarnMoney = true;
            
            StageManager.Instance.Tai2Stand = true;
        });
        yield return new WaitForSeconds(1.8f/(PosContainer.peoplespeed*PosContainer.Speedratio));
        Debug.Log("加钱：" + profit );
        do
        {
            moneyManager.AddShowMoney(profit, transform.position);
            
            yield return new WaitForSeconds(6f);
            if (isEarnMoney)
            {
                float number = anim[Random.Range(0, 4)];
                animator.SetLayerWeight(1,1);
                animator.SetFloat("Blend",number);
                //Debug.Log("展示台model动作改变" + "动作" + number);
            }
            
            Debug.Log("还在运行",transform.gameObject);
        } while (isEarnMoney);
    }
    
    
    private int profit;
    private int Showprofit;
    private float   profitLevel;
    private MoneyManager moneyManager;

    private void Start()
    {
        moneyManager = MoneyManager.Instance;
        moneyManager.ProflieChange += ProflieChange;
        ProflieChange(moneyManager.ProfileLevel);
    }

    private void ProflieChange(float  obj)
    {
        profitLevel = obj;
        profit = (int)(80  * profitLevel);
    }
}
