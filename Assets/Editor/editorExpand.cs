using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(titlePanel))]
public class editorExpand : Editor
{
    
    #if UNITY_EDITOR
        public override void OnInspectorGUI() {
            DrawDefaultInspector();
    
            titlePanel myScript = (titlePanel)target;
            if(GUILayout.Button("整理列表")) {
                myScript.titleSeq(myScript.getTiltle());
            }
        } 
    #endif
    
}
