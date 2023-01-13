using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data
{
    public const string TAG_WALL = "Wall";
    public const string TAG_ENEMY = "Enemy";
    public const string TAG_MULTI = "Multi";//多乘
    public const string TAG_PLAYER = "Player"; //Enemy
    public const string TAG_UNTAGGED = "Untagged";//未定义

    public const string LAYER_PLAYER = "Player";
    public const string LAYER_ENEMY = "Enemy";
    

    private const string VIB = "VIB";
    private const string COIN = "COIN";
    
    private const string CUR_LEVEL = "CUR_LEVEL";
    private const int INIT_LEVEL = 0;
    

    /// <summary>
    /// 获取金币
    /// </summary>
    public static int GetCoin
    {
        get
        {
            return PlayerPrefs.GetInt(COIN, 100);
        }
    }
    
    /// <summary>
    /// 更新金币数量
    /// </summary>
    /// <param name="num"></param>
    /// <returns>
    /// <c>false则存储失败</c>
    /// <c>true则更新成功</c>
    /// </returns>
    public static bool UpdateCoin(int num)
    {
        int count = GetCoin;

        if (num < 0 && (count + num) < 0)
            return false;
        count += num;
        PlayerPrefs.SetInt(COIN,count);
        return true;
    }
    
    
    /// <summary>
    /// 当前震动状态
    /// </summary>
    public static bool IsVibOn
    {
        get
        {
            return PlayerPrefs.GetInt(VIB, 1) == 1 ? true : false;
        }
    }

    /// <summary>
    /// 是否开启震动
    /// </summary>
    /// <param name="isOpen"></param>
    public static void SetVib(bool isOpen)
    {
        PlayerPrefs.SetInt(VIB,isOpen?1:0);
    }

    /// <summary>
    /// 获取当前关卡
    /// </summary>
    public static int GetCurLevel
    {
        get
        {
            return PlayerPrefs.GetInt(CUR_LEVEL, INIT_LEVEL);
        }
    }
    public static void SetCurLevel(int number)
    {
        PlayerPrefs.SetInt(CUR_LEVEL,number);
    }
    public static void UpLevel()
    {
        int level = GetCurLevel;
        level++;
        
        PlayerPrefs.SetInt(CUR_LEVEL, level);
    }
    
    
    public static int AudienceNumber
    {
        get
        {
            return PlayerPrefs.GetInt("AudiencesNumber");
        }
    }
    public static void UpAudienceNumber()
    {
        int Number = AudienceNumber;
        Number++;
        PlayerPrefs.SetInt("AudiencesNumber", Number);
    }
    
    public static float GetSpeed()
    {
        
        if (PlayerPrefs.GetFloat("Speed") < .8f)
        {
            SetSpeed(.8f);
        }
        return PlayerPrefs.GetFloat("Speed");
    }
    
    
    public static void SetSpeed(float Number)
    {
        
        PlayerPrefs.SetFloat("Speed", Number);
    }

    ///<summary>
    /// 检查是否已经解锁
    /// 0 - false : 锁了
    /// 1 - true : 解锁了
    /// </summary>
    public static bool GetObjLock(int Tag)
    {
        int Lock;
        if (PlayerPrefs.GetInt("ObjLock"+ Tag.ToString()) == null)
        {
            PlayerPrefs.SetInt("ObjLock"+ Tag,0);
        }

        Lock = PlayerPrefs.GetInt("ObjLock" + Tag);
        if ( Lock == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
        
    }
    /// <summary>
    /// 解锁物品
    /// </summary>
    public static void GetObjUnlock(int Tag)
    {
        PlayerPrefs.SetInt(("ObjLock"+ Tag.ToString()),1);
    }
   
    /// <summary>
    /// 读取目前解锁第几组盒子
    /// </summary>
    public static int GetUnlockObjUnmber()
    {
        if (PlayerPrefs.GetInt("Boxgroup") == null)
        {
            PlayerPrefs.SetInt("Boxgroup",0);
        }
        return PlayerPrefs.GetInt("Boxgroup");
    }
    
    /// <summary>
    /// 升级目前解锁盒子
    /// </summary>
    public static void SetUnlockObjUnmber()
    {
        int number = PlayerPrefs.GetInt("Boxgroup");
        number++;
        PlayerPrefs.SetInt("Boxgroup",number);
    }
    
    /// <summary>
    /// 读取目前舞台更新次数
    /// </summary>
    public static int GetCurStageLevel()
    {
        if (PlayerPrefs.GetInt("StageLevel") == null)
        {
            PlayerPrefs.SetInt("StageLevel",0);
        }
        return PlayerPrefs.GetInt("StageLevel");
    }
    
    /// <summary>
    /// 升级目前舞台更新次数
    /// </summary>
    public static void UpCurStageLevel()
    {
        PlayerPrefs.SetInt("StageLevel",PlayerPrefs.GetInt("StageLevel") + 1);
    }
}
