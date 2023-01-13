using System;
using System.Collections;
using System.Collections.Generic;
using FluffyUnderware.Curvy.Controllers;
using UnityEngine;

using PaintIn3D;

public class idelShowGirl : MonoBehaviour
{
    public List<GameObject> s1List = new List<GameObject>();
    public List<GameObject> x1List = new List<GameObject>();
    public List<GameObject> FaxinList = new List<GameObject>();
    public List<GameObject> XieziList = new List<GameObject>();
    public List<GameObject> BaobaoList = new List<GameObject>();
    public List<GameObject> ToushiList = new List<GameObject>();
    public List<shoushi> ShoushiList = new List<shoushi>();
    public List<GameObject> zhazhenCloth = new List<GameObject>();
    public List<GameObject> TieCloth = new List<GameObject>();
    public List<GameObject> NiukouList = new List<GameObject>();

    public RuntimeAnimatorController ShowBaobao;
    public RuntimeAnimatorController OriAnimator;

    public void ShowCloth( Model model)
    {
        Inactivecloth(s1List);
        Inactivecloth(x1List);
        Inactivecloth(TieCloth);
        Inactivecloth(zhazhenCloth);
        if (model.iszhazhen.isOn )
        {
            //Selectcloth.Ins.SetzhazhenS1Model(model.iszhazhen.ClothNum);
            ShowCurrentCloth(zhazhenCloth,model.iszhazhen.ClothNum);
        }else if (model.iscaijian.isOn)
        {
            //Selectcloth.Ins.SetS1Model(model.S1.);
            ShowCurrentCloth(s1List,model.iscaijian.ClothNum);
            ShowCurrentCloth(model.X1.isOn,x1List,model.X1.ClothNum);
        }else if (model.isTie.isOn)
        {
            //贴纸衣服
            ShowCurrentCloth(model.isTie.isOn,TieCloth,model.isTie.ClothNum);
        } 
        else
        {
            ShowCurrentCloth(s1List,model.S1.ClothNum);
            ShowCurrentCloth(model.X1.isOn,x1List,model.X1.ClothNum);
        }
        if (model.baobao.isOn)
        {
            transform.GetComponent<ModelBeahviour>().NeedHandUp = true;
            transform.GetComponent<Animator>().runtimeAnimatorController = ShowBaobao;
        }
        else
        {
            transform.GetComponent<Animator>().runtimeAnimatorController = OriAnimator;

        }
        
        Inactivecloth(FaxinList);
        ShowCurrentCloth(FaxinList,model.Hair.ClothNum);
        
        Inactivecloth(XieziList);
        ShowCurrentCloth(XieziList,model.xie.ClothNum);
        
        Inactivecloth(ToushiList);
        ShowCurrentCloth(model.toushi.isOn,ToushiList,model.toushi.ClothNum);
        
        Inactivecloth(ShoushiList);
        ShowCurrentCloth(model.shoushi.isOn,ShoushiList,model.shoushi.ClothNum);
        
        Inactivecloth(BaobaoList);
        ShowCurrentCloth(model.baobao.isOn,BaobaoList,model.baobao.ClothNum);
        
        Inactivecloth(NiukouList);
        ShowCurrentCloth(model.niukou.isOn,NiukouList,model.niukou.ClothNum);
        if (model.niukou.isOn)
        {
            NiukouList[model.niukou.ClothNum].GetComponentInChildren<ButtonTest>().transform.localPosition = model.NiukouPos;
            NiukouList[model.niukou.ClothNum].GetComponentInChildren<ButtonTest>().transform.localRotation = model.NiukouRoatate;
            NiukouList[model.niukou.ClothNum].GetComponentInChildren<ButtonTest>().transform.localScale = model.NiukouScale;
        }
        switch (model.caizhi)
        {
            case Ecaizhi.caijian:
                ChangeMaterial(model,Selectcloth.Ins.caijianmaterialList);
                break;
            
            case Ecaizhi.zhazhen:
                ChangeMaterial(model,Selectcloth.Ins.ZhazhenMaterialList);
                break;
            case Ecaizhi.putong:
                ChangeMaterial(model,Selectcloth.Ins.putongmaterialList);
                break;
            case Ecaizhi.pi:
                ChangeMaterial(model,Selectcloth.Ins.pimaterialList);
                break;
            case Ecaizhi.buliao:
                ChangeMaterial(model,Selectcloth.Ins.buliaomaterialList);
                break;
            case Ecaizhi.gewen:
                ChangeMaterial(model,Selectcloth.Ins.gewenmaterialList);
                break;
            case Ecaizhi.huawen:
                ChangeMaterial(model,Selectcloth.Ins.huawenmaterialList);
                break;
            case Ecaizhi.shuiwen:
                ChangeMaterial(model,Selectcloth.Ins.shuiwenmaterialList);
                break;
            case Ecaizhi.Tie:
                ChangeMaterial(model,Selectcloth.Ins.TieMaterialList);
                break;
            default:
                break;
        }
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

    public GameObject mao;
    public void ChangeMaterial( Model model,MaterialList _materialList)
    {
        if (model.iszhazhen.isOn)
        {
            zhazhenCloth[model.iszhazhen.ClothNum].GetComponent<SkinnedMeshRenderer>().materials = new Material[1]
            {
                _materialList.materialList[model.yanse.ClothNum].material01
            };
        }else if (model.isTie.isOn)
        {
            if (TieCloth[model.isTie.ClothNum].GetComponent<SkinnedMeshRenderer>().materials.Length == 1)
            {
                TieCloth[model.isTie.ClothNum].GetComponent<SkinnedMeshRenderer>().materials = new Material[1]
                {
                    _materialList.materialList[model.isTie.ClothNum].material01
                };
            }
            else
            {
                TieCloth[model.isTie.ClothNum].GetComponent<SkinnedMeshRenderer>().materials = new Material[2]
                {
                    _materialList.materialList[model.yanse.ClothNum].material01,
                    _materialList.materialList[model.yanse.ClothNum].material02
                };
            }
            
        }
        else
        {
            if (s1List[model.S1.ClothNum].GetComponent<SkinnedMeshRenderer>().materials.Length == 1)
            {
                s1List[model.S1.ClothNum].GetComponent<SkinnedMeshRenderer>().materials = new Material[1]
                {
                    _materialList.materialList[model.yanse.ClothNum].material01
                };
            }
            else
            {
                s1List[model.S1.ClothNum].GetComponent<SkinnedMeshRenderer>().materials = new Material[2]
                {
                    _materialList.materialList[model.yanse.ClothNum].material01,
                    _materialList.materialList[model.yanse.ClothNum].material02,
                };
            }
            if (x1List[model.X1.ClothNum].GetComponent<SkinnedMeshRenderer>().materials.Length == 1)
            {
                x1List[model.X1.ClothNum].GetComponent<SkinnedMeshRenderer>().materials = new Material[1]
                {
                    _materialList.materialList[model.yanse.ClothNum].material01
                };
            }
            else
            {
                x1List[model.X1.ClothNum].GetComponent<SkinnedMeshRenderer>().materials = new Material[2]
                {
                    _materialList.materialList[model.yanse.ClothNum].material01,
                    _materialList.materialList[model.yanse.ClothNum].material02,
                };
            }
        }
        
        FaxinList[model.Hair.ClothNum].GetComponent<MeshRenderer>().materials = new Material[1]
        {
            Selectcloth.Ins.hairmaterialList.materialList[model.hairyanse.ClothNum].material01
        };
        mao.GetComponent<MeshRenderer>().materials = new Material[1]
        {
            Selectcloth.Ins.hairmaterialList.materialList[model.hairyanse.ClothNum].material01
        };
        
        
        if (XieziList[model.xie.ClothNum].GetComponent<SkinnedMeshRenderer>().materials.Length == 1)
        {
            XieziList[model.xie.ClothNum].GetComponent<SkinnedMeshRenderer>().materials = new Material[1]
            {
                _materialList.materialList[model.Shoeyanse.ClothNum].material01
            };

        }else if(XieziList[model.xie.ClothNum].GetComponent<SkinnedMeshRenderer>().materials.Length == 2){
            XieziList[model.xie.ClothNum].GetComponent<SkinnedMeshRenderer>().materials = new Material[2]
            {
                _materialList.materialList[model.Shoeyanse.ClothNum].material01,
                XieziList[model.xie.ClothNum].GetComponent<SkinnedMeshRenderer>().materials[1]
                
            };
        }
        else
        {
            XieziList[model.xie.ClothNum].GetComponent<SkinnedMeshRenderer>().materials = new Material[3]
            {
                _materialList.materialList[model.Shoeyanse.ClothNum].material01,
                _materialList.materialList[model.Shoeyanse.ClothNum].material02,
                XieziList[model.xie.ClothNum].GetComponent<SkinnedMeshRenderer>().materials[2]
            };
        }
    }
}
