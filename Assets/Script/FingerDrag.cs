using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using ToolBuddy.ThirdParty.VectorGraphics;
using UnityEngine;

public class FingerDrag : MonoBehaviour
{
    public Vector2 TargetPos;
    public Transform StartPos;
    public int levelnumber;
    private Tween tween;
    private void Awake()
    {
        if (levelnumber == Data.GetCurLevel + 1)
        {
            gameObject.SetActive(true);
            transform.position = StartPos.position;
            tween  =  transform.DOLocalMove(TargetPos,2f).SetLoops(-1, LoopType.Restart).SetEase(Ease.InOutQuint);
            Animator animator = transform.GetComponent<Animator>();
            animator.enabled = true;
        }
        else
        {
            gameObject.SetActive(false);

        }

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gameObject.SetActive(false);
        }
    }
}
