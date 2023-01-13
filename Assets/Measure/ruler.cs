using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ruler : MonoBehaviour
{
    #region 字段
    public GameObject juanchi;
    public GameObject juanchichild;
    public GameObject chi01;
    public GameObject chi02;
    public GameObject chi03;
    public GameObject sliderObj;
    public GameObject downBtn;

    public Animator chi01animator;
    public Animator chi02animator;
    public Animator chi03animator;

    public MeshRenderer juanchimeshRenderer;

    public Estage estage = Estage.xiong;

    [Header("初始卷尺长度/卷尺长度")]
    public float rulerLength = 0.5f;                  //尺子长度
    /*[Header("最短卷尺长度")]
    public float rulerShortLength = 0.6f;                  //尺子长度*/
    [Header("卷尺移动速度")]
    public float pressSpeed = 0.5f;                 //按压速度
    [Header("尺子1最终长度")]
    public float chi01FinalLength = 0.7f;
    [Header("尺子2最终长度")]
    public float chi02FinalLength = 0.6f;
    [Header("尺子3最终长度")]
    public float chi03FinalLength = 0.7f;


    [Header("人物视线目标")]
    public Transform targetPosition;
    [Header("胸围人物视线")]
    public Vector3 xiongvector3;
    [Header("腰围人物视线")]
    public Vector3 yaovector3;
    [Header("臀围人物视线")]
    public Vector3 tunvector3;


    [Header("刻度条")]
    public Slider slider;
    [Header("刻度条文字")]
    public Text slidertext;
    public float[] Alength;
    public float[] Blength;
    public float[] Clength;
    public float[] Dlength;
    public float[] Elength;
    public GameObject grape;
    public GameObject lemon;
    public GameObject orange;
    public GameObject melon;
    GameObject grapeText;
    GameObject lemonText;
    GameObject orangeText;
    GameObject melonText;
    [Header("显示米数")]
    public Text measureText01;
    public Text measureText02;
    public RectTransform Textimage;
    public GameObject measureText;
    bool isMoveText;


    RectTransform grapeimage;
    RectTransform lemonimage;
    RectTransform orangeimage;
    RectTransform melonimage;
    public Camera UIcamera;
    public GameObject fruitTarget;
    public GameObject textTarget;
    public Canvas uicanvas;
    Vector2 mouDown;
    bool isMovefruit;
    bool isFixfruit;
    bool isShowFeedback;
    #endregion

    public enum Estage             //测量阶段
    {
        xiong,
        yao,
        tun,
        final
    }

    public List<RectTransform> feedbackListUp = new List<RectTransform>();
    public List<RectTransform> feedbackListMiddle = new List<RectTransform>();
    public List<RectTransform> feedbackListDown = new List<RectTransform>();
    public GameObject feedbackListUptarget;
    public GameObject feedbackListMiddletarget;
    public GameObject feedbackListDowntarget;

    public bool isChangeEstage;

    #region 回调函数
    void Start()
    {
        
        juanchi.SetActive(true);
        chi01.SetActive(false);
        chi02.SetActive(false);
        chi03.SetActive(false);
        downBtn.SetActive(false);

        grapeimage = grape.GetComponent<RectTransform>();
        lemonimage = lemon.GetComponent<RectTransform>();
        orangeimage = orange.GetComponent<RectTransform>();
        melonimage = melon.GetComponent<RectTransform>();

        juanchimeshRenderer.material.SetTextureOffset("_BaseMap", new Vector2(rulerLength, 0));
        Move(CameraPos1, new Vector3(0.1868f, 1.279f, -0.2f));

        measureText01.gameObject.SetActive(false);
        measureText02.gameObject.SetActive(false);
        grapeText = grape.GetComponentInChildren<Text>().gameObject;
        lemonText = lemon.GetComponentInChildren<Text>().gameObject;
        orangeText = orange.GetComponentInChildren<Text>().gameObject;
        melonText = melon.GetComponentInChildren<Text>().gameObject;
        grape.SetActive(false);
        lemon.SetActive(false);
        orange.SetActive(false);
        melon.SetActive(false);
        

        for (int i = 0; i < feedbackListUp.Count; i++)
        {
            feedbackListUp[i].gameObject.SetActive(false);
            feedbackListMiddle[i].gameObject.SetActive(false);
            feedbackListDown[i].gameObject.SetActive(false);
        }

    }

    public Vector3 CameraPos1;
    public Vector3 CameraPos2;
    public Vector3 CameraPos3;

    public GameObject Tips;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Tips)
        {
            Tips.SetActive(false);
        }
        isFixfruit = false;
        MoveFeedBack(feedbackListUp,feedbackListUptarget);
        MoveFeedBack(feedbackListMiddle, feedbackListMiddletarget);
        MoveFeedBack(feedbackListDown, feedbackListDowntarget);
        if (isMoveText)
        {
            //Debug.Log("执行了MoveText");
            MoveText(Textimage, measureText,textTarget);
        }

        if (isMovefruit)
        {
            if (grape.activeSelf)
            {
                MoveFruits(grapeimage,grape,grapeText ,new Vector3(0.9f, 0.9f, 1));
            }
            if (lemon.activeSelf)
            {
                MoveFruits(lemonimage,lemon,lemonText, new Vector3(0.8f, 0.8f, 1));
            }
            if (orange.activeSelf)
            {
                MoveFruits(orangeimage,orange,orangeText, new Vector3(0.5f, 0.5f, 1));
            }
            if (melon.activeSelf)
            {
                MoveFruits(melonimage,melon,melonText, new Vector3(0.5f, 0.5f, 1));
            }
        }

        switch (estage)
        {
            case Estage.xiong:
                PressRuler(chi01FinalLength);
                DoMeasure(chi01, chi01FinalLength, Estage.yao, CameraPos2, new Vector3(0.1868f, 1.175f, -0.2f), chi01animator);

                targetPosition.DOMove(xiongvector3, 1f);
                break;
            case Estage.yao:
                PressRuler(chi02FinalLength);
                DoMeasure(chi02, chi02FinalLength, Estage.tun, CameraPos3, new Vector3(0.1868f, 1.004f, -0.2f), chi02animator);

                targetPosition.DOMove(yaovector3, 1f);
                break;
            case Estage.tun:
                PressRuler(chi03FinalLength);
                DoMeasure(chi03, chi03FinalLength, Estage.final, CameraPos1, new Vector3(0.1868f, 0.3f, -0.2f), chi03animator);

                targetPosition.DOMove(tunvector3, 1f);
                break;
            case Estage.final:
                targetPosition.DOMove(xiongvector3, 1f);
                juanchi.SetActive(false);
                sliderObj.SetActive(false);
                downBtn.SetActive(true);
                break;
            default:
                break;
        }
    }
    #endregion



    #region 方法
    /// <summary>
    /// 按压尺子
    /// </summary>
    /// <param name="rulerShortLength">当前尺子长度</param>
    void PressRuler(float rulerShortLength)
    {
        if (Input.GetMouseButton(0))
        {
            VibratorManager.UpdateTrigger(1);
            //Debug.Log("按下了");
            isChangeEstage = false;
            isShowFeedback = false;
            rulerLength += Time.deltaTime * pressSpeed;
            if (rulerLength > 1f)
            {
                rulerLength = 1f;
            }
            juanchimeshRenderer.material.SetTextureOffset("_BaseMap", new Vector2(rulerLength, 0));
            ShowSilderObj();
        }
        else
        {
            //Debug.Log("松开了");
            if (rulerLength < rulerShortLength)
            {
                rulerLength -= Time.deltaTime * pressSpeed;
                if (rulerLength < 0.5f)
                {
                    rulerLength = 0.5f;
                }
                juanchimeshRenderer.material.SetTextureOffset("_BaseMap", new Vector2(rulerLength, 0));
            }
            else
            {
                isShowFeedback = true;
            }


            switch (estage)
            {
                case Estage.xiong:
                    if(isShowFeedback)
                    {
                        ShowFeedback(feedbackListUp);
                    }
                    
                    break;
                case Estage.yao:
                    if (isShowFeedback)
                    {
                        ShowFeedback(feedbackListMiddle);
                    }
                    break;
                case Estage.tun:
                    if (isShowFeedback)
                    {
                        ShowFeedback(feedbackListDown);
                    }
                    break;
                case Estage.final:
                    break;
                default:
                    break;
            }

            isChangeEstage = true;


            if (estage==Estage.yao)
            {
                isMoveText = true;
            }
            measureText02.gameObject.SetActive(false);

            
        }

    }

    /// <summary>
    /// 三围测量
    /// </summary>
    /// <param name="_ruler">尺子序号</param>
    /// <param name="_finalLength">最小合适长度</param>
    /// <param name="_estage">下一个要测试的枚举值</param>
    /// <param name="_cameravector3">相机移动位置</param>
    /// <param name="_chizivector3">卷尺移动位置</param>
    /// <param name="animator">对应尺子动画</param>
    void DoMeasure(GameObject _ruler, float _finalLength, Estage _estage, Vector3 _cameravector3, Vector3 _chizivector3, Animator animator)
    {

        if (rulerLength >= _finalLength && Input.GetMouseButtonUp(0))
        {
            Debug.Log("臀围测量成功");
            
            Emoji[currentNumber].Play();
            _ruler.SetActive(true);
            animator.speed = 0.5f;
            animator.Play(0);
            _ruler.GetComponent<SkinnedMeshRenderer>().material.SetTextureOffset("_BaseMap", new Vector2(rulerLength, 0));
            rulerLength = 0.5f;
            juanchimeshRenderer.material.SetTextureOffset("_BaseMap", new Vector2(rulerLength, 0));
            estage = _estage;
            Move(_cameravector3, _chizivector3);

            //melon.transform.position= UIcamera.WorldToScreenPoint(melonTransform.position);
            isMovefruit = true;
        }
    }

    /// <summary>
    /// 相机和卷尺具体移动函数
    /// </summary>
    /// <param name="_cameravector3">相机要移动到的位置</param>
    /// <param name="_chizivector3">卷尺要移动到的位置</param>
    void Move(Vector3 _cameravector3, Vector3 _chizivector3)
    {
        Camera.main.transform.DOMove(_cameravector3, 1f);
        juanchi.transform.DOMove(_chizivector3, 1f);
    }


    /// <summary>
    /// 显示量胸围时的水果
    /// </summary>
    void ShowSilderObj()
    {
        double measureUSA = (rulerLength - 0.5) * 80;
        //Debug.Log(measureUSA);
        ShowMeasureText(measureUSA);
        if (measureUSA > Alength[(int)estage])
        {
            /*if (!isShowFeedBack02 && !isShowFeedBack03 && !isShowFeedBack04 && !isShowFeedBack05)
            {
                ShowFeedback(1);
                isShowFeedBack02 = true;
            }*/
            if (estage == Estage.xiong)
            {
                grape.SetActive(true);
                lemon.SetActive(false);
                orange.SetActive(false);
                melon.SetActive(false);
            }
        }
        if (measureUSA > Blength[(int)estage])
        {
            /*if (!isShowFeedBack03 &&  !isShowFeedBack04 && !isShowFeedBack05)
            {
                ShowFeedback(2);
                isShowFeedBack03 = true;
            }*/
            if (estage == Estage.xiong)
            {
                lemon.SetActive(true);
                grape.SetActive(false);
                orange.SetActive(false);
                melon.SetActive(false);
            }
            
        }
        if (measureUSA > Clength[(int)estage])
        {
            /*if (!isShowFeedBack04 && !isShowFeedBack05)
            {
                ShowFeedback(3);
                isShowFeedBack04 = true;
            }*/
            if (estage == Estage.xiong)
            {
                orange.SetActive(true);
                grape.SetActive(false);
                lemon.SetActive(false);
                melon.SetActive(false);
            }

        }
        if (measureUSA > Dlength[(int)estage])
        {
            /*if (!isShowFeedBack05)
            {
                ShowFeedback(4);
                isShowFeedBack05 = true;
            }*/
            if (estage == Estage.xiong)
            {
                melon.SetActive(true);
                grape.SetActive(false);
                lemon.SetActive(false);
                orange.SetActive(false);
            }

        }

    }


    /// <summary>
    /// 量完胸围要移动水果的函数
    /// </summary>
    /// <param name="_image">移动的水果位置</param>
    /// <param name="_gameObject">缩放的物体</param>
    public void MoveFruits(RectTransform _image,GameObject _gameObject,GameObject _childernObj,Vector3 _vector3)
    {
        
        _childernObj.SetActive(false);
        mouDown = Camera.main.WorldToScreenPoint(fruitTarget.transform.position);
        Vector2 mouseUGUIPos = new Vector2();
        //Debug.Log(mouseUGUIPos);
        bool isrect = RectTransformUtility.ScreenPointToLocalPointInRectangle(uicanvas.transform as RectTransform, mouDown, UIcamera, out mouseUGUIPos);
        if (isrect && !isFixfruit)
        {
            _gameObject.transform.DOScale(_vector3, 1);
            float timeCount=0;
            _image.DOAnchorPos(mouseUGUIPos, 0.5f).OnComplete(() => {
                DOTween.To(() => timeCount, a => timeCount = a, 1f, 2f).OnComplete(() =>
                {
                    isFixfruit = true;
                });
            });
            
            

        }
        if (isFixfruit)
        {
            //Debug.Log("Asddddddddddddddddddddd");
            _image.anchoredPosition = mouseUGUIPos;
        }
    }


    /// <summary>
    /// 量完胸围要移动刻度的函数
    /// </summary>
    /// <param name="_image">移动的刻度位置</param>
    /// <param name="_gameObject">缩放的物体</param>
    public void MoveText(RectTransform _image, GameObject _gameObject,GameObject _textTarget)
    {
        mouDown = Camera.main.WorldToScreenPoint(_textTarget.transform.position);
        Vector2 mouseUGUIPos = new Vector2();
        bool isrect = RectTransformUtility.ScreenPointToLocalPointInRectangle(uicanvas.transform as RectTransform, mouDown, UIcamera, out mouseUGUIPos);
        if (isrect && !isFixfruit)
        {
            float timeCount = 0;
            _image.DOAnchorPos(mouseUGUIPos, 0.5f).OnComplete(() => {
                DOTween.To(() => timeCount, a => timeCount = a, 1, 3).OnComplete(() =>
                {
                    isFixfruit = true;
                });
            });
            _gameObject.transform.DOScale(new Vector3(1, 1, 1), 1);
        }
        if (isFixfruit)
        {
            _image.anchoredPosition = mouseUGUIPos;
        }
    }


    /// <summary>
    /// 显示量三围时的刻度
    /// </summary>
    /// <param name="_measureUSA">刻度换算为国际单位</param>
    public void ShowMeasureText(double _measureUSA)
    {

        if(estage==Estage.xiong)
        {
            measureText01.gameObject.SetActive(true);
            measureText01.text = (_measureUSA.ToString("F0") + " in");
        }
        else
        {
            measureText02.gameObject.SetActive(true);
            measureText02.text = (_measureUSA.ToString("F0") + " in");
        }
        
    }

    public List<ParticleSystem> Emoji;
    public int currentNumber;
    public void ShowFeedback(List<RectTransform> _feedbackList)
    {
        Debug.Log("执行了委托");
        Action<int> action = delegate (int num)
        {
            
            for (int i = 0; i < _feedbackList.Count; i++)
            {
                if (i == num)
                {
                    if (!_feedbackList[i].gameObject.activeSelf)
                    {
                        _feedbackList[i].gameObject.SetActive(true);
                        Debug.Log("显示",_feedbackList[i].gameObject);
                    }
                }
                else
                {
                    _feedbackList[i].gameObject.SetActive(false);
                }
            }
        };

        double measureUSA = (rulerLength - 0.5) * 80;
        if (measureUSA > Alength[(int)estage])
        {
            action(0);
            currentNumber = 0;
        }
        if (measureUSA > Blength[(int)estage])
        {
            action(1);
            currentNumber = 0;

        }
        if (measureUSA > Clength[(int)estage])
        {
            action(2);
            currentNumber = 1;

        }
        if (measureUSA > Dlength[(int)estage])
        {
            action(3);
            currentNumber = 2;

        }
        if (measureUSA > Elength[(int)estage])
        {
            action(4);
            currentNumber = 2;

        }

    }


    public void MoveFeedBack(List<RectTransform> _feedbackList,GameObject _target)
    {
        for (int i = 0; i < _feedbackList.Count; i++)
        {
            MoveText(_feedbackList[i], _feedbackList[i].gameObject, _target);
        }
    }
    #endregion

}
