using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager Instance;

    public static System.Action<bool> OnSwitch;//音量更新时调用

    public SFXManager sfxManager;
    public BGMManager bgmManager;

    public static bool _soundon;

    /// <summary>
    /// 声音是否打开
    /// </summary>
    public static bool SoundOn
    {
        get
        {
            return _soundon;
        }
        set {
            _soundon = value;
            int sound = value ? 1 : 0;
            PlayerPrefs.SetInt("SoundOn", sound);

            Instance.sfxManager.audiosource.volume = sound;
            Instance.bgmManager.audiosource.volume = sound;

            OnSwitch?.Invoke(value);
            
        }
    }

    private void Awake()
    {
        Instance = this;
        SoundOn = PlayerPrefs.GetInt("SoundOn", 1) == 1 ? true : false;

    }
    public static void Switch()
    {
        SoundOn = !SoundOn;
    }


    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="index"></param>
    public static void PlaySFX(int index)
    {
        Instance.sfxManager.PlayClip(index);
    }

    /// <summary>
    /// 切换bgm
    /// </summary>
    /// <param name="index"></param>
    public static void PlayBgm(int index)
    {
        Instance.bgmManager.PlayBgm(index);
    }

    /// <summary>
    /// 绑定事件
    /// </summary>
    /// <param name="actionEvent"></param>
    public static void BindEvent(System.Action<bool> actionEvent)
    {
        OnSwitch += actionEvent;
        OnSwitch?.Invoke(SoundOn);
    }
}
