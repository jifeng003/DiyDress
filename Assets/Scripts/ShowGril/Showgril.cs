using System;
using System.Collections;
using System.Collections.Generic;using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using UnityEngine;
using DG.Tweening;
using PaintIn3D;
using RootMotion;
using RootMotion.FinalIK;


public class Showgril : MonoBehaviour
{
    [Header("扎针、普通")]public int Color;
    
    
    public bool isputongPart;
    
    [Space]
    public bool iszhazhenPart;
    public int Currentzz;
    [Space]
    public bool iscaijianPart;
    public int CurrentS1;
    public int CurrentCaijianMaterial;
    [Space] 
    public bool isIdelPart;

    [Space] 
    public bool isTiePart;
    public int CurrentTieCloth;
    [Space]
    public titlePanel TitlePanel;
    public List<GameObject> s1List = new List<GameObject>();
    public List<GameObject> x1List = new List<GameObject>();
    public List<GameObject> FaxinList = new List<GameObject>();
    public List<GameObject> XieziList = new List<GameObject>();
    public List<GameObject> BaobaoList = new List<GameObject>();
    public List<GameObject> ToushiList = new List<GameObject>();
    public List<shoushi> ShoushiList = new List<shoushi>();
    public List<GameObject> zhazhenCloth = new List<GameObject>();
    public List<GameObject> TieCloth = new List<GameObject>();
    public List<GameObject>  NiukouList = new List<GameObject>();
    public Vector3 NiukouPos;
    public Quaternion NiukouRoatate;
    public Vector3 NiukouScale;

    public GameObject NeiYi;

    public bool shangyi = false;
    public bool xiazhuang = false;
    public bool faxin = false;
    public bool xiezi = false;
    public bool baobao = false;
    public bool toushi = false;
    public bool shoushi = false;
    public bool caizhi = false;
    public bool niukou = false;
    public GameObject mao;


    private void OnEnable()
    {
        if (!isIdelPart)
        {
            CheckEnableCloth();
        }
        
        InsSelectChoth();
    }

    public void InsSelectChoth()
    {
        //InActiveAll();
        
        if (isIdelPart)
        {
            isTiePart     = Selectcloth.Ins.isTiePart;
            
            CurrentTieCloth= Selectcloth.Ins.currentTiecloth;
            iscaijianPart = Selectcloth.Ins.iscaiPart;
            
            isputongPart  = Selectcloth.Ins.isputong;
            iszhazhenPart = Selectcloth.Ins.iszhaPart;
            CurrentCaijianMaterial = Selectcloth.Ins.currentColor;
            Currentzz = Selectcloth.Ins.currentZZcloth;
            niukou = Selectcloth.Ins.isNouikou;
            if (niukou)
            {
                Debug.Log("niukou 位置");
                NiukouList[Selectcloth.Ins.currentNiukou].GetComponentInChildren<ButtonTest>().transform.localPosition =
                    NiukouPos  ;
                NiukouList[Selectcloth.Ins.currentNiukou].GetComponentInChildren<ButtonTest>().transform.localRotation =
                    NiukouRoatate;
                NiukouList[Selectcloth.Ins.currentNiukou].GetComponentInChildren<ButtonTest>().transform.localScale =
                    NiukouScale;
            }
        }
        else
        {
            Selectcloth.Ins.isTiePart = isTiePart     ;
            Selectcloth.Ins.iscaiPart = iscaijianPart ;
            Selectcloth.Ins.isputong = isputongPart  ;
            Selectcloth.Ins.iszhaPart = iszhazhenPart ;
            Selectcloth.Ins.SetColor(CurrentCaijianMaterial);
            Selectcloth.Ins.SetShoeColor(CurrentCaijianMaterial);
            Selectcloth.Ins.isNouikou = niukou ;
            
            if (iszhazhenPart)
            {
                Selectcloth.Ins.iniCloth(Ecaizhi.zhazhen,Color);
                Selectcloth.Ins.currentZZcloth = Currentzz;
            }
            if (isTiePart)
            {
                Selectcloth.Ins.iniCloth(Ecaizhi.Tie);
                Selectcloth.Ins.currentTiecloth = CurrentTieCloth;
                TitlePanel.ColorOFF = true;
            }
            if (iscaijianPart)
            {
                Selectcloth.Ins.iniCloth(Ecaizhi.caijian);
                Selectcloth.Ins.currentColor = CurrentCaijianMaterial;
                Selectcloth.Ins.currentShoeColor = CurrentCaijianMaterial;
                TitlePanel.ColorOFF = true;
            }
            if((isputongPart))
            {
                Selectcloth.Ins.iniCloth(Ecaizhi.putong,Color);
            }
        }

        ShowCurrentCloths();
    }
    public void CheckEnableCloth()
    {
        foreach (var VARIABLE in TitlePanel.TitleEvents)
        {
            switch (VARIABLE.Tag)
            {
                case 0:
                    if (isputongPart)
                    {
                        shangyi = true;
                    }
                    break;
                case 1:
                    if (isputongPart || iscaijianPart)
                    {
                        xiazhuang = true;
                    }
                    break;
                case 2:
                    caizhi = true;
                    break;
                case 3:
                    faxin = true;
                    break;
                case 4:
                    xiezi = true;
                    break;
                case 5:
                    toushi = true;
                    break;
                case 6:
                    shoushi = true;
                    break;
                case 7:
                    niukou = true;
                    
                    break;
                case 8:
                    baobao = true;
                    break;
                default:
                    break;
            }
        }
    }
    public Model model;
    private void Update()
    {
        if (niukou )
        {
            Selectcloth.Ins.CurrentShowGirl.NiukouPos = NiukouList[Selectcloth.Ins.currentNiukou].GetComponentInChildren<ButtonTest>().transform.localPosition;
            Selectcloth.Ins.CurrentShowGirl.NiukouRoatate = NiukouList[Selectcloth.Ins.currentNiukou].GetComponentInChildren<ButtonTest>().transform.localRotation;
            Selectcloth.Ins.CurrentShowGirl.NiukouScale = NiukouList[Selectcloth.Ins.currentNiukou].GetComponentInChildren<ButtonTest>().transform.localScale;
        }
        Modelshow();
    }

    void Modelshow()
    {
        model.S1.isOn = shangyi;
        model.X1.isOn = xiazhuang;
        model.xie.isOn = xiezi;
        model.Hair.isOn = faxin;
        model.baobao.isOn = baobao;
        model.toushi.isOn = toushi;
        model.shoushi.isOn = shoushi;
        model.niukou.isOn = niukou;
        
        model.caizhi = Selectcloth.Ins.ecaizhi;
        
        model.S1.ClothNum = Selectcloth.Ins.currents1;
        model.X1.ClothNum = Selectcloth.Ins.currentx1;
        model.xie.ClothNum = Selectcloth.Ins.currentXiezi;
        model.Hair.ClothNum = Selectcloth.Ins.currentFaxin;
        model.baobao.ClothNum = Selectcloth.Ins.currentBaobao;
        model.niukou.ClothNum = Selectcloth.Ins.currentNiukou;
        model.toushi.ClothNum = Selectcloth.Ins.currentToushi;
        model.shoushi.ClothNum = Selectcloth.Ins.currentShoushi;

        model.iszhazhen.isOn = iszhazhenPart;
        model.iscaijian.isOn = iscaijianPart;
        model.iscaijian.ClothNum = CurrentS1;
        model.isTie.isOn = isTiePart;
        model.isTie.ClothNum = CurrentTieCloth;
        
        model.iszhazhen.ClothNum = Currentzz;
        model.yanse.ClothNum = Selectcloth.Ins.currentColor;
        model.hairyanse.ClothNum = Selectcloth.Ins.currentHairColor;
        model.Shoeyanse.ClothNum = Selectcloth.Ins.currentShoeColor;

        model.NiukouPos = NiukouPos;
        model.NiukouRoatate = NiukouRoatate;
        model.NiukouScale = NiukouScale;
    }
    void ShowCloth()
    {
        //ShowCurrentCloths();
        
        switch (Selectcloth.Ins.ecaizhi)
        {
            case Ecaizhi.caijian:
                ChangeMaterial(Selectcloth.Ins.caijianmaterialList);
                break;
            case Ecaizhi.putong:
                ChangeMaterial(Selectcloth.Ins.putongmaterialList);
                break;
            case Ecaizhi.pi:
                ChangeMaterial(Selectcloth.Ins.pimaterialList);
                break;
            case Ecaizhi.buliao:
                ChangeMaterial(Selectcloth.Ins.buliaomaterialList);
                break;
            case Ecaizhi.gewen:
                ChangeMaterial(Selectcloth.Ins.gewenmaterialList);
                break;
            case Ecaizhi.huawen:
                ChangeMaterial(Selectcloth.Ins.huawenmaterialList);
                break;
            case Ecaizhi.shuiwen:
                ChangeMaterial(Selectcloth.Ins.shuiwenmaterialList);
                break;
            case Ecaizhi.Tie:
                ChangeMaterial(Selectcloth.Ins.TieMaterialList);
                break;
            case Ecaizhi.zhazhen:
                ChangeMaterial(Selectcloth.Ins.ZhazhenMaterialList);
                break;
            default:
                break;
        }
        
    }

    public void ShowCurrentCloths()
    {
        
        Inactivecloth(s1List);
        Inactivecloth(x1List);
        Inactivecloth(TieCloth);
        Inactivecloth(zhazhenCloth);
        if (iszhazhenPart)
        {
            Selectcloth.Ins.SetzhazhenS1Model(Currentzz);
            ShowCurrentCloth(zhazhenCloth,Selectcloth.Ins.currentZZcloth);
        }
        else if(iscaijianPart)
        {
            Selectcloth.Ins.SetS1Model(CurrentS1);
            ShowCurrentCloth(s1List,Selectcloth.Ins.currents1);
            ShowCurrentCloth(xiazhuang,x1List,Selectcloth.Ins.currentx1);
        }else if (isTiePart)
        {
            //贴纸衣服
            ShowCurrentCloth(isTiePart,TieCloth,CurrentTieCloth);
        } 
        else
        {
            ShowCurrentCloth(s1List,Selectcloth.Ins.currents1);
            ShowCurrentCloth(xiazhuang,x1List,Selectcloth.Ins.currentx1);

        }
        Inactivecloth(FaxinList);
        ShowCurrentCloth(FaxinList,Selectcloth.Ins.currentFaxin);
        
        Inactivecloth(XieziList);
        ShowCurrentCloth(XieziList,Selectcloth.Ins.currentXiezi);
        
        Inactivecloth(ToushiList);
        ShowCurrentCloth(toushi,ToushiList,Selectcloth.Ins.currentToushi);
        
        
        Inactivecloth(ShoushiList);
        
        ShowCurrentCloth(shoushi,ShoushiList,Selectcloth.Ins.currentShoushi);
        
        
        Inactivecloth(BaobaoList);
        ShowCurrentCloth(baobao,BaobaoList,Selectcloth.Ins.currentBaobao);
        
        Inactivecloth(NiukouList);
        ShowCurrentCloth(niukou,NiukouList,Selectcloth.Ins.currentNiukou);
        
        
            
        ShowCloth();
        
    }
    public void ShowCurrentCloth(List<GameObject> Cloth,int Num )
    {
        Cloth[Num].SetActive(true);
    }
    public void ShowCurrentCloth(bool isenable,List<GameObject> Cloth,int Num )
    {
        if (isenable)
        {
            Cloth[Num].SetActive(true);
        }
        
    }
    public void ShowCurrentCloth(bool isenable,List<shoushi> Cloth,int Num )
    {
        if (isenable)
        {
            Cloth[Num].Obj[0].SetActive(true);
            if (Cloth[Num].isDouble)
            {
                Cloth[Num].Obj[1].SetActive(true);
            }
        }
    }
    public void Inactivecloth(List<GameObject> clothes)
    {
        for (int i = 0; i < clothes.Count; i++)
        {
            clothes[i].SetActive(false);
        }
        
    }
    public void Inactivecloth(List<shoushi> cloth)
    {
        for (int i = 0; i < cloth.Count; i++)
        {
            if (cloth[i].isDouble)
            {
                cloth[i].Obj[0].SetActive(false);
                cloth[i].Obj[1].SetActive(false);
            }
            else
            {
                cloth[i].Obj[0].SetActive(false);
            }
        }
        
    }
    
    public void ChangeMaterial(MaterialList _materialList)
    {
        if (iszhazhenPart)
        {
            zhazhenCloth[Selectcloth.Ins.currentZZcloth].GetComponent<SkinnedMeshRenderer>().materials = new Material[1]
            {
                _materialList.materialList[Selectcloth.Ins.currentColor].material01
            };
        }else if (isTiePart)
        {
            Selectcloth.Ins.currentColor = CurrentTieCloth;
            TieCloth[CurrentTieCloth].GetComponent<SkinnedMeshRenderer>().materials = new Material[1]
            {
                _materialList.materialList[Selectcloth.Ins.currentColor].material01
            };
            
        }
        else
        {
            if (s1List[Selectcloth.Ins.currents1].GetComponent<SkinnedMeshRenderer>().materials.Length == 1)
            {
                s1List[Selectcloth.Ins.currents1].GetComponent<SkinnedMeshRenderer>().materials = new Material[1]
                {
                    _materialList.materialList[Selectcloth.Ins.currentColor].material01
                };
            }
            else
            {
                s1List[Selectcloth.Ins.currents1].GetComponent<SkinnedMeshRenderer>().materials = new Material[2]
                {
                    _materialList.materialList[Selectcloth.Ins.currentColor].material01,
                    _materialList.materialList[Selectcloth.Ins.currentColor].material02,
                };
            }
            if (x1List[Selectcloth.Ins.currentx1].GetComponent<SkinnedMeshRenderer>().materials.Length == 1)
            {
                x1List[Selectcloth.Ins.currentx1].GetComponent<SkinnedMeshRenderer>().materials = new Material[1]
                {
                    _materialList.materialList[Selectcloth.Ins.currentColor].material01
                };
            }
            else
            {
                x1List[Selectcloth.Ins.currentx1].GetComponent<SkinnedMeshRenderer>().materials = new Material[2]
                {
                    _materialList.materialList[Selectcloth.Ins.currentColor].material01,
                    _materialList.materialList[Selectcloth.Ins.currentColor].material02,
                };
            }
        }
        
        FaxinList[Selectcloth.Ins.currentFaxin].GetComponent<MeshRenderer>().materials = new Material[1]
        {
            Selectcloth.Ins.hairmaterialList.materialList[Selectcloth.Ins.currentHairColor].material01
        };
        mao.GetComponent<MeshRenderer>().materials = new Material[1]
        {
            Selectcloth.Ins.hairmaterialList.materialList[Selectcloth.Ins.currentHairColor].material01
        };

        ChangeXieZi(_materialList,xiezi,TitlePanel.ColorOFF);

    }

    public void ChangeXieZi(MaterialList _materialList,bool Canchange,bool colorChange)
    {
        if (Canchange && !colorChange)
        {
            if (XieziList[Selectcloth.Ins.currentXiezi].GetComponent<SkinnedMeshRenderer>().materials.Length == 1)
            {
                XieziList[Selectcloth.Ins.currentXiezi].GetComponent<SkinnedMeshRenderer>().materials = new Material[1]
                {
                    _materialList.materialList[Selectcloth.Ins.currentShoeColor].material01
                };

            }else if(XieziList[Selectcloth.Ins.currentXiezi].GetComponent<SkinnedMeshRenderer>().materials.Length == 2){
                XieziList[Selectcloth.Ins.currentXiezi].GetComponent<SkinnedMeshRenderer>().materials = new Material[2]
                {
                    _materialList.materialList[Selectcloth.Ins.currentShoeColor].material01,
                    XieziList[Selectcloth.Ins.currentXiezi].GetComponent<SkinnedMeshRenderer>().materials[1]
                
                };
            }
            else
            {
                XieziList[Selectcloth.Ins.currentXiezi].GetComponent<SkinnedMeshRenderer>().materials = new Material[3]
                {
                    _materialList.materialList[Selectcloth.Ins.currentShoeColor].material01,
                    _materialList.materialList[Selectcloth.Ins.currentShoeColor].material02,
                    XieziList[Selectcloth.Ins.currentXiezi].GetComponent<SkinnedMeshRenderer>().materials[2]
                };
            }
        }
        else
        {
            Selectcloth.Ins.currentShoeColor = Selectcloth.Ins.currentColor;
            if (XieziList[Selectcloth.Ins.currentXiezi].GetComponent<SkinnedMeshRenderer>().materials.Length == 1)
            {
                XieziList[Selectcloth.Ins.currentXiezi].GetComponent<SkinnedMeshRenderer>().materials = new Material[1]
                {
                    _materialList.materialList[Selectcloth.Ins.currentShoeColor].material01
                };

            }else if(XieziList[Selectcloth.Ins.currentXiezi].GetComponent<SkinnedMeshRenderer>().materials.Length == 2){
                XieziList[Selectcloth.Ins.currentXiezi].GetComponent<SkinnedMeshRenderer>().materials = new Material[2]
                {
                    _materialList.materialList[Selectcloth.Ins.currentShoeColor].material01,
                    XieziList[Selectcloth.Ins.currentXiezi].GetComponent<SkinnedMeshRenderer>().materials[1]
                
                };
            }
            else
            {
                XieziList[Selectcloth.Ins.currentXiezi].GetComponent<SkinnedMeshRenderer>().materials = new Material[3]
                {
                    _materialList.materialList[Selectcloth.Ins.currentShoeColor].material01,
                    _materialList.materialList[Selectcloth.Ins.currentShoeColor].material02,
                    XieziList[Selectcloth.Ins.currentXiezi].GetComponent<SkinnedMeshRenderer>().materials[2]
                };
            }
        }
    }
}

[Serializable]
public class shoushi
{
    public bool isDouble;
    public GameObject[] Obj = new GameObject[2];

}