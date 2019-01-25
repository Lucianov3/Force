using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static List<GameObject> ObjectForLoadingLevels;

    [SerializeField] private List<GameObject> objectForLoadingLevels;

    public static LevelEditorScript Editor;

    public static bool isGravityOn = false;

    private void Start()
    {
        if (ObjectForLoadingLevels == null)
        {
            ObjectForLoadingLevels = objectForLoadingLevels;
        }
    }

    public static void LoadLevel()
    {
        for (int i = 0; i < Editor.EditLevel.Content.GetLength(0); i++)
        {
            for (int j = 0; j < Editor.EditLevel.Content.GetLength(1); j++)
            {
                for (int k = 0; k < Editor.EditLevel.Content.GetLength(2); k++)
                {
                    if (Editor.EditLevel.Content[i, j, k].Object != 0)
                    {
                        Instantiate(GameManager.ObjectForLoadingLevels[Editor.EditLevel.Content[i, j, k].Object - 1], i == 0 ? new Vector3(-31.5f + j, 1.0f + k) : new Vector3(-31.5f + j, -19.0f + k), Quaternion.Euler(new Vector3(0, Editor.EditLevel.Content[i, j, k].Rotation)));
                    }
                }
            }
        }
    }

    public static void LoadLevelIntoMapEditor()
    {
        for (int i = 0; i < Editor.EditLevel.Content.GetLength(0); i++)
        {
            for (int j = 0; j < Editor.EditLevel.Content.GetLength(1); j++)
            {
                for (int k = 0; k < Editor.EditLevel.Content.GetLength(2); k++)
                {
                    Editor.SetObjectInLevel(Editor.EditLevel.Content[i, j, k].Object, Editor.EditLevel.Content[i, j, k].Rotation, j, k, i);
                }
            }
        }
    }
}