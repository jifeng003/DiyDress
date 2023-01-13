using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class LevelTest : MonoBehaviour
{
    public List<level> Levels = new List<level>();
    public int currentNum;
    public int nextSpecialNum;
    public RectTransform Slide;
    public Slider SlideBar;
    public int pointnum;
    private bool find;


    public Sprite defaultImg;
    public Sprite SpecialImg;
    public Sprite StartImg;
    public Sprite FinishImg;
    public List<GameObject> levelui = new List<GameObject>();

    public Transform LevelUIparent;
    public Vector2 levelUIsize = new Vector2(70,70);
    public Vector2 SpecialUIsize= new Vector2(80,80);

    public Font font;
    private void Awake()
    {
        SlideBar = Slide.GetComponent<Slider>();
        int i = 1;
        foreach (var VARIABLE in Levels)
        {
            VARIABLE.num = i++;
        }
        sortListUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            sortListUI();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            nextLevel();
        }

    }


    public void sortListUI( )
    {
        
        currentNum = Data.GetCurLevel;
        bool find = false;
        for (int i = currentNum; i < Levels.Count; i++)
        {
            if (Levels[i].levelState == LevelState.SpecialLevel && !find)
            {
                nextSpecialNum = i;
                find = true;
            }
        }

        if (find == false)
        {
            nextSpecialNum = Levels.Count-1;
        }

        pointnum = nextSpecialNum - currentNum +1;
        
        Slide.sizeDelta= new Vector2(pointnum * 100 + 20,30f);
        
        var startUI = new GameObject("start",typeof(Image));
        startUI.transform.parent = LevelUIparent;
        startUI.GetComponent<Image>().sprite = StartImg;
        startUI.GetComponent<RectTransform>().sizeDelta = SpecialUIsize;
        int PosXNum = -pointnum * 50;
        startUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(PosXNum,0);
        startUI.GetComponent<RectTransform>().localScale = Vector3.one;
        levelui.Add(startUI);
        
        
        for (int i = currentNum; i <= nextSpecialNum; i++)
        {
            var UI = new GameObject("level"+i.ToString(),typeof(Image));
            
            if (Levels[i].levelState == LevelState.NumberLevel)
            {
                UI.GetComponent<Image>().sprite = defaultImg;
                UI.transform.parent = LevelUIparent;
                UI.GetComponent<RectTransform>().sizeDelta = levelUIsize;
                
                var UItext = new GameObject("leveltext"+i.ToString(),typeof(Text));
                UItext.transform.parent = UI.transform;
                UItext.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
                UItext.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
                UItext.GetComponent<RectTransform>().offsetMax = Vector2.zero;
                UItext.GetComponent<RectTransform>().offsetMin = Vector2.zero; 
                setText(UItext.GetComponent<Text>(),Levels[i].num);
                
            }
            else
            {
                UI.GetComponent<Image>().sprite = SpecialImg;
                UI.transform.parent = LevelUIparent;
                UI.GetComponent<RectTransform>().sizeDelta = SpecialUIsize;
                
                var UIImg = new GameObject("levelImg"+i.ToString(),typeof(Image));
                UIImg.transform.parent = UI.transform;
                UIImg.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
                UIImg.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
                UIImg.GetComponent<RectTransform>().offsetMax = -new Vector2(5, 10);
                UIImg.GetComponent<RectTransform>().offsetMin = new Vector2(5, 10);
                UIImg.GetComponent<Image>().sprite = Levels[i].Image;
            } 
            
            PosXNum += 100;
            UI.GetComponent<RectTransform>().anchoredPosition = new Vector2(PosXNum,0);
            UI.GetComponent<RectTransform>().localScale = Vector3.one;
            
            levelui.Add(UI);
        }
        SlideBar.value = (float)(1.0f/pointnum)*(pointnum - (nextSpecialNum-currentNum));
        if ((1.0f / pointnum) * (pointnum - nextSpecialNum - currentNum) == 0)
        {
            SlideBar.value = 1;
        }
        
        Debug.Log(pointnum +" " +1/pointnum);
    }

    public void nextLevel()
    {
        currentNum++;
        if (Levels[currentNum-1].levelState == LevelState.NumberLevel)
        {
            if (pointnum - (nextSpecialNum - currentNum)-1 != 0 )
            {
                levelui[pointnum - (nextSpecialNum - currentNum)-1].transform.GetComponent<Image>().sprite = FinishImg;
            }
            
            SlideBar.value = (float)(1.0f/pointnum)*(pointnum - (nextSpecialNum-currentNum));
            if ((1.0f / pointnum) * (pointnum - nextSpecialNum - currentNum) == 0)
            {
                SlideBar.value = 1;
            }
        }
        else
        {
            ReFlash();
        }
    }

    public void ReFlash()
    {
        for (int i = levelui.Count - 1; i >= 0; i--)
        {
            Destroy(levelui[i]);
        }
        levelui.Clear();
        sortListUI();
    }
    public void setText(Text text ,int num)
    {
        text.text = num.ToString();
        text.font = font;
        text.fontSize = 44;
        text.horizontalOverflow = HorizontalWrapMode.Overflow;
        text.verticalOverflow = VerticalWrapMode.Overflow;
        text.alignment = TextAnchor.MiddleCenter;
    }
}

public enum LevelState
{
    NumberLevel,
    SpecialLevel
}

[Serializable]
public class level
{
    public int num;
    public Sprite Image;
    public bool passLevel;
    public bool nowLevel;
    public LevelState levelState;
}