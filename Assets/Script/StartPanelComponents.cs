using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Lww;
using TMPro;

//注意：该脚本会在执行'生成或更新UIPanel脚本'时被更新覆盖
public class StartPanelComponents : UIPanel, IUIPanelComponents
{

    private TextMeshProUGUI secProfit;
    protected TextMeshProUGUI SecProfit
    {
        get
        {
            if (!secProfit)
            {
                secProfit = transform.Find("SecProfit/profitText").GetComponent<TextMeshProUGUI>();
            }
            return secProfit;
        } set { secProfit = value; }
    }

    private TextMeshProUGUI money;
    protected TextMeshProUGUI Money
    {
        get { if (!money) { money = transform.Find("Money/MoneyText").GetComponent<TextMeshProUGUI>(); } return money; }
        set { money = value; }
    }

    private TextMeshProUGUI mergeMoneyText;
    protected TextMeshProUGUI MergeMoneyText {
        get { if (!mergeMoneyText) { mergeMoneyText = transform.Find("MergeButton/NeedMoney").GetComponent<TextMeshProUGUI>(); } return mergeMoneyText; }
        set { mergeMoneyText = value; }
    }

    private TextMeshProUGUI mergeLevelText;
    protected TextMeshProUGUI MergeLevelText
    {
        get { if (!mergeLevelText) { mergeLevelText = transform.Find("MergeButton/Level").GetComponent<TextMeshProUGUI>(); } return mergeLevelText; }
        set { mergeLevelText = value; }
    }

    private TextMeshProUGUI buyLevelText;
    protected TextMeshProUGUI BuyLevelText
    {
        get { if(!buyLevelText) { buyLevelText = transform.Find("BuyButton/Level").GetComponent<TextMeshProUGUI>(); }return buyLevelText; }
        set { buyLevelText = value; }
    }

    private TextMeshProUGUI buyMoneyText;
    protected TextMeshProUGUI BuyMoneyText
    {
        get { if (!buyMoneyText) { buyMoneyText = transform.Find("BuyButton/NeedMoney").GetComponent<TextMeshProUGUI>(); }return buyMoneyText; }
        set { buyMoneyText = value; }
    }

    private TextMeshProUGUI incomeMoneyText;
    protected TextMeshProUGUI IncomeMoneyText
    {
        get { if (!incomeMoneyText) { incomeMoneyText = transform.Find("IncomeButton/NeedMoney").GetComponent<TextMeshProUGUI>(); } return incomeMoneyText; }
        set { incomeMoneyText = value; }
    }

    private TextMeshProUGUI incomeLevelText;
    protected TextMeshProUGUI IncomeLevelText
    {
        get { if (!incomeLevelText) { incomeLevelText = transform.Find("IncomeButton/Level").GetComponent<TextMeshProUGUI>(); } return incomeLevelText; }
        set { incomeLevelText = value; }
    }

    private TextMeshProUGUI audienceMoneyText;
    protected TextMeshProUGUI AudienceMoneyText
    {
        get { if (!audienceMoneyText) { audienceMoneyText = transform.Find("AudienceButton/NeedMoney").GetComponent<TextMeshProUGUI>(); } return audienceMoneyText; }
        set { audienceMoneyText = value; }
    }

    private TextMeshProUGUI audienceLevelText;
    protected TextMeshProUGUI AudienceLevelText
    {
        get { if (!audienceLevelText) { audienceLevelText = transform.Find("AudienceButton/Level").GetComponent<TextMeshProUGUI>(); } return audienceLevelText; }
        set { audienceLevelText = value; }
    }

    private Transform activeVibTra;
    protected Transform ActiveVibTra
    {
        get
        {
            activeVibTra = transform.Find("VibButton/VibActive");
            return activeVibTra;
        }
        set
        {
            activeVibTra = value;
        }
    }

    protected Button buyButton;
    protected Button incomeButton;
    protected Button audienceButton;
    protected Button vibButton;

    protected override void Awake()
    {
        base.Awake();
        //mergeButton = transform.Find("MergeButton").GetComponent<Button>();
        buyButton = transform.Find("BuyButton").GetComponent<Button>();
        incomeButton = transform.Find("IncomeButton").GetComponent<Button>();
        audienceButton = transform.Find("AudienceButton").GetComponent<Button>();
        vibButton = transform.Find("VibButton").GetComponent<Button>();
        BindButtonEvent();
    }

}
