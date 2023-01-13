using System;
using System.Runtime.InteropServices;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif



public class VibratorManager : MonoBehaviour
{


#if UNITY_IOS
        [DllImport("__Internal")]
        private static extern void _PlayTaptic(int type);
        [DllImport("__Internal")]
        private static extern void _PlayTaptic6s(int type);
#endif

    private static System.Action<bool> _onSwitch;

    private static bool _vibratorEnable=true;


    /// <summary>
    /// Vibrator是否开启
    /// </summary>
    private static bool VibratorEnable
    {
        get =>_vibratorEnable; 
        set {
            _vibratorEnable = value;
            PlayerPrefs.SetInt("VibratorEnable", value ? 1 : 0);
            _onSwitch?.Invoke(value);
        }
    }


    private void Awake()
    {
        VibratorEnable = PlayerPrefs.GetInt("VibratorEnable",1) == 1 ? true : false;
    }

    /// <summary>
    /// 0 = Selection change   {0,25}
    /// 1 = ImpactLight  {0,50}
    /// 2 = ImpactMedium {0,75}
    /// 3 = ImpactHeavy {0,100}
    /// 4 = Success {0,200}
    /// 5 = Warning {0,300}
    /// 6 = Failure {0,300}
    /// </summary>
    /// <param name="level"></param>
    public static void Trigger(int level)
    {
        try
        {
        
            if (VibratorEnable)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            AndroidTaptic.Haptic((HapticTypes)level);
#endif
#if UNITY_IOS && !UNITY_EDITOR
               if (IsTapticEngine())
            {
               _PlayTaptic(level);
            }
            else
            {  
                _PlayTaptic6s(level);
            }
#endif

                Debug.Log("[VibratorManager]:Trigger"+level);
            }
            else
            {
                Debug.Log("[VibratorManager]:VibratorEnable==False");
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }

    }


    private static float _lastTriggerTime = 0;
    /// <summary>
    /// 在Update中调用，获得一个连续的振动效果
    /// </summary>
    /// <param name="level">振动等级,详见Tigger方法注释</param>
    /// <param name="interval">振动间隔</param>
    public static void UpdateTrigger(int level=0,float interval=0.05f)
    {
        if(Time.time-_lastTriggerTime<interval)
            return;
        Trigger(level);
        _lastTriggerTime = Time.time;
    }




    /// <summary>
    /// 设备是否支持TapticEngine
    /// </summary>
    /// <returns><c>true</c>, if taptic engine was ised, <c>false</c> otherwise.</returns>
    private static bool IsTapticEngine()
    {
        try
        {
            if (IsiPadOriPod())
                return false;
            var s = SystemInfo.deviceModel;
            int iPhoneId;
            if (s[7].Equals(','))
                iPhoneId = int.Parse(s[6].ToString());
            else
                iPhoneId = int.Parse(s[6] + "" + s[7]);
            return iPhoneId >8;
        }
        catch (Exception e)
        {
            return false;
        }
      

        //return SystemInfo.deviceModel == "iPhone8,1" || SystemInfo.deviceModel == "iPhone8,2";
    }

    /// <summary>
    /// 判断设备是否为ipad或iPod,在ipad上应该隐藏震动按钮
    /// </summary>
    /// <returns><c>true</c>, if pad was isied, <c>false</c> otherwise.</returns>
    public static bool IsiPadOriPod()
    {
        return SystemInfo.deviceModel.Contains("Pad")||SystemInfo.deviceModel.Contains("Pod");
    }

    public static void Switch()
    {
        VibratorEnable = !VibratorEnable;
    }
    /// <summary>
    /// 绑定事件
    /// </summary>
    /// <param name="actionEvent"></param>
    public static void BindEvent(System.Action<bool> actionEvent)
    {
        _onSwitch += actionEvent;
        _onSwitch?.Invoke(_vibratorEnable);
    }

}
