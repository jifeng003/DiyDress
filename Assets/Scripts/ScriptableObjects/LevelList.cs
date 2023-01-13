using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/LevelList")]
public class LevelList : ScriptableObject
{
    public List<SizeLevelDate> SizelevelList = new List<SizeLevelDate>();
}
[Serializable]
public class SizeLevelDate
{
    public int LevelNumber;
    public Material[] neiyiClolor;
    public Material[] xieClolr;
    public int[] TitleNumber;
}
