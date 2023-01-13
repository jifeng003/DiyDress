using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/MeshList")]
public class MeshList : ScriptableObject
{
    public List<mList> materialList = new List<mList>();
}

[Serializable]
public class mList
{
    public Sprite sprite;
    [Header("是否多色")]public bool isHomochromy;
    [Header("是否解锁")] public bool isUnlock;

}
