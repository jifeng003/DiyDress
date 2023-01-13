using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using RootMotion;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PeoplePosContainer : MonoBehaviour
{
    public PeoplePos[] PeoplePosList ;
    public float radius = .8f;

    public float peoplespeed;

    public int currentModelNumber;
    private JsonSave json;

    public RuntimeAnimatorController Orianimator;
    public RuntimeAnimatorController Idelanimator;
    public RuntimeAnimatorController ShowBaoanimator;

    public List<GameObject> ModelList;

    public void ClearAllModel()
    {
        foreach (var VARIABLE in ModelList)
        {
            VARIABLE.SetActive(false);
            VARIABLE.transform.parent = PoolManager.instance.parent;
        }

        PoolManager.instance.DespawnToPool("Model");
        foreach (var VARIABLE in PeoplePosList)
        {
            VARIABLE.enable = false;
        }
        ModelList.Clear();
    }
    private void Start()
    {
        json = GameManager.Instance.jsonSave;
        currentModelNumber = json.datas.Models.Count;
        peoplespeed = Data.GetSpeed();
        PeoplePosList = transform.GetComponentsInChildren<PeoplePos>(); 
        
        float i = 0;
        foreach (var VARIABLE in PeoplePosList)
        {
            VARIABLE._splineController.Position = i;
            i += VARIABLE._splineController.Length / PeoplePosList.Length;
            VARIABLE._splineController.Speed = peoplespeed;
            VARIABLE._splineController.enabled = true;
        }
        
        for (int j = currentModelNumber - 1; j >= 0; j--)
        {
            GameObject model = PoolManager.instance.SpawnFromPool("Model", PeoplePosList[j].transform);
            ModelList.Add(model);
            
            model.GetComponent<ModelBeahviour>().PosContainer = this;
            PeoplePosList[j].Ini(json.datas.Models[j]);
        }
        
    }

    public void AddPeople(Model model)
    {
        for (int i = 0; i < PeoplePosList.Length; i++)
        {
            if (PeoplePosList[i].enable == false)
            {
                GameObject Model = PoolManager.instance.SpawnFromPool("Model", PeoplePosList[i].transform);
                ParticleSystem particleCreat = Instantiate(GameManager.Instance.particleCreatModel,PeoplePosList[i].transform.position,Quaternion.identity);
                particleCreat.Play();
                Model.GetComponent<ModelBeahviour>().PosContainer = this;
                Model.transform.localScale = Vector3.zero;
                Model.transform.DOScale(Vector3.one * 1.1f, .5f);
                PeoplePosList[i].Ini(model);
                currentModelNumber += 1;
                ModelList.Add(Model);
               
                break;
            }
        }
    }

    public float Speedratio = 1;
    private void Update()
    {
        foreach (var VARIABLE in PeoplePosList)
        {
            VARIABLE._splineController.Speed = peoplespeed * Speedratio;
            if (VARIABLE.animator)
            {
                VARIABLE.animator.speed = peoplespeed * Speedratio;
            }
            
        }
    }

    public void SpeedUp()
    {
        peoplespeed = Data.GetSpeed();
    }
}
