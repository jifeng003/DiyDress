using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdelPartIni : MonoBehaviour
{
    //public GameObject ModelShowPanel;
    public GameObject Model;
    private void OnEnable()
    {
        //ModelShowPanel.SetActive(true);
        Model.SetActive(true);
    }
}
