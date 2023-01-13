using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleleton<T> : MonoBehaviour where T:Singleleton<T>
{
    public static T _instance;

    public static T Instance
    {
        get
        {
            return _instance;
            
        }
    }

    protected virtual void Awake()
    {
        if(_instance!=null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = (T)this;
        }
    }

    protected virtual void  OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }

    public  static bool IsInitialized
    {
        get
        {
            return _instance != null;
        }
    }
}
