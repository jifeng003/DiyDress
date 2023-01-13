using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Timers;
using Pathfinding;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using DG.Tweening;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(DynamicGridObstacle))]
public class audience : MonoBehaviour
{
    public bool isEarnMoney = false;
    public Transform Tai;
    public Vector3 RandomPointPos;

    public bool SpecialPeople;
    
    public Vector3 node;
    public Animator animator;
    public bool first;
    private void Awake()
    {
        transform.localScale = Vector3.zero;
        Tai = StageManager.Instance.Stage;
        animator = transform.GetComponent<Animator>();
    }

    private Coroutine _coroutine;
    private void OnEnable()
    {
        first = false;
        if (!SpecialPeople)
        {
            RandomPointPos = new Vector3(Random.Range(-3, 3), 0, Random.Range(-7, 7))+Tai.position;
            node = AstarPath.active.GetNearest(RandomPointPos, NNConstraint.Default).position;
            transform.position = node;

        }
        transform.DOLookAt(Tai.position, .8f);
        transform.DOScale(Vector3.one, 1f).SetEase(Ease.InOutBack).OnComplete(() =>
        {
            isEarnMoney = true;
            _coroutine = StartCoroutine("StartEarnMoney");
        });
        
        
         
    }

    IEnumerator StartEarnMoney()
    {
        int n = 0;
        if (SpecialPeople && first && Data.GetCurLevel == 1 && Data.GetCurStageLevel() == 0)
        {
            do
            {

                MoneyManager.Instance.AddShowMoney(Random.Range(40, 50), transform.position);
                n++;

                if (Random.Range(1f, 4f) >= 3)
                {
                    animator.Play("Cheering");
                }
                else
                {
                    animator.Play("Clapping");
                }

                yield return new WaitForSeconds(5f);
            } while (n <= 5);
            
            do
            {
                MoneyManager.Instance.AddShowMoney(Random.Range(2, 8), transform.position);
                yield return new WaitForSeconds(Random.Range(5f,10f));
                if (Random.Range(1f, 4f) >=3)
                {
                    animator.Play("Cheering");
                }
                else
                {
                    animator.Play("Clapping");

                }
            } while (isEarnMoney);
        }
        else
        {
            do
            {
                MoneyManager.Instance.AddShowMoney(Random.Range(2, 8), transform.position);
                yield return new WaitForSeconds(Random.Range(5f,10f));
                if (Random.Range(1f, 4f) >=3)
                {
                    animator.Play("Cheering");
                }
                else
                {
                    animator.Play("Clapping");

                }
            } while (isEarnMoney);
        }
        
        Debug.Log("观众+钱");
        
    }

    public void OnDisable()
    {
        isEarnMoney = false;
        StopCoroutine(_coroutine);
    }

}
