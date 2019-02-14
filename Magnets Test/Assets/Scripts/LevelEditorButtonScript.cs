using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelEditorButtonScript : MonoBehaviour
{
    public int LevelNumber = 0;

    public TextMeshProUGUI Text;

    public void CountLevelNumberUp()
    {
        if (LevelNumber < 99)
        {
            LevelNumber++;
        }
        Text.text = LevelNumber.ToString();
    }

    public void CountLevelNumbeDown()
    {
        if (LevelNumber > 0)
        {
            LevelNumber--;
        }
        Text.text = LevelNumber.ToString();
    }

    public void LoadLevel()
    {
        GameManager.Editor.EditLevel.LoadLevelFromJson(Application.dataPath + "/Levels/Level" + LevelNumber + ".txt");
        GameManager.LoadLevelIntoMapEditor();
    }

    public void SaveLevel()
    {
        GameManager.Editor.EditLevel.SaveLevelToJson(Application.dataPath + "/Levels/Level" + LevelNumber + ".txt");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}