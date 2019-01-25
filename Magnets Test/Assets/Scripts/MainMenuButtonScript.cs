using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButtonScript : MonoBehaviour
{
    public void Scene(string buttonPress)
    {
        SceneManager.LoadScene(buttonPress);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}