using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMove : MonoBehaviour
{
    // Start is called before the first frame update
    public Texture2D cursor;
    public Texture2D clickcursor;

    public Texture2D cursorTexture;    //在外面为该变量赋值;

    public AudioClip audioClick;
    public AudioSource audioSource;

    [Header("是否启用小手")]
    public bool isHand;

    [Header("是否启用小手")]
    public bool isMusic;
    void OnGUI()
    {
        if(isHand)
        {
            Vector2 mouse_Pos = Input.mousePosition;
            if (!Input.GetMouseButton(0))
            {
                GUI.DrawTexture(new Rect(mouse_Pos.x, Screen.height - mouse_Pos.y, 100, 100), cursorTexture);   //绘制鼠标，鼠标的大小可以自己设置；
            }

            if (Input.GetMouseButton(0))
            {
                GUI.DrawTexture(new Rect(mouse_Pos.x, Screen.height - mouse_Pos.y, 100, 100), clickcursor);
            }
        }
        
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(isHand)
        {
            Cursor.visible = false;
        }
        
        if(Input.GetMouseButton(0) && isMusic)
        {
            audioSource.clip = audioClick;
            audioSource.Play();
        }
    }

}
