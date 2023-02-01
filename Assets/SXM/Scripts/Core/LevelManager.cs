using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using SXM;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager :Singleleton<LevelManager>
{
  public GameObject[] level;
  public Transform Pos;
  public LevelTest LevelTest;
  public PeoplePosContainer PosContainer;
  public Text StageLevel;
  public TTPGame_SDK SDK;
  public void LoadLevel()
  {
    levelIns();
  }

  public void NextLevel(){
    Destroy(Pos.GetChild(0).gameObject);
    SDK.OnMissionComplete();

    Debug.Log("关卡"+Data.GetCurLevel+"level长度"+level.Length);
    if (Data.GetCurLevel == level.Length)
    {
      Data.UpCurStageLevel();
      StopAllCoroutines();
      Data.SetCurLevel(0);
      LevelTest.ReFlash();
      GameManager.Instance.jsonSave.datas.Models.Clear();
      GameManager.Instance.jsonSave.SaveDatasJson();
      StageManager.Instance.Tai2 = false;
      StageManager.Instance.Tai2Stand = false;
      PosContainer.ClearAllModel();
      GameManager.Instance.jsonSave.TieRefresh();
    }
    else
    {
      LevelTest.nextLevel();
    }
    levelIns();
  }
  
  public void RestLevel(){
    Destroy(Pos.GetChild(0).gameObject);
    levelIns();
  }
  public void levelIns()
  {
    TinyStarted();
    Debug.Log(Data.GetCurLevel);
    StageLevel.text = "Stage"+(Data.GetCurStageLevel() + 1).ToString("D2");
    if (Data.GetCurLevel == 0)
    {
      GameManager.Instance.jsonSave.TieRefresh();
    }
    SDK.OnMissionStarted(Data.GetCurLevel+1);
    Instantiate(level[Data.GetCurLevel], Pos);
    
  }
  public void TinyStarted()
  {
    int lv = Data.GetCurLevel;
    //TinySauce.OnGameStarted(levelNumber:lv.ToString());
  }
}
