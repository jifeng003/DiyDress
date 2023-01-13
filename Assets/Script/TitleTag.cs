using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class TitleTag : MonoBehaviour
{
    void Start()
    {
        EventTrigger.Entry Click = new EventTrigger.Entry ();
        Click.eventID = EventTriggerType.PointerClick;
        UnityAction<BaseEventData> click = new UnityAction<BaseEventData> (OnClick);
        Click.callback.AddListener (click);
 
        EventTrigger trigger = gameObject.AddComponent<EventTrigger> ();
        trigger.triggers.Add (Click);
    }
    public int Tag;

    public void OnClick(BaseEventData data)
    {
        Debug.Log("click");

        transform.parent.GetComponent<titlePanel>().SetSelecttitle(Tag);
    }
}
