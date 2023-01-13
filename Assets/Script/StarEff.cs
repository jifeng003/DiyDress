using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class StarEff : MonoBehaviour
{
    public GameObject Star;
    public bool not;
    private void Update()
    {
        not = false;
        MeshRenderer[] child = transform.GetComponentsInChildren<MeshRenderer>();
        foreach (var VARIABLE in child)
        {
            if (VARIABLE.gameObject.activeSelf && Star == null)
            {
                Star = Instantiate(GameManager.Instance.star, VARIABLE.transform.position,quaternion.identity,transform);
                //Star.transform.localPosition = Vector3.zero;
            }
            if (VARIABLE.gameObject.activeSelf)
            {
                not = true;
            }
        }

        if (not == false)
        {
            if (Star)
            {
                Destroy(Star);
                Star = null;
            }
        }
    }
}
