using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayEventHandler : MonoBehaviour
{
    public delegate void OnTimeOverEventHandler();
    private OnTimeOverEventHandler onTimerOver;

    private float timer = 0;
    private bool start = false;

    public void Delay(float timer_, OnTimeOverEventHandler ontimerOverEvent)
    {
        timer = timer_;
        onTimerOver = ontimerOverEvent;
        if (timer > 0)
        {
            start = true;
        }
    }

    private void Update()
    {
        if (start && timer > 0 && onTimerOver != null)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                start = false;
                onTimerOver();
            }
        }
    }
}
