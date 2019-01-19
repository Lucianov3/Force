using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class save : MonoBehaviour
{
    private Level level;

    private void Start()
    {
        level = new Level();
        Debug.Log(Application.dataPath + "/Levels/level.txt");
        level.LoadLevelFromJson(Application.dataPath + "/Levels/level.txt");
    }

    private void Update()
    {
        Debug.Log(level.Content[0, 5, 7].Object);
    }
}