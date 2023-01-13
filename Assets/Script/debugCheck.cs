using System.Collections;
using System.Collections.Generic;
using FluffyUnderware.Curvy.Controllers;
using UnityEngine;

public class debugCheck : MonoBehaviour
{
    public void checkStart()
    {
        Debug.Log("Start");
    }
    public void checkEnd()
    {
        Debug.Log("End");
        transform.GetComponent<SplineController>().Speed += 1;
    }
}
