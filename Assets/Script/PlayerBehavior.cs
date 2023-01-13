using FluffyUnderware.Curvy.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using FluffyUnderware.Curvy;
using System;

namespace ZYB
{
    public class PlayerBehavior : MonoBehaviour
    {

       //[HideInInspector] public    CurvySpline spline;
        //private ParticleSystem particleCreat;
        private GameManager gameManager;
        //private Vector3 posCreatEffect;
        //public CreatTytpe creatTytpe;

        private MoneyManager moneyManager;
        private float   profitLevel;
        private int peopleLevel;
        private int profit;

        private Animator _animator;
        private void Awake()
        {
            //
            // splineController = transform.GetComponent<SplineController>();
            // splineController.enabled = false;
            // gameManager = GameManager.Instance;
            // particleCreat = gameManager.particleCreatPeople;
            // posCreatEffect = new Vector3(transform.position.x,2.5f,transform.position.z);
            // moneyManager = MoneyManager.Instance;
            // _animator = transform.GetComponent<Animator>();
            //
            // if (PlayerPrefs.GetFloat("Speed") < .8f)
            // {
            //     _animator.speed = .8f;
            //     splineController.Speed = .8f;
            // }
            // else
            // {
            //     _animator.speed = PlayerPrefs.GetFloat("Speed");
            //     splineController.Speed = PlayerPrefs.GetFloat("Speed");
            // }
            //
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
            profit = (int)((peopleLevel * 300 + 100)  * profitLevel);
        }


        private void OnEnable()
        {
            InitializeInfo();
            
        }

        private void InitializeInfo()
        {
            moneyManager = MoneyManager.Instance;
            gameManager = GameManager.Instance;
            //particleCreat = Instantiate(gameManager.particleCreatPeople);
            
            _animator = transform.GetComponent<Animator>();
            
            
            splineController = transform.GetComponent<SplineController>();
            
            //posCreatEffect = new Vector3(transform.position.x,2.5f,transform.position.z);
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
            
            //Invoke(nameof(SetGetMoney),Time.deltaTime*3);
        }


        public  void ReachControllerPoint()
        {
            moneyManager.AddMoney(profit, transform.position);
        }


    }
}