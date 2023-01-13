using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FluffyUnderware.Curvy;
using RootMotion;

public class StageManager : Singleleton<StageManager>
{
    public CurvySpline StageSpline;
    public Transform Stage;
    public Collider Obs;
    //public Transform ModelContainer;
    
    public GameObject people;
    public Transform IdelShowPos1;
    public Transform IdelShowPos2;

    public PeoplePosContainer ModelContainer;
    public bool Tai2;
    public bool Tai2Stand;
    public Transform Tai2Pos;
    
}