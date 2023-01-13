using System.Collections.Generic;
using UnityEngine;
public enum DeviceType
{
    iPad,          // 3:4≈1.33 (典型分辨率：2048x2732，iPad Pro,iPad Air等)
    Normal,        // 9:16≈1.78 (典型分辨率：1242x2208，iPhone6/7/8，小米6等)
    AndroidNarrow, // 18:37≈2.06 (典型分辨率：1080x2220，三星S系列等)
    iPhoneX,       // 6:13≈2.17 (典型分辨率：1242x2688，iPhoneX/Xs/11等)
    SuperNarrow,   // >2.2 （超窄屏，极少数）
}
namespace Lww
{
   /// <summary>
   /// UI界面管理器
   /// </summary>
   [DefaultExecutionOrder(-99)]
   public class UIManager : MonoBehaviour
   {
   
       private UIPanel[] _panels;
       private Dictionary<string, UIPanel> _panelDict;
       private static UIManager _instance;


       public RectTransform[] UIS;
       public RectTransform[] MainUIS;
        
        public static DeviceType CurrentDevice
        {
            get
            {
                float hdw = Screen.height * 1.0f / Screen.width;
                if (hdw > 1.3f && hdw < 1.4f)//4:3
                {
                    //Debug.Log("iPad");
                    return DeviceType.iPad;
                }

                if (hdw > 1.7f && hdw < 1.8f)//16:9
                {
                    //Debug.Log("iPhone6/7/8");
                    return DeviceType.Normal;
                }

                if (hdw > 1.8f && hdw < 2.1f)//18:37
                {
                    //Debug.Log("AndroidNarrow");
                    return DeviceType.AndroidNarrow;
                }

                if (hdw > 2.1f && hdw < 2.2f)//6:13
                {
                    return DeviceType.iPhoneX;
                }

                if (hdw > 2.2f)//>2.2
                {
                    //Debug.Log("SuperNarrow");
                    return DeviceType.SuperNarrow;
                }

                return DeviceType.Normal;
            }
        }
       private static UIManager Instance
       {
           get
           {
               if (!_instance)
               {
                   _instance = FindObjectOfType<UIManager>().Init();
               }
               return _instance;
           }
       }

       public RectTransform PeoPleBG;
       private void Awake()
       {
           Init();
           PeoPleBG.sizeDelta = new Vector2(Screen.width, Screen.height / 4);
           if (CurrentDevice == DeviceType.iPhoneX)
           {
               foreach (var VARIABLE in UIS)
               {
                   VARIABLE.anchoredPosition =
                       new Vector2(VARIABLE.anchoredPosition.x, -105);
               }
               foreach (var VARIABLE in MainUIS)
               {
                   VARIABLE.localScale =
                       Vector2.one * .8f;
               }
           }
       }

       private UIManager Init()
       {
           if(_instance)
               return this;
           _instance = this;
           _panels=GetComponentsInChildren<UIPanel>(true);
           _panelDict = new Dictionary<string, UIPanel>();
           foreach (var panel in _panels)
           {
               if (panel.addToUIManager)
                   _panelDict[panel.GetType().ToString()] = panel;
               panel.isShowIng = panel.gameObject.activeInHierarchy;
           }

           return this;
       }

       /// <summary>
       /// 获取界面
       /// </summary>
       /// <typeparam name="T"></typeparam>
       /// <returns></returns>
       public static T GetPanel<T>() where T : UIPanel
       {
           T ret = (T)System.Convert.ChangeType(Instance._panelDict[typeof(T).ToString()], typeof(T));
           return ret;
       }
   
       public static void ShowPanel<T>() where T : UIPanel
       {
           Instance._panelDict[typeof(T).ToString()].Show();
       }
   
       public static void HidePanel<T>() where T : UIPanel
       {
           Instance._panelDict[typeof(T).ToString()].Hide();
       }
   }
 
}

