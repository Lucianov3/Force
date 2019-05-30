using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartLevel : MonoBehaviour
{
    [SerializeField] private GameObject[] objects;

    private bool restartAllowed = true;

    public void ResetLevel()
    {
        Time.timeScale = 1;
        if (restartAllowed)
        {
            foreach (GameObject obj in objects)
            {
                ResetPosition.onPlayerCollision(obj.transform.parent.gameObject.name);
            }
            restartAllowed = false;
            Invoke("AllowAnotherRestart", 3.0f);
        }
    }

    private void AllowAnotherRestart()
    {
        restartAllowed = true;
    }
}