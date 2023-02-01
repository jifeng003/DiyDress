using System;
using System.Collections.Generic;
using Tabtale.TTPlugins;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace SXM
{
    public class TTPGame_SDK : MonoBehaviour
    {
        private bool isSdkIns;
        private void Awake()
        {
            // Initialize CLIK Plugin   
            TTPCore.Setup(); //放在首行
            isSdkIns = true;
            Debug.Log("SDK初始化");
            // Your code here
        }

        private Dictionary<string,object> parameters;
        
        /// <summary>
        /// 开始新关卡
        /// </summary>
        public void OnMissionStarted(int levelId)
        {
            if (!isSdkIns) return;
        
            Debug.Log("开始游戏");
            parameters  = new Dictionary<string, object>();

            if (levelId == 1)
            {
                Debug.Log("初次游戏");
                parameters.Add("missionName","First Level");
            }
                
            TTPGameProgression.FirebaseEvents.MissionStarted(levelId, parameters);

        }
        
        /// <summary>
        /// 任务成功
        /// </summary>
        public  void OnMissionComplete()
        {
            if (!isSdkIns) return;

            Debug.Log("游戏成功");

            TTPGameProgression.FirebaseEvents.MissionComplete(parameters);
        }
        
        
        /// <summary>
        /// 任务失败
        /// </summary>
        public  void OnMissionFailed()
        {
            if (!isSdkIns) return;

            TTPGameProgression.FirebaseEvents.MissionFailed(parameters);//游戏失败
        }
    }

}

