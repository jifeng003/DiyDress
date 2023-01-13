using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


//----------------------材质--------------------------------
[CreateAssetMenu(menuName ="Custom/MaterialList")]
public class MaterialList : ScriptableObject
{
    public Ecaizhi ecaizhi;
    public List<mrList> materialList = new List<mrList>();
}

[Serializable]
public class mrList
{
    public Material material01;
    public Material material02;
}



public enum Ecaizhi
{
    putong,
    pi,
    buliao,
    gewen,
    huawen,
    shuiwen,
    caijian,
    zhazhen,
    Tie
}