using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum XYZ
{
    X,
    Y,
    Z,
    none
}
public class ButtonTool : MonoBehaviour
{
    Vector3 offset;
    public string destinationTag = "Cup";
    public XYZ Axis;
    public Transform Decal;
    // private void Awake()
    // {
    //     Move = true;
    // }


    public Vector3 StartPosition;
    public RectTransform UI;
    public bool drag;
    private void Update()
    {
        // if (Move)
        // {
        //     transform.position = Input.mousePosition + new Vector3(50, 50, 0);
        // }
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("OnmouseButtonDown");
            transform.position = MouseWorldPosition();
            offset = transform.position - MouseWorldPosition();
            StartPosition = MouseWorldPosition();
            transform.GetComponent<Collider>().enabled = false;
        }

        if (Input.GetMouseButton(0))
        {
            
        }
    }

    //public bool Move;
    private void OnMouseDown()
    {
        Debug.Log("OnmouseButtonDown");
        transform.position = MouseWorldPosition();
        offset = transform.position - MouseWorldPosition();
        StartPosition = MouseWorldPosition();
        transform.GetComponent<Collider>().enabled = false;
    }

    private void OnMouseDrag()
    {
        AxisMoveChange();
        float distance = Vector3.Distance(transform.position, StartPosition);
        Debug.Log(distance);
    }
    
    private void OnMouseUp()
    {
        //Move = true;
        UpJudge();
        transform.GetComponent<Collider>().enabled = true;
    }

    // private void OnMouseDown()
    // {
    //     offset = transform.position - MouseWorldPosition();
    //     transform.GetComponent<Collider>().enabled = false;
    // transform.GetComponent<Collider>().enabled = false;
    // }
    // private void OnMouseDrag()
    // {
    //     AxisMoveChange();
    // }
    // private void OnMouseUp()
    // {
    //     UpJudge();
    //     transform.GetComponent<Collider>().enabled = true;
    //     
    // }
    
    
    /// <summary>
    /// 鼠标位置转3D位置
    /// </summary>
    /// <returns> 转换后位置</returns>
    private Vector3 MouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }
    
    
    /// <summary>
    /// 选择忽略移动轴向
    /// 拖拽
    /// </summary>
    public Vector2 RangeX;
    public Vector2 RangeY;
    public Vector2 RangeZ;
    private void AxisMoveChange()
    {
        //移动限制
        float clampX  = Mathf.Clamp(MouseWorldPosition().x + offset.x,RangeX.x,RangeX.y) ;
        float clampY  = Mathf.Clamp(MouseWorldPosition().y + offset.y,RangeY.x,RangeY.y) ;
        float clampZ  = Mathf.Clamp(MouseWorldPosition().z + offset.z,RangeZ.x,RangeZ.y) ;

        
        if (Axis == XYZ.X)
        {
            transform.position = new Vector3(transform.position.x, clampY, clampZ);
        }else if (Axis == XYZ.Y)
        {
            transform.position = new Vector3( clampX, transform.position.y, clampZ);
        }
        else if (Axis == XYZ.Z)
        {
            transform.position = new Vector3(clampX, clampY, transform.position.z);
        }
        else if(Axis == XYZ.none)
        {
            transform.position = new Vector3(clampX, clampY, clampZ);
        }
    }
    /// <summary>
    /// 抬起判断
    /// </summary>
    private void UpJudge()
    {
        var rayOrigin = Camera.main.transform.position;
        var rayDirection = MouseWorldPosition() - Camera.main.transform.position;
        RaycastHit hitInfo;
        if (Physics.Raycast(rayOrigin, rayDirection, out hitInfo))
        {
            if (hitInfo.transform.tag == destinationTag)
            { 
                Debug.Log(transform.name);
                //transform.position = hitInfo.transform.position;
            }
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color=Color.green;//更改颜色
        //Gizmos.DrawCube(transform.position, new Vector3(1,1,2));
        Gizmos.DrawLine(transform.position,transform.position - new Vector3(0,0,-3));
    }
}