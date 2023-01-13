using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DelayEvent
{
    public static IEnumerator DelayAction(float seconds, System.Action delayAction)
    {
        yield return new WaitForSeconds(seconds);
        delayAction?.Invoke();
    }


    /// <summary>
    /// 延迟执行，会抵消之前的事件,使用Update驱动
    /// </summary>
    /// <param name="behaviour"></param>
    /// <param name="timer"></param>
    /// <param name="onTimerOverHandler"></param>
    /// <returns></returns>
    public static MonoBehaviour Delay(this MonoBehaviour behaviour, float timer, DelayEventHandler.OnTimeOverEventHandler onTimerOverHandler)
    {
        behaviour.GetDelayEventHandler().Delay(timer, onTimerOverHandler);
        return behaviour;
    }

    public static DelayEventHandler GetDelayEventHandler(this MonoBehaviour behaviour)
    {
        DelayEventHandler delayEventHandler = behaviour.GetComponent<DelayEventHandler>();
        if (delayEventHandler == null)
        {
            delayEventHandler = behaviour.gameObject.AddComponent<DelayEventHandler>();
        }
        return delayEventHandler;
    }
}
