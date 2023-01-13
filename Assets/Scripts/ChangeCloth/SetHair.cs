using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SetHair : MonoBehaviour
{
    public int num;
    public int Tag;
    [HideInInspector]
    public Image image;
    optionPanel optionPanel;
    public Eselectoption eselectoption;
    
    public Ecaizhi ecaizhi;
    private void Awake()
    {
        if (transform.GetComponent<EventsTriger>())
        {
            transform.GetComponent<EventsTriger>().callBack = SetMaterialClick;
        }
    }
    private void Start()
    {
        optionPanel = GetComponentInParent<optionPanel>();
        image = transform.GetChild(0).GetComponentInChildren<Image>();
    }

    public void SetMaterialClick()
    {
        
        if (transform.GetComponent<UIDrag>().isDrag)
        {
            transform.GetComponent<UIDrag>().isDrag = false;
            return;
        }
        VibratorManager.Trigger(1);
        this.gameObject.transform.DOShakeScale(0.5f, new Vector3(0.1f, 0.1f, 0));
        switch (eselectoption)
        {
            case Eselectoption.shangyi:
                Selectcloth.Ins.SetS1Model(num);
                break;
            case Eselectoption.xiayi:
                Selectcloth.Ins.SetX1Model(num);
                break;
            case Eselectoption.niukou:
                Selectcloth.Ins.SetNiukou(num);
                
                break;
            case Eselectoption.caizhi :
                Selectcloth.Ins.SetCaizhi(ecaizhi);
                break;
            case Eselectoption.faxing:
                Selectcloth.Ins.SetHairModel(num);
                break;
            case Eselectoption.xiezi :
                Selectcloth.Ins.SetXieziModel(num);
                break;
            case Eselectoption.color :
                Selectcloth.Ins.SetColor(num);
                break;
            case Eselectoption.toushi:
                Selectcloth.Ins.SetToushiModel(num);
                break;
            case Eselectoption.shoushi:
                Selectcloth.Ins.SetShoushiModel(num);
                break;
            case Eselectoption.baobao:
                Selectcloth.Ins.SetBaobaoModel(num);
                break;
            case Eselectoption.HairColor:
                Selectcloth.Ins.SetHairColor(num);
                break;
            case Eselectoption.ShoeColor:
                Selectcloth.Ins.SetShoeColor(num);
                break;    
            default:
                break;
        }
        optionPanel.SelectOption(eselectoption, Tag);
        
        Selectcloth.Ins.CurrentShowGirl.ShowCurrentCloths();
        
    }
}
