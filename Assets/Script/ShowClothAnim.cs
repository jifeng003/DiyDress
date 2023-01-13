using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using PaintIn3D;
using UnityEngine;

public enum Scene
{
    caijian,
    zhazhen
}
public class ShowClothAnim : MonoBehaviour
{
    public Transform Cloth;
    //public Transform TagrgetPos;
    public ParticleSystem Smoke;
    public Camera mainCamera;
    public Transform Ori;
    public Transform now;
    public PanelAnim PanelAnim;
    public GameObject matchpart;
    public GameObject Bg;

    public Showgril Showgril;

    private void Awake()
    {
        Showgril = transform.GetComponent<Showgril>();
    }

    public void BeginAnim()
    {
        Cloth.DOScale(Showgril.s1List[Showgril.CurrentS1].transform.localScale, 1.2f);
        Cloth.DOLocalMoveY(Showgril.s1List[Showgril.CurrentS1].transform.localPosition.y, 1.2f);
        Cloth.DOLocalMoveZ(Showgril.s1List[Showgril.CurrentS1].transform.localPosition.z, 1.2f);
        Cloth.DORotateQuaternion(Showgril.s1List[Showgril.CurrentS1].transform.rotation, 1.2f);
        
        Tool.Timer.Register(.5f, delegate
        {
            mainCamera.transform.DORotateQuaternion(now.rotation, 1f);
            mainCamera.transform.DOMove(now.position, 1f);
        });
        Tool.Timer.Register(1.7f, delegate
        {
            Smoke.Play();
        });
        Tool.Timer.Register(1.25f, delegate
        {
            transform.GetComponent<Showgril>().NeiYi.SetActive(false);
            Cloth.DOLocalMoveX(Showgril.s1List[Showgril.CurrentS1].transform.localPosition.x, 1.2f).OnComplete((() =>
            {
                Destroy(Cloth.gameObject);
                transform.GetComponent<Showgril>().enabled = true;
            }));
        });
        Tool.Timer.Register(2.8f, delegate
        {
            mainCamera.transform.DORotateQuaternion(Ori.rotation, 1f);
            mainCamera.transform.DOMove(Ori.position, 1f);
            
            matchpart.SetActive(true);
            Bg.SetActive(false);
            PanelAnim.ShowPanel();
            
        });
    }

    public void PaintAnim()
    {
        VibratorManager.Trigger(2);
        Debug.Log("PLAY");
        mainCamera.transform.DORotateQuaternion(Ori.rotation, 1f);
        mainCamera.transform.DOMove(Ori.position, 1f);
        Bg.SetActive(false);
        matchpart.SetActive(true);
        PanelAnim.ShowPanel();
        
        transform.GetComponent<Showgril>().TieCloth[transform.GetComponent<Showgril>().CurrentTieCloth].transform.GetChild(0).GetComponent<P3dPaintableTexture>().SaveSXM();
        
    }
}
