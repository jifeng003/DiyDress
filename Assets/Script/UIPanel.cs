using UnityEngine;
using System;
namespace Lww
{
    public interface IUIPanelComponents
    {
    }

    public class UIPanel : MonoBehaviour
    {

        [Header("Panel是否显示中")]
        public bool isShowIng;

        [Header("使用UIManager管理")]
        public bool addToUIManager=true;


        [HideInInspector] public RectTransform panelRect;

        public Action<object> GetOnPanelHideAction { get;private set; }
        public Action GetOnPanelShowAction { get;private set; }

        protected object[] Args;
        protected object hideArg;
        protected virtual void Awake()
        {
            panelRect = GetComponent<RectTransform>();
            isShowIng = true;
        }

        public UIPanel Show(params object[] args)
        {
            if(args.Length>0)
                Args = args;
            isShowIng = true;
            GetOnPanelShowAction = null;
            GetOnPanelHideAction = null;
            LShow();
            return this;
        }

        public UIPanel Hide(object arg=null)
        {
            hideArg = arg;
            isShowIng = false;
            LHide();
            return this;
        }

   



        protected virtual void LShow()
        {
            gameObject.SetActive(true);
        }

        protected virtual void LHide()
        {
            gameObject.SetActive(false);
            InvokeHideAction();
        }

        protected virtual void BindButtonEvent()
        {
        }


        protected void InvokeShowAction()
        {
            GetOnPanelShowAction?.Invoke();

        }

        protected void InvokeHideAction()
        {
            GetOnPanelHideAction?.Invoke(hideArg);

        }

        public UIPanel OnPanelShow(Action action)
        {
            GetOnPanelShowAction = action;
            return this;
        }
        
        public UIPanel OnPanelHide(Action<object> action)
        {
            GetOnPanelHideAction = action;
            return this;
        }

        
    }
}