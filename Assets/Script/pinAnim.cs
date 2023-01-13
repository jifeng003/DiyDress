using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class pinAnim : MonoBehaviour
{
    public Transform TargetTransform;

    public void AnimPlay()
    {
        transform.DOMove(TargetTransform.position, .8f).OnComplete(() =>
        {
            transform.DORotateQuaternion(TargetTransform.rotation, .8f).OnComplete(() =>
            {
                transform.GetChild(0).GetComponent<ParticleSystem>().Play();
            });
        });
        

    }
}
