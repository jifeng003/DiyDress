using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Lock : MonoBehaviour
{
    public TextMeshProUGUI Text;

    public void TextReflash(int text)
    {
        
        Text.text = (text+1).ToString("D2");
    }
}
