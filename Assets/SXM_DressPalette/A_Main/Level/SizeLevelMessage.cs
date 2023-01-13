using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeLevelMessage : MonoBehaviour
{
    public Showgril Showgril;
    //public List<SizeLevelDate> levelList = new List<SizeLevelDate>();
    public LevelList levelList;
    
    public SkinnedMeshRenderer[] neiyiClolor;
    public SkinnedMeshRenderer[] xieClolr;

    public titlePanel TitlePanel;

    public int level;
    private void Awake()
    {
        TitlePanel = Showgril.TitlePanel;
        
        levelList = Resources.Load<LevelList>("SizeLevelList");
        foreach (var VARIABLE in levelList.SizelevelList)
        {
            if (level == VARIABLE.LevelNumber)
            {
                ChangeColor(VARIABLE.neiyiClolor, VARIABLE.xieClolr);
                TitlePanel.OpenTitle(VARIABLE.TitleNumber);
                break;
            }
        }
    }

    private void ChangeColor(Material[]neiyi ,Material[] xie)
    {
        foreach (var VARIABLE in neiyiClolor)
        {
            VARIABLE.materials = neiyi;
        }
        foreach (var VARIABLE in xieClolr)
        {
            VARIABLE.materials = xie;
        }
    }
    private void OnEnable()
    {
        Selectcloth.Ins.CurrentShowGirl = Showgril.GetComponent<Showgril>();
    }
}
