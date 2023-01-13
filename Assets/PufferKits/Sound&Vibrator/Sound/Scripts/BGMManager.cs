using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 背景音乐控制器
/// </summary>
public class BGMManager : SoundManagerBase<BGMManager>
{

    public List<AudioClip> bgm_clip_list;

    protected override void Awake()
    {
        base.Awake();
        type = AudioType.BGM;
        audiosource.playOnAwake = true;
        audiosource.loop = true;
        PlayBgm(0);

    }


    public void PlayBgm(int index)
    {
        if (bgm_clip_list != null && bgm_clip_list.Count > index)
        {
            audiosource.clip = bgm_clip_list[index];
            audiosource.Play();
            Debug.Log("AudioManager:PlayBgm" +bgm_clip_list[index].name);
        }
    }
    /// <summary>
    /// 按照索引播放背景音乐
    /// </summary>
    /// <param name="index"></param>
    public static void PlayBgmClip(int index)
    {
        if (Instance != null)
        {
            Instance.PlayBgm(index);
        }
    }
}
