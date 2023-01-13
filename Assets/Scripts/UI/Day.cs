using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Day : MonoBehaviour
{
    Text Daytext;
    private void Start()
    {
        Daytext = this.gameObject.GetComponent<Text>();
        string path = "DAY" + (Data.GetCurLevel+1).ToString();
        Daytext.text = path;
    }

}
