using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LightUp : MonoBehaviour
{
    public Animator Animator;
    public ColorChange zhu;
    private void Start()
    {
        
        Animator = transform.GetComponent<Animator>();
        StartCoroutine(showLight());
    }

    public IEnumerator showLight()
    {
        do
        {
            zhu.isColorChange = false;
            zhu.Close = true;
            Animator.enabled = false;
            
            yield return new WaitForSeconds(4f);
            zhu.Open = true;
            Animator.enabled = true;
            yield return new WaitForSeconds(7f);
        } while (gameObject.activeSelf);

    }
    
}
