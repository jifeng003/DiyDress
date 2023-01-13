using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioType
{
    BGM,
    SFX
}

public class SoundManagerBase<T> : MonoBehaviour where T : SoundManagerBase<T>
{

    protected AudioType type;
    public AudioSource audiosource;
    

    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        DontDestroyOnLoad(gameObject);
        audiosource = GetComponent<AudioSource>();

        if (!(Instance == null))
            return;
        Instance = this as T;
    }






}
