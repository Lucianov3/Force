using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEditorButtonScript : MonoBehaviour
{
    public string LevelName;

    public Text Text;

    public void SetLevelName()
    {
        LevelName = Text.text;
    }

    public void LoadLevel()
    {
        GameManager.Editor.EditLevel.LoadLevelFromJson(Application.dataPath + "/Levels/" + LevelName + ".txt");
        GameManager.LoadLevelIntoMapEditor();
    }

    public void SaveLevel()
    {
        GameManager.Editor.EditLevel.SaveLevelToJson(Application.dataPath + "/Levels/" + LevelName + ".txt");
    }
}