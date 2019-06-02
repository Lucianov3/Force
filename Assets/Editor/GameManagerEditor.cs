using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GameManager myTarget = (GameManager)target;
        if(GUILayout.Button("Create New Object"))
        {
            myTarget.CreateNewObject();
        }
    }
}
