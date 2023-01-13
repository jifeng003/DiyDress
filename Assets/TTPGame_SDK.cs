using System;
using System.Collections.Generic;
using Tabtale.TTPlugins;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace SXM
{
    public class TTPGame_SDK : MonoBehaviour
    {
        
        

        private Dictionary<string,object> parameters;
        
        /// <summary>
        /// 开始新关卡
        /// </summary>
        public void OnMissionStarted(int levelId)
        {
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
            Debug.Log("游戏成功");

            TTPGameProgression.FirebaseEvents.MissionComplete(parameters);
        }
        
        
        /// <summary>
        /// 任务失败
        /// </summary>
        public  void OnMissionFailed()
        {
            
            TTPGameProgression.FirebaseEvents.MissionFailed(parameters);//游戏失败
        }
    }

}

