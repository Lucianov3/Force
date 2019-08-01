using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConnectPlayerTextScript : MonoBehaviour
{
    [SerializeField]
    private Animation animation;
    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private bool player1;
    [SerializeField]
    private string disconnectedText;
    [SerializeField]
    private string connectedText;


    private void Awake()
    {
        if (player1)
        {
            if (InputManager.Instance != null)
            {
                if(InputManager.Instance.Player1Input != null)
                {
                    InputManager.Instance.connectPlayer1Text = this;
                    animation.Stop();
                    text.text = connectedText;
                }
                else
                {
                    animation.Play();
                    text.text = disconnectedText;
                }
            }
        }
        else
        {
            if (InputManager.Instance != null)
            {
                if (InputManager.Instance.Player2Input != null)
                {
                    InputManager.Instance.connectPlayer2Text = this;
                    animation.Stop();
                    text.text = connectedText;
                }
                else
                {
                    animation.Play();
                    text.text = disconnectedText;
                }
            }
        }
    }

    private void OnDisable()
    {
        if(player1)
        {
            InputManager.Instance.connectPlayer1Text = null;
        }
        else
        {
            InputManager.Instance.connectPlayer1Text = null;
        }
    }

    public void PlayerConnected()
    {
        text.text = connectedText;
        animation.Stop();
        text.color = new Color32(255, 255, 255, 255);
    }

    public void PlayerDisconnected()
    {
        text.text = disconnectedText; ;
        animation.Play();
    }
}
