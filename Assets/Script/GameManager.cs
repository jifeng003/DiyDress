using FluffyUnderware.Curvy;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[Serializable]
  struct CameraPos
{
    public Vector3 iPadPos;
    public Vector3 AndroidNarrowPos;
    public Vector3 iPhoneXPos;
    public Vector3 NormalPos;
    public Vector3 SuperNarrowPos;

}
[DefaultExecutionOrder(-100)]
public class GameManager : Singleleton<GameManager>
{
    [SerializeField] private CameraPos cameraPos;
    [HideInInspector] public Camera cameraMain;
    public GameObject star;
    public ParticleSystem particleCreatPeople;
    public ParticleSystem particleCreatModel;
    private string xmlPath;
    public MainToIdel Canvas;
    protected override void Awake()
    {
        
        base.Awake();
        
        cameraMain = Camera.main;
    }
    public JsonSave jsonSave;
    public Button BackButton;
    public GameObject IdlePart;
    public GameObject IdleButton;
    public GameObject LevelShow;
    public GameObject IdleGuide;
    public Camera UIcamera;
    
        
   
    public GameObject LockImg;
    private void Start()
    {
        BackButton.gameObject.SetActive(false);
        LevelManager.Instance.LoadLevel();
    }
    
    
    private static Vector2 _canvasSize;

    /// <summary>
    /// Convert the world coordinate system to the screen coordinate system
    /// </summary>
    /// <returns></returns>
    public static Vector2 WorldToUGUI(Vector3 _vector3)
    {
        Vector2 screenPosition = Vector2.zero.normalized;
        var tempCamera = Camera.main;
        if (tempCamera)
        {
            screenPosition = tempCamera.WorldToScreenPoint(_vector3);
        }

        Vector2 screenPosition2;
        screenPosition2.x = screenPosition.x - (Screen.width * 0.5f);
        screenPosition2.y = screenPosition.y - (Screen.height * 0.5f);
        Vector2 finalUIPosition;
        finalUIPosition.x = (screenPosition2.x / Screen.width) * CanvasSize.x;
        finalUIPosition.y = (screenPosition2.y / Screen.height) * CanvasSize.y;
        return finalUIPosition;
    }

    /// <summary>
    /// Convert the world coordinate system to the screen coordinate system
    /// </summary>
    /// <returns></returns>
    public static Vector2 ScreenToUGUI(Vector3 _vector3)
    {
        Vector2 screenPosition = Vector2.zero.normalized;
        var tempCamera = Camera.main;
        if (tempCamera)
        {
            screenPosition = _vector3;
        }

        Vector2 screenPosition2;
        screenPosition2.x = screenPosition.x - (Screen.width * 0.5f);
        screenPosition2.y = screenPosition.y - (Screen.height * 0.5f);
        Vector2 finalUIPosition;
        finalUIPosition.x = (screenPosition2.x / Screen.width) * CanvasSize.x;
        finalUIPosition.y = (screenPosition2.y / Screen.height) * CanvasSize.y;
        return finalUIPosition;
    }
    
    
    private static Vector2 CanvasSize
    {
        get
        {
            if (_canvasSize.x < 10)
            {
                _canvasSize = GameObject.Find("Canvas_UIManager").GetComponent<RectTransform>().sizeDelta;
            }
            return _canvasSize;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            string directoryName = Screen.width + "x" + Screen.height;
            string path = Application.dataPath.Replace("/Assets", "/" + Application.productName + "_Screenshot/" + directoryName);
            string imageName = directoryName + "_" + System.Guid.NewGuid() + ".png";

            int fileCount = System.IO.File.Exists(path) ?
                new System.IO.DirectoryInfo(path).GetFiles().Length
                : System.IO.Directory.CreateDirectory(path).GetFiles().Length;

            ScreenCapture.CaptureScreenshot(path + "/" + imageName);
            Debug.Log("***截图成功:" + imageName + "  |***存放路径" + path + "  |***该尺寸数量" + (fileCount + 1));
        }
    }

    
    
    

}