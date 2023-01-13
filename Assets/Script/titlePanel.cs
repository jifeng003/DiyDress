using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class titlePanel : BasePanel
{
    public Eselecttitle eselecttitle;
    //public bool isEpic;
    [Header("选中的样式")]
    public Sprite selectSprite;
    [Header("未选中的样式")]
    public Sprite unselectSprite;
    [Header("标题")]
    public List<Image> titleImages = new List<Image>();
    [Header("选择面板")]
    public List<GameObject> optionObjs = new List<GameObject>();
    
    [Header("衣服颜色面板")]
    public GameObject colorPanel;
    
    [Header("鞋子颜色面板")]
    public GameObject ShoecolorPanel;
    
    [Header("头发颜色面板")]
    public GameObject HaircolorPanel;
    
    [Header("颜色标题")]
    public GameObject ColorTitle;

    [Header("双排物品 不显示标题")]
    public bool ColorOFF;

    
    public TitleTag[] TitleEvents;
    private List<RectTransform> Titles = new List<RectTransform>();
    
    public List<RectTransform> getTiltle()
    {
        
        List<RectTransform> Titles = new List<RectTransform>();
        
        TitleEvents = transform.GetComponentsInChildren<TitleTag>();
        //transform.parent.parent.GetComponent()
        foreach (var VARIABLE in TitleEvents)
        {
            Titles.Add(VARIABLE.transform.GetComponent<RectTransform>());
        }
        return Titles;
    }
    
    public void OpenTitle(int[] Number)
    {
        foreach (var VARIABLE in Number)
        {
            transform.GetChild(VARIABLE).gameObject.SetActive(true);
        }

        titleSeq(getTiltle());
    }
    public void titleSeq(List<RectTransform> Titles)
    {
        //奇数
        if(Titles.Count==3)
        {
            int Number = 330 / Titles.Count;
            Titles[0].anchoredPosition = new Vector2(-Number, Titles[(Titles.Count) / 2].anchoredPosition.y);
            Titles[1].anchoredPosition = new Vector2(0,Titles[(Titles.Count) / 2].anchoredPosition.y);
            Titles[2].anchoredPosition = new Vector2(+Number,Titles[(Titles.Count) / 2].anchoredPosition.y);
        }
        else if (Titles.Count == 2)
        {
            int Number = 220 / Titles.Count;

            Titles[0].anchoredPosition = new Vector2(-Number/2, Titles[(Titles.Count) / 2].anchoredPosition.y);
            Titles[1].anchoredPosition = new Vector2(Number/2,Titles[(Titles.Count) / 2].anchoredPosition.y);
        }
        else if (Titles.Count == 5)
        {
            int Number = 550 / Titles.Count;

            Titles[0].anchoredPosition = new Vector2(-2*Number, Titles[(Titles.Count) / 2].anchoredPosition.y);
            Titles[1].anchoredPosition = new Vector2(-Number, Titles[(Titles.Count) / 2].anchoredPosition.y);
            Titles[2].anchoredPosition = new Vector2(0,Titles[(Titles.Count) / 2].anchoredPosition.y);
            Titles[3].anchoredPosition = new Vector2(+Number,Titles[(Titles.Count) / 2].anchoredPosition.y);
            Titles[4].anchoredPosition = new Vector2(2*Number,Titles[(Titles.Count) / 2].anchoredPosition.y);
        }else if(Titles.Count == 4)
        {
            int Number = 440 / Titles.Count;

            Titles[0].anchoredPosition = new Vector2(-Number*1.5f, Titles[(Titles.Count) / 2].anchoredPosition.y);
            Titles[1].anchoredPosition = new Vector2(-Number/2, Titles[(Titles.Count) / 2].anchoredPosition.y);
            Titles[2].anchoredPosition = new Vector2(Number/2,Titles[(Titles.Count) / 2].anchoredPosition.y);
            Titles[3].anchoredPosition = new Vector2(Number*1.5f,Titles[(Titles.Count) / 2].anchoredPosition.y);
        }else if(Titles.Count == 6)
        {
            int Number = 660 / Titles.Count;

            Titles[0].anchoredPosition = new Vector2(-Number*2.5f, Titles[(Titles.Count) / 2].anchoredPosition.y);
            Titles[1].anchoredPosition = new Vector2(-Number*1.5f, Titles[(Titles.Count) / 2].anchoredPosition.y);
            Titles[2].anchoredPosition = new Vector2(-Number/2,Titles[(Titles.Count) / 2].anchoredPosition.y);
            Titles[3].anchoredPosition = new Vector2(Number/2,Titles[(Titles.Count) / 2].anchoredPosition.y);
            Titles[4].anchoredPosition = new Vector2(Number*1.5f,Titles[(Titles.Count) / 2].anchoredPosition.y);
            Titles[5].anchoredPosition = new Vector2(Number*2.5f,Titles[(Titles.Count) / 2].anchoredPosition.y);
        }
        
    }
    private void Start()
    {
        SetSelecttitle(TitleEvents[0].Tag);
    }
    public Transform CameraOriPos;
    public Transform HeadCamera;
    public Transform FootCamera;
    public  GameObject niukouTips;
    public void SetSelecttitle(int _eselecttitle)
    {
        VibratorManager.Trigger(1);
        eselecttitle = (Eselecttitle)_eselecttitle;
        
        if (_eselecttitle == 0||_eselecttitle == 1||_eselecttitle == 2||_eselecttitle == 8||_eselecttitle == 7)
        {
            Camera.main.transform.DOMove(CameraOriPos.position,1f);
            Camera.main.transform.DORotateQuaternion(CameraOriPos.rotation, 1f);

        }else if (_eselecttitle == 4)
        {
            Camera.main.transform.DOMove(FootCamera.position,1f);
            Camera.main.transform.DORotateQuaternion(FootCamera.rotation, 1f);

        }
        else
        {
            Camera.main.transform.DOMove(HeadCamera.position,1f);
            Camera.main.transform.DORotateQuaternion(HeadCamera.rotation, 1f);

        }
        
        switch(eselecttitle)
        {
            case Eselecttitle.shangyi:
                HaircolorPanel.SetActive(false);
                ShoecolorPanel.SetActive(false);
                SelectTitle(Eselecttitle.shangyi);
                
                if (!ColorOFF)
                {
                    colorPanel.SetActive(true);
                    ColorTitle.SetActive(true);
                }else
                {
                    colorPanel.SetActive(false);
                    ColorTitle.SetActive(false);
                }
                niukouTips.SetActive(false);

                break;
            case Eselecttitle.xiayi:
                ShoecolorPanel.SetActive(false);

                HaircolorPanel.SetActive(false);
                
                if (!ColorOFF)
                {
                    colorPanel.SetActive(true);
                    ColorTitle.SetActive(true);
                }else
                {
                    colorPanel.SetActive(false);
                    ColorTitle.SetActive(false);
                }                
                niukouTips.SetActive(false);

                SelectTitle(Eselecttitle.xiayi);
                break;
            case Eselecttitle.caizhi:
                HaircolorPanel.SetActive(false);
                ShoecolorPanel.SetActive(false);

                if (!ColorOFF)
                {
                    ColorTitle.SetActive(true);
                    colorPanel.SetActive(true);
                    
                }else
                {
                    colorPanel.SetActive(false);
                    ColorTitle.SetActive(false);
                }
                niukouTips.SetActive(false);

                SelectTitle(Eselecttitle.caizhi);
                break;
            case Eselecttitle.faxing:
                HaircolorPanel.SetActive(true);
                ShoecolorPanel.SetActive(false);
 
                ColorTitle.SetActive(true);
                colorPanel.SetActive(false);
                niukouTips.SetActive(false);

                SelectTitle(Eselecttitle.faxing);
                break;
            case Eselecttitle.xiezi:
                HaircolorPanel.SetActive(false);
                colorPanel.SetActive(false);

                if (!ColorOFF)
                {
                    ColorTitle.SetActive(true);
                    ShoecolorPanel.SetActive(true);

                }
                else
                {
                    ShoecolorPanel.SetActive(false);
                    ColorTitle.SetActive(false);
                }
                niukouTips.SetActive(false);

                SelectTitle(Eselecttitle.xiezi);
                break;
            case Eselecttitle.toushi:
                HaircolorPanel.SetActive(false);
                ColorTitle.SetActive(false);
                colorPanel.SetActive(false);
                niukouTips.SetActive(false);
                ShoecolorPanel.SetActive(false);

                SelectTitle(Eselecttitle.toushi);
                break;
            case Eselecttitle.niukou:
                HaircolorPanel.SetActive(false);
                ColorTitle.SetActive(false);
                colorPanel.SetActive(false);
                ShoecolorPanel.SetActive(false);

                niukouTips.SetActive(true);

                SelectTitle(Eselecttitle.niukou);
                break;
            case Eselecttitle.shoushi:
                HaircolorPanel.SetActive(false);
                ColorTitle.SetActive(false);
                colorPanel.SetActive(false);
                niukouTips.SetActive(false);
                ShoecolorPanel.SetActive(false);

                SelectTitle(Eselecttitle.shoushi);
                break;
            case Eselecttitle.baobao:
                HaircolorPanel.SetActive(false);
                ColorTitle.SetActive(false);
                colorPanel.SetActive(false);
                niukouTips.SetActive(false);
                ShoecolorPanel.SetActive(false);

                SelectTitle(Eselecttitle.baobao);
                break;
            default:
                break;
        }
    }


    //通过枚举值显示对应的图片以及是否隐藏对应的选择面板
    public void SelectTitle(Eselecttitle _eselecttitle)
    {
        
        for(int i=0;i<titleImages.Count;i++)
        {
            if(i==(int)_eselecttitle)
            {
                optionObjs[i].SetActive(true);
                titleImages[i].sprite = selectSprite;
            }
            else
            {
                optionObjs[i].SetActive(false);
                titleImages[i].sprite = unselectSprite;
            }
        }
    }

}

public enum Eselecttitle
{
    shangyi,
    xiayi,
    caizhi,
    faxing,
    xiezi,
    toushi,
    shoushi,
    niukou,
    baobao
}
