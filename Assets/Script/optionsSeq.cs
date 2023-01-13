using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class optionsSeq : MonoBehaviour
{
    
    public List<RectTransform> Transforms = new List<RectTransform>();
    [ContextMenu("Seq")] 
    void Seq()
    {
        EventTrigger[] seqs = transform.GetComponentsInChildren<EventTrigger>();
        foreach (var VARIABLE in seqs)
        {
            Transforms.Add(VARIABLE.transform.GetComponent<RectTransform>());
        }
        
    }
}
