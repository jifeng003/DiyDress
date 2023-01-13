using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using FluffyUnderware.DevTools.Extensions;
using UnityEngine;

public class DecalClose : MonoBehaviour
{
    public Pen pen;
    public PanelAnim PanelAnim;
    public bool isNiukou;
    private void OnMouseDown()
    {
        if (isNiukou)
        {
            Selectcloth.Ins.SetNiukou(0);
            PanelAnim.ShowPanel();
        }
        else
        {
            pen.distory();
        }
        Debug.Log("删除");
        
    }
}
