using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;



public class Selectcloth : MonoBehaviour
{
    //使用单例模式便于衣服更换的调用
    private static Selectcloth _Ins;
    public static Selectcloth Ins
    {
        get
        {
            return _Ins;
        }
    }
    
    [HideInInspector]
    public MaterialList caijianmaterialList;
    [HideInInspector]
    public MaterialList putongmaterialList;
    [HideInInspector]
    public MaterialList pimaterialList;
    [HideInInspector]
    public MaterialList buliaomaterialList;
    [HideInInspector]
    public MaterialList gewenmaterialList;
    [HideInInspector]
    public MaterialList huawenmaterialList;
    [HideInInspector]
    public MaterialList shuiwenmaterialList;
    
    [HideInInspector]
    public MaterialList hairmaterialList;
    
    [HideInInspector]
    public MaterialList TieMaterialList;
    
    [HideInInspector]
    public MaterialList ZhazhenMaterialList;

    //需要存储的数据
    public int currentColor;                        //当前颜色
    public int currentHairColor;                    //当前头发颜色
    public int currentShoeColor;                    //当前鞋子颜色
    public Ecaizhi ecaizhi = Ecaizhi.putong;        ///当前材质
    public int currents1;                           //当前上衣下标
    public int currentx1;                           //当前下衣下标
    public int currentFaxin;                          //当前头发下标
    public int currentXiezi;                        //当前鞋子下标
    public int currentBaobao;                        //当前包包下标
    public int currentToushi;                        //当前头饰下标
    public int currentShoushi;                        //当前首饰下标
    public int currentNiukou;                        //当前纽扣下标
    public int currentZZcloth;                        //当前扎针衣物下标
    public int currentTiecloth;
    
    public bool isTiePart;
    public bool iszhaPart;
    public bool iscaiPart;
    public bool isputong;
    public bool isNouikou;

    public Showgril CurrentShowGirl;
    
    //若单例类的自身初始化及调用问题不放在Awake会导致其他在Start调用此字段的类找不到这个单例类
    private void Awake()
    {
        if (!_Ins)
        {
            _Ins = this;
        }
        hairmaterialList = Resources.Load<MaterialList>("hairmaterialList");
        
        putongmaterialList = Resources.Load<MaterialList>("putongMaterialList");
        caijianmaterialList = Resources.Load<MaterialList>("cajiianMaterialList");
        
        ZhazhenMaterialList = Resources.Load<MaterialList>("ZhazhenMaterialList");
        pimaterialList = Resources.Load<MaterialList>("piMaterialList");
        buliaomaterialList = Resources.Load<MaterialList>("buliaoMaterialList");
        gewenmaterialList = Resources.Load<MaterialList>("gewenMaterialList");
        huawenmaterialList = Resources.Load<MaterialList>("huawenMaterialList");
        shuiwenmaterialList = Resources.Load<MaterialList>("shuiwenMaterialList");
        TieMaterialList = Resources.Load<MaterialList>("TieMaterialList");
    }

    /// <summary>
    /// 扎针、普通
    /// </summary>
    /// <param name="caizhi"></param>
    /// <param name="ColorNumber"></param>
    public void iniCloth(Ecaizhi caizhi,int ColorNumber)
    {
        
        ecaizhi = caizhi;        ///当前材质
        currentColor = ColorNumber; //当前颜色
        currentShoeColor = currentColor;       //当前鞋子颜色
        currentHairColor = 0;                  //当前头发颜色
                         

        currents1 = 1;             //当前上衣下标
        currentx1 = 0;           //当前下衣下标
        currentFaxin = 0;                //当前头发下标
        currentXiezi = 0;              //当前鞋子下标
        currentBaobao = 0;                //当前包包下标
        currentToushi = 0;                //当前头饰下标
        currentShoushi = 0;
        currentNiukou = 0;
    }

    /// <summary>
    /// 裁剪贴纸
    /// </summary>
    /// <param name="caizhi"></param>
    public void iniCloth(Ecaizhi caizhi)
    {
        
        ecaizhi = caizhi;        ///当前材质
        currentColor = 0; //当前颜色
        currentHairColor = 0;                  //当前头发颜色
        currentShoeColor = 0;                  //当前鞋子颜色
        currents1 = 1;             //当前上衣下标
        currentx1 = 0;           //当前下衣下标
        currentFaxin = 0;                //当前头发下标
        currentXiezi = 0;              //当前鞋子下标
        currentBaobao = 0;                //当前包包下标
        currentToushi = 0;                //当前头饰下标
        currentShoushi = 0;
        currentNiukou = 0;
    }
    
    public void SetS1Model(int _meshIndex)
    {
        currents1 = _meshIndex;
    }
    
    public void SetzhazhenS1Model(int _meshIndex)
    {
        currentZZcloth = _meshIndex;
    }
    public void SetX1Model(int _meshIndex)
    {
        currentx1 = _meshIndex;
    }
    public void SetHairModel(int _meshIndex)
    {
        currentFaxin = _meshIndex;
    }
    public void SetToushiModel(int _meshIndex)
    {
        currentToushi = _meshIndex;

    }
    public void SetShoushiModel(int _meshIndex)
    {
        currentShoushi = _meshIndex;

    }
    public void SetBaobaoModel(int _meshIndex)
    {
        currentBaobao = _meshIndex;

    }
    public void SetXieziModel(int _meshIndex)
    {
        currentXiezi = _meshIndex;

    }
    public void SetCaizhi(Ecaizhi _ecaizhi)
    {
        ecaizhi = _ecaizhi;

    }
    public void SetColor(int _colorIndex)
    {
        currentColor = _colorIndex;

    }
    public void SetHairColor(int _colorIndex)
    {
        currentHairColor = _colorIndex;

    }
    
    public void SetShoeColor(int _colorIndex)
    {
        currentShoeColor = _colorIndex;

    }
        
    public void SetNiukou(int _colorIndex)
    {
        currentNiukou = _colorIndex;
        CurrentShowGirl.ShowCurrentCloths();
    }
    
}
