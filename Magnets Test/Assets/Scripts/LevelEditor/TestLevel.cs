using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestLevel : MonoBehaviour
{
    public static Level TestLvl;

    private void Start()
    {
        GameManager.LoadLevel(TestLvl);
        GameManager.isGravityOn = true;
    }

    private void Update()
    {
        if (Input.GetButton("Start Button 1"))
        {
            SceneManager.LoadScene("LevelEditor");
        }
    }

    private void OnDisable()
    {
        GameManager.isGravityOn = false;
    }
}