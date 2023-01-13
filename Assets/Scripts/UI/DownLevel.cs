using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DownLevel : MonoBehaviour
{
    public GameObject NEXTBtn;
    public GameObject WINImag;
    // Start is called before the first frame update
    private void Start()
    {
        float timeCount = 0;
        NEXTBtn.SetActive(false);
        WINImag.SetActive(false);
        DOTween.To(() => timeCount, a => timeCount = a, 1f, 1f).OnComplete(new TweenCallback(delegate
                 {
                     NEXTBtn.SetActive(true);
                     WINImag.SetActive(true);
                 }));
    }
    public void DownLevelClick()
    {
        //GameManager.Instance.SetDownLevel();
    }
}
