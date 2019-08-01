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
            LevelObject asset = LevelObject.CreateInstance<LevelObject>();
            asset.Id = myTarget.ObjectForLoadingLevels.Count + 1;
            AssetDatabase.CreateAsset(asset, "Assets/Prefabs/LevelObjects/ScriptableObjects/NewObject.asset");

            AssetDatabase.SaveAssets();
            myTarget.ObjectForLoadingLevels.Add(asset);
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;
        }
    }
}
