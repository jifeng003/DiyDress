using System;
using System.Collections;
using System.Collections.Generic;
using FluffyUnderware.Curvy.Controllers;
using Pathfinding;
using UnityEngine;
using Random = UnityEngine.Random;

public class PeopleContainer : MonoBehaviour
{
    public int createAudiences;
    private void Awake()
    {
       
    }

    
    private void OnEnable()
    {
        createAudiences = PlayerPrefs.GetInt("AudiencesNumber");
        audience[]  Audiences = transform.GetComponentsInChildren<audience>();
        if (Audiences != null)
        {
            foreach (var VARIABLE in Audiences)
            {
                VARIABLE.gameObject.SetActive(false);
            }
        }
        Debug.Log(createAudiences);
        StartCoroutine(CreatAudiences());
    }
    
    IEnumerator CreatAudiences()
    {
        yield return 0;
        if (Data.GetCurLevel != 0 || Data.GetCurStageLevel() != 0)
        {
            Debug.Log("增加初始观众");
            GameObject Audiences = PoolManager.instance.SpawnFromPool("00");
            //Audiences.GetComponent<audience>().SpecialPeople = false;
        }
        for (int i = 1; i < createAudiences ; i++)
        {
            Debug.Log("生成第"+i);
            GameObject Audiences = PoolManager.instance.SpawnFromPool(Random.Range(1, 21 ).ToString());
            
            yield return 0;
        }
        yield break;
    }
}
