using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class optionPanel : BasePanel
{
    [Header("颜色选中的样式")]
    public Sprite colorselectSprite;
    [Header("颜色未选中的样式")]
    public Sprite uncolorselectSprite;
    [Header("普通选中的样式")]
    public Sprite selectSprite;
    [Header("普通未选中的样式")]
    public Sprite unselectSprite;
    
    [Header("颜色选择面板")]
    public List<GameObject> colorOptions = new List<GameObject>();
    [Header("头发颜色选择面板")]
    public List<GameObject> hairColorOptions = new List<GameObject>();    
    
    [Header("鞋子颜色选择面板")]
    public List<GameObject> shoeColorOptions = new List<GameObject>();
    
    [Header("上衣选择面板")]
    public List<GameObject> shangyiOptions = new List<GameObject>();
    [Header("下衣选择面板")]
    public List<GameObject> xiayiOptions = new List<GameObject>();
    [Header("材质选择面板")]
    public List<GameObject> caizhiOptions = new List<GameObject>();
    [Header("头发选择面板")]
    public List<GameObject> faxingOptions = new List<GameObject>();
    [Header("鞋子选择面板")]
    public List<GameObject> xieziOptions = new List<GameObject>();
    [Header("头饰选择面板")]
    public List<GameObject> toushiOptions = new List<GameObject>();
    [Header("首饰选择面板")]
    public List<GameObject> shoushiOptions = new List<GameObject>();
    [Header("纽扣选择面板")]
    public List<GameObject> niukouOptions = new List<GameObject>();
    [Header("包包选择面板")]
    public List<GameObject> baobaoOptions = new List<GameObject>();

    public titlePanel TitlePanel;
    public GameObject NiuKouTip;
    private void Awake()
    {

        colorOptions = SortOptions(TitlePanel.colorPanel.transform);
        hairColorOptions=SortOptions(TitlePanel.HaircolorPanel.transform);
        shoeColorOptions = SortOptions(TitlePanel.ShoecolorPanel.transform);
        hairColorOptions=SortOptions(TitlePanel.HaircolorPanel.transform);
        
        shangyiOptions = SortOptions(TitlePanel.optionObjs[0].transform);
        xiayiOptions  =  SortOptions(TitlePanel.optionObjs[1].transform);
        caizhiOptions =  SortOptions(TitlePanel.optionObjs[2].transform);
        faxingOptions =  SortOptions(TitlePanel.optionObjs[3].transform);
        xieziOptions  =  SortOptions(TitlePanel.optionObjs[4].transform);
        toushiOptions =  SortOptions(TitlePanel.optionObjs[5].transform);
        shoushiOptions = SortOptions(TitlePanel.optionObjs[6].transform);
        niukouOptions =  SortOptions(TitlePanel.optionObjs[7].transform);
        baobaoOptions =  SortOptions(TitlePanel.optionObjs[8].transform);
    }

    public List<GameObject> SortOptions(  Transform transform)
    {
        SetHair[] coloroption = transform.GetComponentsInChildren<SetHair>();
        List<GameObject> Option = new List<GameObject>();
        foreach (var VARIABLE in coloroption) 
        {
            Option.Add(VARIABLE.gameObject);
        }
        return Option;
    }
    
    private void Start()
    {
        SelectOption(Eselectoption.color, Selectcloth.Ins.currentColor);
        
        SelectOption(Eselectoption.shangyi, 0);
        SelectOption(Eselectoption.xiayi, Selectcloth.Ins.currentx1);
        SelectOption(Eselectoption.caizhi, 0);
        SelectOption(Eselectoption.faxing, Selectcloth.Ins.currentFaxin);
        SelectOption(Eselectoption.xiezi, Selectcloth.Ins.currentXiezi);
        SelectOption(Eselectoption.toushi, Selectcloth.Ins.currentToushi);
        SelectOption(Eselectoption.shoushi,Selectcloth.Ins.currentShoushi );
        SelectOption(Eselectoption.niukou, Selectcloth.Ins.currentNiukou);
        SelectOption(Eselectoption.baobao, Selectcloth.Ins.currentBaobao);
        SelectOption(Eselectoption.HairColor, Selectcloth.Ins.currentHairColor);
        
        SelectOption(Eselectoption.ShoeColor, Selectcloth.Ins.currentShoeColor);

        SortOption(colorOptions );
        
        SortOption(hairColorOptions);
        SortOption(shoeColorOptions);
        SortOption(shangyiOptions);
        SortOption(xiayiOptions);
        SortOption(caizhiOptions);
        SortOption(faxingOptions);
        SortOption(xieziOptions);
        SortOption(toushiOptions);
        SortOption(shoushiOptions);
        SortOption(niukouOptions);
        SortOption(baobaoOptions);
    }

    public void SortOption(List<GameObject> sortList)
    {
        int i = 0;
        foreach (var VARIABLE in sortList)
        {
            VARIABLE.GetComponent<SetHair>().Tag = i;
            i++;
        }
    }
    public void SelectOption(Eselectoption _eselectoption, int _index)
    {
        switch(_eselectoption)
        {
            case Eselectoption.color:
                for (int i = 0; i < colorOptions.Count; i++)
                {
                    if (i == _index)
                    {
                        colorOptions[i].GetComponent<Image>().sprite = colorselectSprite;
                    }
                    else
                    {
                        colorOptions[i].GetComponent<Image>().sprite = uncolorselectSprite;
                    }
                }
                break;
            case Eselectoption.HairColor:
                for (int i = 0; i < hairColorOptions.Count; i++)
                {
                    if (i == _index)
                    {
                        hairColorOptions[i].GetComponent<Image>().sprite = colorselectSprite;
                    }
                    else
                    {
                        hairColorOptions[i].GetComponent<Image>().sprite = uncolorselectSprite;
                    }
                }
                break;
            case Eselectoption.ShoeColor:
                for (int i = 0; i < shoeColorOptions.Count; i++)
                {
                    if (i == _index)
                    {
                        shoeColorOptions[i].GetComponent<Image>().sprite = colorselectSprite;
                    }
                    else
                    {
                        shoeColorOptions[i].GetComponent<Image>().sprite = uncolorselectSprite;
                    }
                }
                break;
            case Eselectoption.shangyi:
                for (int i = 0; i < shangyiOptions.Count; i++)
                {
                    if (i == _index)
                    {
                        shangyiOptions[i].GetComponent<Image>().sprite = selectSprite;
                    }
                    else
                    {
                        shangyiOptions[i].GetComponent<Image>().sprite = unselectSprite;
                    }
                }
                break;
            case Eselectoption.xiayi:
                for (int i = 0; i < xiayiOptions.Count; i++)
                {
                    if (i == _index)
                    {
                        xiayiOptions[i].GetComponent<Image>().sprite = selectSprite;
                    }
                    else
                    {
                        xiayiOptions[i].GetComponent<Image>().sprite = unselectSprite;
                    }
                }
                break;
            case Eselectoption.caizhi:
                for (int i = 0; i < caizhiOptions.Count; i++)
                {
                    if (i == _index)
                    {
                        caizhiOptions[i].GetComponent<Image>().sprite = selectSprite;
                    }
                    else
                    {
                        caizhiOptions[i].GetComponent<Image>().sprite = unselectSprite;
                    }
                }
                break;
            case Eselectoption.faxing:
                for (int i = 0; i < faxingOptions.Count; i++)
                {
                    if (i == _index)
                    {
                        faxingOptions[i].GetComponent<Image>().sprite = selectSprite;
                    }
                    else
                    {
                        faxingOptions[i].GetComponent<Image>().sprite = unselectSprite;
                    }
                }
                break;
            case Eselectoption.xiezi:
                for (int i = 0; i < xieziOptions.Count; i++)
                {
                    if (i == _index)
                    {
                        xieziOptions[i].GetComponent<Image>().sprite = selectSprite;
                    }
                    else
                    {
                        xieziOptions[i].GetComponent<Image>().sprite = unselectSprite;
                    }
                }
                break;
            case Eselectoption.toushi:
                for (int i = 0; i < toushiOptions.Count; i++)
                {
                    if (i == _index)
                    {
                        toushiOptions[i].GetComponent<Image>().sprite = selectSprite;
                    }
                    else
                    {
                        toushiOptions[i].GetComponent<Image>().sprite = unselectSprite;
                    }
                }
                break;
            case Eselectoption.shoushi:
                for (int i = 0; i < shoushiOptions.Count; i++)
                {
                    if (i == _index)
                    {
                        shoushiOptions[i].GetComponent<Image>().sprite = selectSprite;
                    }
                    else
                    {
                        shoushiOptions[i].GetComponent<Image>().sprite = unselectSprite;
                    }
                }
                break;
            case Eselectoption.niukou:
                for (int i = 0; i < niukouOptions.Count; i++)
                {
                    if (i == _index)
                    {
                        niukouOptions[i].GetComponent<Image>().sprite = selectSprite;
                    }
                    else
                    {
                        niukouOptions[i].GetComponent<Image>().sprite = unselectSprite;
                    }
                }
                break;
            case Eselectoption.baobao:
                for (int i = 0; i < baobaoOptions.Count; i++)
                {
                    if (i == _index)
                    {
                        baobaoOptions[i].GetComponent<Image>().sprite = selectSprite;
                    }
                    else
                    {
                        baobaoOptions[i].GetComponent<Image>().sprite = unselectSprite;
                    }
                }
                break;
            
            default:
                break;

        }
    }
}

public enum Eselectoption
{
    color,
    shangyi,
    xiayi,
    caizhi,
    faxing,
    xiezi,
    toushi,
    shoushi,
    niukou,
    baobao,
    HairColor,
    ShoeColor
}
