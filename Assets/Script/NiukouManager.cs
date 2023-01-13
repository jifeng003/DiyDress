using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NiukouManager : MonoBehaviour
{
    public ButtonTest[] ButtonTest;
    public DecalClose[] DecalCloses;
    public ButtonMove[] ButtonMoves;


    public Camera uicamera;
    public PanelAnim PanelAnim;
    public titlePanel TitlePanel;
    private void Awake()
    {
        TitlePanel = transform.parent.GetComponent<Showgril>().TitlePanel;
        foreach (var VARIABLE in ButtonTest)
        {
            VARIABLE.uiCamera = uicamera;
            VARIABLE.TitlePanel = TitlePanel;
            VARIABLE.PanelAnim = PanelAnim;
        }
        foreach (var VARIABLE in DecalCloses)
        {
            VARIABLE.PanelAnim = PanelAnim;
            VARIABLE.isNiukou = true;
        }

        for (int i = 0; i < ButtonTest.Length; i++)
        {
            ButtonMoves[i].ButtonTest = ButtonTest[i];
        }
    }
}
