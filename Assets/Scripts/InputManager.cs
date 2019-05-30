using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Plugins.PlayerInput;
using UnityEngine.InputSystem.Plugins.UI;
using UnityEngine.SceneManagement;
using static UnityEngine.InputSystem.InputAction;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    public PlayerPhysicController Player1;
    public PlayerPhysicController Player2;

    public PlayerInput Player1Input;
    public PlayerInput Player2Input;

    public PlayerInputManager PlayerInputManager;

    public IngameMenu menu;


    private bool isMultiplayerMode = false;

    void Start ()
    {
        
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Player1Input = GetComponent<PlayerInput>();
        }
        else
        {
            Destroy(gameObject);
        }
	}

    public void OnSecondPlayerJoin(PlayerInput playerInput)
    {
        if(PlayerInputManager.playerCount == 2)
        {
            Debug.Log("Player Joined");
            isMultiplayerMode = true;
            Player1Input.SwitchCurrentActionMap("MulitPlayer");
            Player2Input = playerInput;
        }
    }
    public void OnSecondPlayerLeft()
    {
        if (PlayerInputManager.playerCount == 2)
        {
            Debug.Log("Player Left");
            isMultiplayerMode = false;
            Player1Input.SwitchCurrentActionMap("SinglePlayer");
            Player2Input = null; 
        }
    }
    
    public void TopHover(CallbackContext callbackContext)
    {
        if(Player1 != null)
        {
            Player1.Hover = callbackContext.ReadValue<float>();
        }
    }

    public void BottomHover(CallbackContext callbackContext)
    {
        if (Player2 != null)
        {
            Player2.Hover = callbackContext.ReadValue<float>();
        }
    }

    public void TopMovement(CallbackContext callbackContext)
    {
        if (Player1 != null)
        {
            Player1.Movement = callbackContext.ReadValue<float>();
        }
    }

    public void BottomMovement(CallbackContext callbackContext)
    {
        if (Player2 != null)
        {
            Player2.Movement = callbackContext.ReadValue<float>();
        }
    }

    public void TopDuck(CallbackContext callbackContext)
    {
        if (Player1 != null)
        {
            Player1.Duck = callbackContext.ReadValue<float>() == 0 ? false : true;
        }
    }

    public void BottomDuck(CallbackContext callbackContext)
    {
        if (Player2 != null)
        {
            Player2.Duck = callbackContext.ReadValue<float>() == 0 ? false : true;
        }
    }

    public void Pause(CallbackContext callbackContext)
    {
        if(menu != null)
        {
            if(callbackContext.ReadValue<float>() == 1)
            {
                menu.OpenCloseMenu();
            }
        }
    }

    public void Hover(CallbackContext callbackContext)
    {
        if (Player2 != null)
        {
            Player2.Hover = callbackContext.ReadValue<float>();
        }
    }

    public void Movement(CallbackContext callbackContext)
    {
        if (Player1 != null)
        {
            Player1.Movement = callbackContext.ReadValue<float>();
        }
    }

    public void Duck(CallbackContext callbackContext)
    {
        if (Player2 != null)
        {
            Player2.Duck = callbackContext.ReadValue<float>() == 0 ? false : true;
        }
    }
}
