using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class InDecal : MonoBehaviour
{
    public Camera UICamera;
    public GameObject DecalPrefab;
    public Transform DecalTransform;
    public PanelAnim PanelAnim;

    public Image ImageUI;
    private void Awake()
    {
        transform.GetComponent<TieEventTrigr>().callBack = IniDecal;
    }
    public void IniDecal()
    {
        if (DecalManager.Instance.canInsDecal)
        {
            GameObject decal = Instantiate(DecalPrefab, DecalTransform);
            decal.GetComponent<Pen>().uiCamera = UICamera;
            decal.GetComponent<Pen>().isEnable = true;
            decal.GetComponent<Pen>().penDecal.Texture = transform.GetChild(1).GetComponent<Image>().sprite.texture;
            decal.GetComponent<Pen>().TieShow.GetComponent<Image>().sprite = transform.GetChild(1).GetComponent<Image>().sprite;
            decal.GetComponent<Pen>().panelAnim = PanelAnim;
        }
    }
    
    
}
