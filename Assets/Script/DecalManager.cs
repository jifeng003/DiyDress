using System;
using System.Collections;
using System.Collections.Generic;
using PaintIn3D;
using UnityEngine;

public class DecalManager : Singleleton<DecalManager>
{
    public Pen[] Decals;
    public bool canInsDecal;
    
    public void Update()
    {
        Decals = transform.GetComponentsInChildren<Pen>();
        if (Decals.Length >= 1)
        {
            canInsDecal = false;
        }
        else
        {
            canInsDecal = true;
        }
    }

    public void closeDecal()
    {
        Destroy(Decals[Decals.Length - 1].gameObject);
        Decals[Decals.Length - 1].penDecal.GetComponent<P3dHitNearby>().ManuallyHitNow();
        
    }

}
