using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 音效控制器
/// </summary>
public class SFXManager : SoundManagerBase<SFXManager>
{
    //public static SFXManager Instance { get; private set; }
    public List<AudioClip> clip_list;
    protected override void Awake()
    {
        base.Awake();
        type = AudioType.SFX;
        audiosource.playOnAwake = false;
        audiosource.loop = false;
    }


    public void PlayClip(int index)
    {
        if (clip_list != null && clip_list.Count > index)
        {
            audiosource.PlayOneShot(clip_list[index]);
            Debug.Log("AudioManager:PlaySFX" + clip_list[index].name);
        }
    }
    /// <summary>
    /// 按索引播放音效
    /// </summary>
    /// <param name="i"></param>
    public static void Play(int index)
    {
        if (Instance != null)
        {
            Instance.PlayClip(index);
        }
    }
}
