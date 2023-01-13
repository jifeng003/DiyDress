using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FingerEffect : MonoBehaviour
{
    /// <summary>
    /// 滑动事件
    /// </summary>
    public UnityEvent fingerDownEvent = new UnityEvent();

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private Vector3 _downMousePos;

    private float delayTime = .1f;

    /// <summary>
    /// 操作模式
    /// </summary>
    public OpreateMode opreateMode;
    
    public enum OpreateMode
    {
        SlideDown, //向下滑动
        Hold,//长按
    }
    

    private bool _isDown;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //鼠标按下
            _downMousePos =Input.mousePosition;
            _isDown = true;
        }
        
        //手指按下
        if (_isDown)
        {
            delayTime -= Time.deltaTime;
            if (delayTime <= 0)
            {
                delayTime = .1f;
                _downMousePos = Input.mousePosition;
            }

            switch (opreateMode)
            {
                case OpreateMode.SlideDown:
                    if (IsSliderDown())
                    {
                        MouseUp();
                    }
                    break;
                case OpreateMode.Hold:
                    MouseUp();//直接回调
                    break;
            }
            MoveTrailRender();
        }

        if (Input.GetMouseButtonUp(0) && _isDown)
        {
            _isDown = false;
            
            if (IsSliderDown())
            {
                MouseUp();
            }
        }
    }

    public TrailRenderer trailRenderer;

    void MoveTrailRender()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        trailRenderer.transform.position = ray.origin + ray.direction * dis;
    }

    [SerializeField] private float dis = 0.2f;
    
    
    public float maxDis = 4f;

    bool IsSliderDown()
    {
        Vector2 sliderDir = Input.mousePosition - _downMousePos;
        if (sliderDir.magnitude > maxDis)
        {
            if (Vector2.Dot(Vector2.down, sliderDir) >= 0)
            {
                return true;
            }
        }
        return false;
    }

    public Renderer renderer;
    private bool i;
    
    /// <summary>
    /// 鼠标抬起
    /// </summary>
    public void MouseUp()
    {
        //回调一次
        _downMousePos = Input.mousePosition;
        fingerDownEvent?.Invoke();

        return;
        if (i)
        {
            renderer.sharedMaterial.color = Color.blue;
        }
        else
        {
            renderer.sharedMaterial.color = Color.green;
        }

        i = !i;
    }

    
    
}
