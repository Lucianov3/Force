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

    public static void LoadLevel(Level level)
    {
        for (int i = 0; i < level.Content.GetLength(0); i++)
        {
            for (int j = 0; j < level.Content.GetLength(1); j++)
            {
                for (int k = 0; k < level.Content.GetLength(2); k++)
                {
                    if (level.Content[i, j, k].Object != 0)
                    {
                        GameObject obj = Instantiate(GameManager.ObjectForLoadingLevels[level.Content[i, j, k].Object - 1], i == 0 ? new Vector3(-31.5f + j, 1.0f + k) : new Vector3(-31.5f + j, -19.0f + k), Quaternion.Euler(new Vector3(0, 0, Editor.EditLevel.Content[i, j, k].Rotation)));
                        if (obj.GetComponent<GateScript>() != null)
                        {
                            obj.GetComponent<GateScript>().Channel = level.Content[i, j, k].Channel;
                        }
                        else if (obj.GetComponent<SwitchScript>() != null)
                        {
                            obj.GetComponent<SwitchScript>().Channel = level.Content[i, j, k].Channel;
                        }
                        else if (obj.GetComponent<PlayerPhysicController>() != null)
                        {
                            obj.GetComponent<Rigidbody>().isKinematic = false;
                            obj.GetComponent<PlayerPhysicController>().enabled = true;
                        }
                        else if (obj.transform.childCount > 0)
                        {
                            if (obj.transform.GetChild(0).GetComponent<TriggerSliderScript>() != null)
                            {
                                obj.transform.GetChild(0).GetComponent<TriggerSliderScript>().Channel = level.Content[i, j, k].Channel;
                            }
                            else if (obj.transform.GetChild(0).GetComponent<LevelCompleteScript>() != null)
                            {
                                obj.transform.GetChild(0).GetComponent<LevelCompleteScript>().LevelLoad = "LevelEditor";
                            }
                        }
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
                    Editor.SetObjectInLevel(Editor.EditLevel.Content[i, j, k].Object, Editor.EditLevel.Content[i, j, k].Rotation, j, k, i, Editor.EditLevel.Content[i, j, k].Channel);
                }
            }
        }
    }
}