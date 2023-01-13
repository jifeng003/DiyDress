using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ZYB;

public class ClaimBanknoteEffect : MonoBehaviour
{
    public RectTransform claimEffect;
    public TextMeshProUGUI claimValueText;

    private Tween _tween1;
    private Tween _tween2;
    private Tween _tween3;
    private Tween _tween4;

    public void ShowEffect(Vector3 worldPosition, int value )
    {
        _tween1?.Kill();
        _tween2?.Kill();
        _tween3?.Kill();
        _tween4?.Kill();

        claimValueText.text = $"+ {value}";

        var randomOffset = new Vector2(Random.Range(-25, 25f), Random.Range(-25, 25f));

        var targetPosition =  GameManager.WorldToUGUI(worldPosition) + randomOffset;;
        claimEffect.anchoredPosition = targetPosition;
        claimEffect.transform.localScale = Vector3.zero;
        claimValueText.color = new Color(claimValueText.color.r, claimValueText.color.g, claimValueText.color.b, 1);
        claimEffect.gameObject.SetActive(true);

        _tween1 = claimEffect.transform.DOScale(1, 0.4f).SetEase(Ease.OutBack).OnComplete(() => {
            _tween4 = claimValueText.DOFade(0, 0.5f).SetEase(Ease.Linear).OnComplete(() => {
                claimEffect.gameObject.SetActive(false);
            });


        });
        _tween2 = claimEffect.DOAnchorPosY(claimEffect.anchoredPosition.y + 300, 2).SetEase(Ease.Linear);

    }
    
    
    public void ShowEffect1(Vector3 ScreenPosition, int value )
    {
        _tween1?.Kill();
        _tween2?.Kill();
        _tween3?.Kill();
        _tween4?.Kill();

        claimValueText.text = $"+ {value}";

        var randomOffset = new Vector2(Random.Range(-25, 25f), Random.Range(-25, 25f));

        var targetPosition =  GameManager.ScreenToUGUI(ScreenPosition) + randomOffset;;
        claimEffect.anchoredPosition = targetPosition;
        claimEffect.transform.localScale = Vector3.zero;
        claimValueText.color = new Color(claimValueText.color.r, claimValueText.color.g, claimValueText.color.b, 1);
        claimEffect.gameObject.SetActive(true);

        _tween1 = claimEffect.transform.DOScale(1, 0.4f).SetEase(Ease.OutBack).OnComplete(() => {
            _tween4 = claimValueText.DOFade(0, 0.5f).SetEase(Ease.Linear).OnComplete(() => {
                claimEffect.gameObject.SetActive(false);
            });


        });
        _tween2 = claimEffect.DOAnchorPosY(claimEffect.anchoredPosition.y + 300, 2).SetEase(Ease.Linear);

    }

}
