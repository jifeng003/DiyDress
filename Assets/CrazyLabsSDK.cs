using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tabtale.TTPlugins;

[DefaultExecutionOrder(-200)]
public class CrazyLabsSDK : MonoBehaviour
{
    private void Awake()
    {
        // Initialize CLIK Plugin   
        TTPCore.Setup(); //放在首行
        // Your code here
    }
}
