using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DIYStates
{
    cut,
    match,
    size,
    drag,
    tie
}
public class CutLevelMessage: MonoBehaviour
{
    public Showgril Showgril;
    public titlePanel TitlePanel;
    public int[] TitleNumber;
    public DIYStates diyStates;
    [Header("Match")] public Showgril girl1;
    public Showgril girl2;
    public int color;
    private void Awake()
    {
        switch (diyStates)
        {
            case DIYStates.cut: 
                Showgril.enabled = false;   
                break;
            case DIYStates.match:
                color = Data.GetCurLevel / 4;
                girl1.Color = color;
                girl2.Color = color;
                break;
        }
        
        TitlePanel = Showgril.TitlePanel;
        TitlePanel.OpenTitle(TitleNumber);
    }

    private void OnEnable()
    {
        Selectcloth.Ins.CurrentShowGirl = Showgril;
    }
}
