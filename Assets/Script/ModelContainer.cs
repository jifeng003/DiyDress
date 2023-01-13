using System;
using System.Collections;
using System.Collections.Generic;
using FluffyUnderware.Curvy.Controllers;
using UnityEngine;

public class ModelContainer : MonoBehaviour
{
    public List<GameObject> Models = new List<GameObject>();
    void OnEnable()
    {
        if (checkUnderModelSpline() != null)
        {
            for (int i = checkUnderModelSpline().Length-1; i >= 0; i--)
            {
                Destroy(checkUnderModelSpline()[i].gameObject);
            }
        }
        Models.Clear();
        ModelsNumber = GameManager.Instance.jsonSave.datas.Models.Count;
        StartCoroutine(InsModels());
        
    }
    public SplineController[] checkUnderModelSpline()
    {
        return  transform.GetComponentsInChildren<SplineController>();
    }

    IEnumerator InsModels()
    {
        int Modelsnum = ModelsNumber;
        for (int i = 0; i < Modelsnum; i++)
        {
            GameObject Model = Instantiate(StageManager.Instance.people);
            //Model.transform.parent = StageManager.Instance.ModelContainer;
            Model.GetComponent<SplineController>().Spline = StageManager.Instance.StageSpline;
            Model.GetComponent<idelShowGirl>().ShowCloth(GameManager.Instance.jsonSave.datas.Models[i]);
            Models.Add(Model);
            yield return new WaitForSeconds(2f);
            ModelsNumber--;
        }
    }

    public int ModelsNumber;
    private void OnDestroy()
    {
        PlayerPrefs.SetInt("ModelsNumber",Models.Count);
    }
}
