using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class TieEventTrigr : MonoBehaviour
{
    public Action callBack;

    void Start()
    {
        EventTrigger.Entry Click = new EventTrigger.Entry ();
        Click.eventID = EventTriggerType.PointerDown;
        UnityAction<BaseEventData> click = new UnityAction<BaseEventData> (OnClick);
        Click.callback.AddListener (click);

        EventTrigger trigger = gameObject.AddComponent<EventTrigger> ();
        trigger.triggers.Add (Click);
    }

 
    public void OnClick(BaseEventData data)
    {
       callBack.Invoke();
            
    }
    
}
