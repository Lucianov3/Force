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

    public void ChangePlayerMode(bool active)
    {
        Controls.TwoPlayerMode = active;
    }

    public void ResetTransmitter()
    {
        for (int i = 0; i < TransmitterEventManager.NumberOfTransmitterPerChannel.Length; i++)
        {
            TransmitterEventManager.NumberOfTransmitterPerChannel[i] = 0;
            TransmitterEventManager.NumberOfActivatedTransmitterPerChannel[i] = 0;
        }
    }
}