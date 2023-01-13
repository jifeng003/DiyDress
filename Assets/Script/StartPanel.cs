using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Lww;
using ZYB;
using TMPro;
using DG.Tweening;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

//using MoreMountains.NiceVibrations;

[DefaultExecutionOrder(-80)]
public class StartPanel : StartPanelComponents
{
    private MoneyManager moneyManager;
    public ClaimBanknoteEffect bankNoteEffect;
    public ClaimBanknoteEffect bankNoteEffect1;
    public ParticleSystem MoneyEff;

    
    private int tempMoney;

    
    #region buySpeed
    private int buySpeedNeedMoney;
    private const string buyLevelName = "SpeedLevel";
    private float NowSpeed;
    private const string LastSpeed = "Speed";
    private int buySpeedLevel;
    private const string buyNeedMoneyName = "buySpeedNeedMoney";
    #endregion
    
    #region income
    private int incomeLevel;
    private const string IncomeLevelName = "incomeLevel";
    private int incomeNeedMoney;
    private const string incomeNeedMoneyName = "incomeNeedMoney";
    #endregion
    
    #region audience
    private int audienceLevel;
    private const string audienceLevelName = "audienceLevel";
    private int audienceNeedMoney;
    private const string audienceNeedMoneyName = "audienceNeedMoney";

    private bool activeVib;
    private int secMoney;


    #endregion

    protected override void Awake()
    {
        base.Awake();
        InitializeInfo();
    }

    private void Start()
    {
        
        moneyManager = MoneyManager.Instance;
        moneyManager.MoneyChange += JudgeCanBuy;
        //moneyManager.SecMoneyChange += GetMoneyText;
        activeVib = true;
        ActiveVibTra.gameObject.SetActive(activeVib);
        StageManager.Instance.Obs.enabled = false;
        UnlockTip.SetActive(false);

    }

    public GameObject UnlockTip;
    public Transform UnlockObj;
    private void Update()
    {
        if (moneyManager.Money >= 200 && UnlockObj.gameObject.activeSelf)
        {
            UnlockTip.GetComponent<RectTransform>().anchoredPosition =
                GameManager.WorldToUGUI(UnlockObj.position) + new Vector2(0, 100);
            UnlockTip.SetActive(true);
        }
        else
        {
            UnlockTip.SetActive(false);
        }
        
    }

    private void InitializeInfo()
    {
        Money.text = PlayerPrefs.GetInt("Money").ToString();
        JudgeCanBuy(PlayerPrefs.GetInt("Money"));
        
        buySpeedNeedMoney = PlayerPrefs.GetInt(buyNeedMoneyName, 300);
        buySpeedLevel = PlayerPrefs.GetInt(buyLevelName, 1);
        
        BuyMoneyText.text = buySpeedNeedMoney.ToString();
        ConnectLevelText(BuyLevelText,buySpeedLevel);

        incomeLevel = PlayerPrefs.GetInt(IncomeLevelName, 1);
        incomeNeedMoney = PlayerPrefs.GetInt(incomeNeedMoneyName, 300);
        IncomeMoneyText.text = incomeNeedMoney.ToString();
        ConnectLevelText(IncomeLevelText, incomeLevel);

        audienceLevel= PlayerPrefs.GetInt(audienceLevelName,1);
        audienceNeedMoney = PlayerPrefs.GetInt(audienceNeedMoneyName, 100);
        AudienceMoneyText.text = audienceNeedMoney.ToString();
        ConnectAudienceNumberText(AudienceLevelText, audienceLevel);
        
    }

    private void ConnectLevelText(TextMeshProUGUI text,int n)
    {
        text.text = "LEVEL " + n.ToString();
    }

    private void ConnectAudienceNumberText(TextMeshProUGUI text,int n)
    {
        text.text = "NUMBER " + n.ToString();
    }
    
    private void JudgeCanBuy(int money)
    {
        StartCoroutine(DelayEvent.DelayAction(Time.deltaTime * 2, () =>
        {
            incomeButton.interactable = WhetherEnoughMoney(money,incomeNeedMoney);
            buyButton.interactable = WhetherEnoughMoney(money,buySpeedNeedMoney);
            audienceButton.interactable = WhetherEnoughMoney(money,audienceNeedMoney);
        }));
        DOTween.To(() => tempMoney, x => { tempMoney = x; Money.text = tempMoney.ToString(); }, money, 0.2f);
    }

    private List<ClaimBanknoteEffect> _effectPool;
    public void ShowEffect(Vector3 worldPosition, int value,bool isShowEFF)
    {
        if (_effectPool == null)
            _effectPool = new List<ClaimBanknoteEffect>();

        ClaimBanknoteEffect returnEffect = null;
        for (var i = 0; i < _effectPool.Count; i++)
        {
            if (!_effectPool[i].gameObject.activeInHierarchy)
            {
                returnEffect = _effectPool[i];
                break;
            }
        }
        
        if (returnEffect == null)
        {
            returnEffect = Instantiate(bankNoteEffect, transform);
            _effectPool.Add(returnEffect);
        }
       
        returnEffect.ShowEffect(worldPosition, value);
    }
    
    
    private List<ClaimBanknoteEffect> _effectShowPool;
    //private List<ParticleSystem> Moneys;
    public void ShowShowEffect(Vector3 worldPosition, int value , bool isScreen)
    {
        if (_effectShowPool == null)
            _effectShowPool = new List<ClaimBanknoteEffect>();

        ClaimBanknoteEffect returnEffect = null;
        for (var i = 0; i < _effectShowPool.Count; i++)
        {
            if (!_effectShowPool[i].gameObject.activeInHierarchy)
            {
                returnEffect = _effectShowPool[i];
                break;
            }
        }
        if (returnEffect == null)
        {
            returnEffect = Instantiate(bankNoteEffect1, transform);
            _effectShowPool.Add(returnEffect);
        }

        // ParticleSystem returnMoneys;
        // returnMoneys = Instantiate(MoneyEff, transform);
        // returnMoneys.transform.position = worldPosition + new Vector3(0,2.3f,0);
        // returnMoneys.Play();
        if (isScreen)
        {
            returnEffect.ShowEffect1(worldPosition, value);

        }
        else
        {
            returnEffect.ShowEffect(worldPosition, value);
        }
        
    }

    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="money1">现在有的钱</param>
    /// <param name="money2">需要的钱</param>
    /// <returns></returns>
    private bool WhetherEnoughMoney(int money1,int money2)
    {
        if (money1 >= money2) return true;
        else return false;
    }


    //增加速度
    //public SplineController People;
    public PeoplePosContainer peoplePos;

    

    private void OnBuySpeed()
    {
        NowSpeed = peoplePos.peoplespeed + 1/peoplePos.peoplespeed *0.1f;
        Debug.Log("按下加速按钮");
        VibratorManager.Trigger(2);
        if (moneyManager.CanBuy(buySpeedNeedMoney))
        {
            DotweenNumber(BuyMoneyText, (buySpeedNeedMoney + buySpeedLevel * buySpeedLevel*20 + 150), ref buySpeedNeedMoney);
            PlayerPrefs.SetInt(buyNeedMoneyName, buySpeedNeedMoney);
            
            buySpeedLevel++;
            PlayerPrefs.SetInt(buyLevelName,buySpeedLevel);
            ConnectLevelText(BuyLevelText, buySpeedLevel);
            
            PlayerPrefs.SetFloat(LastSpeed,NowSpeed);
            //VibratorManager.Trigger(HapticTypes.Selection);
            StageManager.Instance.ModelContainer.SpeedUp();
        }
        
    }
    //增加收入
    private void OnIncome()
    {
        VibratorManager.Trigger(2);

        if (moneyManager.CanBuy(incomeNeedMoney))
        {
            moneyManager.ProfileLevel += 0.1f;
            incomeLevel++;
            ConnectLevelText(IncomeLevelText,incomeLevel);
            PlayerPrefs.SetInt(IncomeLevelName, incomeLevel);
            CountIncomeNeedMoney();
        }
    }

    //增加观众
    private void OnAudience()
    {
        VibratorManager.Trigger(2);

        if (moneyManager.CanBuy(audienceNeedMoney))
        {
            //GameObject People = Instantiate(PeopleAI[Random.Range(0, this.PeopleAI.Count)],StageManager.Instance.PeoplePos.position, transform.rotation);
            GameObject People = PoolManager.instance.SpawnFromPool(Random.Range(1, 21).ToString());
            ParticleSystem particleCreat = Instantiate(GameManager.Instance.particleCreatPeople);
            particleCreat.transform.position = People.transform.position;
            particleCreat.Play();
            
            Data.UpAudienceNumber();
            
            //seeker.StartPath(transform.position,raycastHit.point , OnPathComplete);
            DotweenNumber(AudienceMoneyText, (audienceNeedMoney + audienceLevel *audienceLevel* audienceLevel*30 ), ref audienceNeedMoney);
            PlayerPrefs.SetInt(audienceNeedMoneyName, audienceNeedMoney);
            audienceLevel++;
            PlayerPrefs.SetInt(audienceLevelName,audienceLevel);
            ConnectAudienceNumberText(AudienceLevelText, audienceLevel);
        }
    }

    
    
    private void CountIncomeNeedMoney()
    {
        var temp = 1;
        for (int i = 0; i < incomeLevel; i++)
        {
            temp = temp * 2;
        }
        DotweenNumber(IncomeMoneyText, (incomeNeedMoney + incomeLevel * incomeLevel * 30), ref incomeNeedMoney);
        PlayerPrefs.SetInt(incomeNeedMoneyName, incomeNeedMoney);
    }

    private void  DotweenNumber(TextMeshProUGUI textmeshpro,int number,ref int n)
    {
        int m = n;
        n= DOTween.To( ()=>  m, x=> { m = x; textmeshpro.text = m.ToString(); },number,0.2f
           ).endValue;
        
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void OnVibButton()
    {
        activeVib = !activeVib;
        //VibratorManager.Switch();
        ActiveVibTra.gameObject.SetActive(activeVib);
    }

    private void GetMoneyText(float n)
    {
        //SecProfit.text = n.ToString();
        int m = (int)(n);
        secMoney = DOTween.To(() => m, x => { m = x; SecProfit.text = $"{m.ToString() } /SEC"; }, (int)(n), 0.2f
           ).endValue;
    }

    public void ChangeSpeed(float n)
    {
        DOTween.To(
            () => secMoneyTemp, x => { secMoneyTemp = x; SecProfit.text = ((int)(x)).ToString(); }, n, 1f
            ).OnComplete(
            () => { tweenSpeed?.Complete(); ResetSpeed(); }
            );
    }

    private float secMoneyTemp;
    private Tween tweenSpeed;
    private void ResetSpeed()
    {
        tweenSpeed = DOTween.To(
            () => secMoneyTemp, x => { secMoneyTemp = x; SecProfit.text = $"{((int)(x)).ToString()} /SEC"; }, secMoney, 1.5f
            );
    }

    #region BindEvent
    protected override void BindButtonEvent()
    {
        buyButton.onClick.AddListener(OnBuySpeed);
        incomeButton.onClick.AddListener(OnIncome);
        audienceButton.onClick.AddListener(OnAudience);
        vibButton.onClick.AddListener(OnVibButton);
    }
#endregion

#region Static
    public static StartPanel Panel => UIManager.GetPanel<StartPanel>();

    public static UIPanel ShowPanel(params object[] args)
    {
        return Panel.Show(args);
    }
    public static UIPanel HidePanel(object arg=null)
    {
        return Panel.Hide(arg);
    }
#endregion






}
