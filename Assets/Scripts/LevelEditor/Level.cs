using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Level
{
    public enum LevelOrientation { UP, DOWN }

    public string Name = "";

    public LevelContent[,,] Content = new LevelContent[2, 64, 19];

    public LevelOrientation TopOrientation = LevelOrientation.DOWN;
    public LevelOrientation BotOrientation = LevelOrientation.UP;

    public Level()
    {
        FillLevelWithContent();
    }

    public void FillLevelWithContent()
    {
        for (int i = 0; i < Content.GetLength(0); i++)
        {
            for (int j = 0; j < Content.GetLength(1); j++)
            {
                for (int k = 0; k < Content.GetLength(2); k++)
                {
                    Content[i, j, k] = new LevelContent();
                }
            }
        }
    }

    public void LoadLevelFromJson(string path)
    {
        if (File.Exists(path))
        {
            using (StreamReader reader = File.OpenText(path))
            {
                JObject o = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
                Level level = JsonConvert.DeserializeObject<Level>(o.ToString());
                this.Name = level.Name;
                this.Content = level.Content;
                this.TopOrientation = level.TopOrientation;
                this.BotOrientation = level.BotOrientation;
            }
            Debug.Log("Level Succesfully loaded");
            GameManager.Instance.PrintMessageInCanvas("Level Loaded");
        }
        else
        {
            GameManager.Instance.PrintMessageInCanvas("Level doesn't Exist");
            Debug.Log("Level doesn't Exist");
        }
    }

    public void DeleteLevel(string path)
    {
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("Level Succesfully Deleted");
            GameManager.Instance.PrintMessageInCanvas("Level Deleted");
        }
        else
        {
            GameManager.Instance.PrintMessageInCanvas("Level doesn't Exist");
            Debug.Log("Level doesn't Exist");
        }
    }

    public void SaveLevelToJson(string path)
    {
        if (!Directory.Exists(Application.dataPath + "/Levels/"))
        {
            Directory.CreateDirectory(Application.dataPath + "/Levels/");
        }
        File.WriteAllText(path, JsonConvert.SerializeObject(this));

        // write JSON directly to a file
        using (StreamWriter file = File.CreateText(path))
        using (JsonTextWriter writer = new JsonTextWriter(file))
        {
            JObject.FromObject(this).WriteTo(writer);
        }
        GameManager.Instance.PrintMessageInCanvas("Level Saved");
        Debug.Log("Level Succesfully saved");
    }
}