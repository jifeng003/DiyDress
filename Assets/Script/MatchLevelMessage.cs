using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchLevelMessage : MonoBehaviour
{
    public Showgril Showgril;
    public titlePanel TitlePanel;
    public int[] TitleNumber;

    private void Awake()
    {
        TitlePanel = Showgril.TitlePanel;
        TitlePanel.OpenTitle(TitleNumber);
    }
    private void OnEnable()
    {
        Selectcloth.Ins.CurrentShowGirl = Showgril;
    }
}
