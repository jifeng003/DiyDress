using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-19)]
public class MoneyManager : Singleleton<MoneyManager>
{
    private int money;
    private const string moneyName = "Money";

    public System.Action<int> MoneyChange;
    public System.Action<float > ProflieChange;
    private  float profileLevel;
    private const string profileName = "proflie";

    private  int getMoney;

    //public System.Action<float>  SecMoneyChange;

    // public int GetMoney
    // {
    //     get { return getMoney; }
    //     set { getMoney = value; SecMoneyChange?.Invoke(getMoney/7); }
    // }

    public float ProfileLevel
    {
        get { return profileLevel; }
        set { profileLevel = value; ProflieChange?.Invoke(profileLevel);PlayerPrefs.SetFloat(profileName,profileLevel); }
        
    }
    
    // public void ChangeGetMoney(int n)
    // {
    //     GetMoney = getMoney + n;
    // }

    protected override void Awake()
    {
        base.Awake();
        Money = PlayerPrefs.GetInt(moneyName, 0);//初始有50金币
        ProfileLevel = PlayerPrefs.GetFloat(profileName, 1);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Money += 500;
        }
    }

    public int Money
    {
        get { return money; }
        set { money = value;PlayerPrefs.SetInt(moneyName,money); MoneyChange?.Invoke(money); }
    }

    public bool CanBuy(int n)
    {
        if (n <= Money)
        {
            Money -= n;
            return true;
        }
        else return false;
    }

    public void AddMoney(int n,Vector3 pos)
    {
        Money += n;
        StartPanel.Panel.ShowEffect(pos,n,false);
    }
    
    public void AddShowMoney(int n,Vector3 pos)
    {
        Money += n;
        StartPanel.Panel.ShowShowEffect(pos,n,false);
    }

    public void RewardMoney(int n)
    {
        Money += n;
        FlyCoin();
    }
    private void FlyCoin()
    {

    }



}