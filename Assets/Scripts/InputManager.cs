using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Plugins.PlayerInput;
using static UnityEngine.InputSystem.InputAction;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    public PlayerPhysicController Player1;
    public PlayerPhysicController Player2;

    public PlayerInput Player1Input;
    public PlayerInput Player2Input;

    public PlayerInputManager PlayerInputManager;

    public IngameMenu IngameMenu;

    public LevelEditorScript LevelEditorScript;

    public ConnectPlayerTextScript connectPlayer1Text;
    public ConnectPlayerTextScript connectPlayer2Text;

    void Start ()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
	}

    public void DisconnectSecondPlayer()
    {
        Destroy(Player2Input.gameObject);
        Player2Input = null;
    }

    public void ChangeActionMap()
    {
        Player1Input.SwitchCurrentActionMap(PlayerInputManager.playerCount == 1 ? "SinglePlayer" : "MultiPlayer");
        if (PlayerInputManager.playerCount == 2)
        {
            Player2Input.SwitchCurrentActionMap("MultiPlayer");
        }
    }

    public void OnPlayerJoin(PlayerInput playerInput)
    {
        GameManager.Instance.PrintMessageInCanvas("Player Joined");
        if (PlayerInputManager.playerCount == 1)
        {
            GameObject.Find("EventSystem").GetComponent<EventSystem>().sendNavigationEvents = true;
            Player1Input = playerInput;
            Player1Input.SwitchCurrentActionMap("SinglePlayer");
            playerInput.GetComponent<PlayersInput>().Player = 1;
            connectPlayer1Text?.PlayerConnected();
        }
        if (PlayerInputManager.playerCount == 2)
        {
            Player1Input.SwitchCurrentActionMap("MultiPlayer");
            Player2Input = playerInput;
            playerInput.GetComponent<PlayersInput>().Player = 2;
            connectPlayer2Text?.PlayerConnected();
        }
    }
    public void OnPlayerLeft(PlayerInput playerInput)
    {
        GameManager.Instance.PrintMessageInCanvas("Player Left");
        if (PlayerInputManager.playerCount == 0)
        {
            Player1Input = null;
            GameObject.Find("EventSystem").GetComponent<EventSystem>().sendNavigationEvents = false;
            connectPlayer1Text?.PlayerDisconnected();

        }
        if (PlayerInputManager.playerCount == 1)
        {
            if (Player1Input == playerInput)
            {
                Player1Input = Player2Input;
                Player2Input = null;
            }
            else
            {
                Player2Input = null;
            }
            Player1Input.SwitchCurrentActionMap("SinglePlayer");
            Player1Input.GetComponent<PlayersInput>().Player = 1;
            connectPlayer2Text?.PlayerDisconnected();
        }
    }
}
