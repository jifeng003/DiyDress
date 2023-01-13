using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using FluffyUnderware.Curvy.Controllers;
using UnityEngine.PlayerLoop;
using ZYB;

[Serializable]
public class ModelChoth
{
    public bool isOn;
    public int ClothNum;
}
[Serializable]
public class Model
{
    public ModelChoth iszhazhen;
    public ModelChoth iscaijian;
    public ModelChoth isTie;
    public ModelChoth S1;
    public ModelChoth X1;
    public ModelChoth xie;
    public ModelChoth Hair;
    public ModelChoth toushi;
    public ModelChoth shoushi;
    public ModelChoth baobao;
    public ModelChoth yanse;
    public ModelChoth Shoeyanse;
    public ModelChoth hairyanse;
    public ModelChoth niukou;
    public Vector3 NiukouPos;
    public Quaternion NiukouRoatate;
    public Vector3 NiukouScale;
    public Ecaizhi caizhi;
}
[Serializable]
public class ModelList
{
    public List<Model> Models = new List<Model>();
}
public class JsonSave : MonoBehaviour
{
    public ModelList datas;
    //public Model datas;//数据
    public string filePath;//存档路径
    //使用单例模式便于衣服更换的调用
    private static JsonSave _Ins;
    public List<Material> TieMaterials;
    public Texture Tie;
    public static JsonSave Ins
    {
        get
        {
            return _Ins;
        }
    }
    private void Awake()
    {
        filePath = Application.persistentDataPath + "/jsonDatas.json";
        if (!File.Exists(filePath))
        {
            Debug.Log("本地没有存档");
            SaveDatasJson();//存档
        }
        ReadDatasJson();//读档
        // MeshRenderer[] mesh = transform.GetChild(0).GetComponentsInChildren<MeshRenderer>();
        // foreach (var VARIABLE in mesh)
        // {
        //     TieMaterials.Add(VARIABLE.material);
        // }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            TieRefresh();
        }
    }

    public void TieRefresh()
    {
        PlayerPrefs.DeleteKey("I0");
        PlayerPrefs.DeleteKey("I1");
        PlayerPrefs.DeleteKey("I2");
        PlayerPrefs.DeleteKey("I21");
        PlayerPrefs.DeleteKey("I3");
        foreach (var VARIABLE in TieMaterials)
        {
            VARIABLE.SetTexture("_MainTex",Tie);
        }
        Debug.Log("已刷新贴纸");

    }

    //存储为Json格式Json文件    
    public void SaveDatasJson()
    {
        string json = JsonUtility.ToJson(datas);
        StreamWriter sw = new StreamWriter(filePath);
        sw.Write(json);
        sw.Close();
        Debug.Log("保存成功");
        Debug.Log(json);
    }
    //读取Json文件数据，并把数据转回目标格式赋值目标数据    
    public void ReadDatasJson()
    {
        if (!File.Exists(filePath))
        {
            Debug.Log("找不到存档文件");
        }
        else
        {
            if (Data.GetCurLevel == 0)
            {
                datas.Models.Clear();
                SaveDatasJson();
            }

            //根据文件路径创建数据流
            StreamReader sr = new StreamReader(filePath);
            //读取Json数据
            string ReadStr = sr.ReadToEnd();
            sr.Close();
            //将获取到的Json数据转为Datas类数据，使用到LitJson库
            ModelList tempDatas= JsonUtility.FromJson<ModelList>(ReadStr);
            
            Debug.Log(ReadStr);
            //把读取的数据赋值给目标数据
            datas= tempDatas;
 
            Debug.Log("本地存档读取赋值成功");
        }
    }
}
