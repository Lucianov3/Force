using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestLevel : MonoBehaviour
{
    public static Level TestLvl;

    private void Start()
    {
        for (int i = 0; i < TransmitterEventManager.NumberOfTransmitterPerChannel.Length; i++)
        {
            TransmitterEventManager.NumberOfTransmitterPerChannel[i] = 0;
            TransmitterEventManager.NumberOfActivatedTransmitterPerChannel[i] = 0;
        }
        GameManager.LoadLevel(TestLvl);
        GameManager.Instance.IsGravityOn = true;
        InputManager.Instance.Player1Input.SwitchCurrentActionMap(InputManager.Instance.PlayerInputManager.playerCount == 1 ? "SinglePlayer":"MultiPlayer");
        if (InputManager.Instance.PlayerInputManager.playerCount == 2)
        {
            InputManager.Instance.Player2Input.SwitchCurrentActionMap("MultiPlayer");
        }
    }

    private void OnDisable()
    {
        GameManager.Instance.IsGravityOn = false;
    }
}