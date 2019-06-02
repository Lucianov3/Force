using UnityEngine;
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


    #region SinglePlayerEvents
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
    #endregion

    public void Pause(CallbackContext callbackContext)
    {
        if(IngameMenu != null)
        {
            if(callbackContext.ReadValue<float>() == 1)
            {
                IngameMenu.OpenCloseMenu();
            }
        }
    }

    #region MultiPlayerEvents
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

    #endregion

    #region LevelEditorEvents

    public void Menu(CallbackContext callbackContext)
    {
        if(LevelEditorScript != null)
        {
            LevelEditorScript.OpenCloseMenu();
        }
    }
    public void SelectObjectMenu(CallbackContext callbackContext)
    {
        if (LevelEditorScript != null)
        {
            LevelEditorScript.OpenCloseObjectSelectionMenu();
        }
    }
    public void SetChannelMenu(CallbackContext callbackContext)
    {
        if (LevelEditorScript != null)
        {
            LevelEditorScript.OpenCloseChannelSelectionMenu();
        }
    }
    public void RotateRight(CallbackContext callbackContext)
    {
        if (LevelEditorScript != null && LevelEditorScript.AllowInputs)
        {
            LevelEditorScript.RotateHeldObject(-90);
        }
    }
    public void RotateLeft(CallbackContext callbackContext)
    {
        if (LevelEditorScript != null && LevelEditorScript.AllowInputs)
        {
            LevelEditorScript.RotateHeldObject(90);
        }

    }
    public void PlaceObject(CallbackContext callbackContext)
    {
        if (LevelEditorScript != null && LevelEditorScript.AllowInputs)
        {
            LevelEditorScript.PlaceHeldObjectInLevel();
        }

    }
    public void DeleteObject(CallbackContext callbackContext)
    {
        if (LevelEditorScript != null && LevelEditorScript.AllowInputs)
        {
            LevelEditorScript.DeleteObjectInLevel();
        }
    }
    public void SwitchLevel(CallbackContext callbackContext)
    {
        if (LevelEditorScript != null && LevelEditorScript.AllowInputs)
        {
            LevelEditorScript.SwitchLevel();
        }
    }
    public void PointerXPress(CallbackContext callbackContext)
    {
        if (LevelEditorScript != null && LevelEditorScript.AllowInputs)
        {
            LevelEditorScript.MovePointerOnXAxis(callbackContext.ReadValue<float>());
        }
    }
    public void PointerXHold(CallbackContext callbackContext)
    {
        if (LevelEditorScript != null && callbackContext.performed && LevelEditorScript.AllowInputs)
        {
            LevelEditorScript.MovePointerOnXAxis(callbackContext.ReadValue<float>());
        }
    }
    public void PointerYPress(CallbackContext callbackContext)
    {
        if (LevelEditorScript != null && LevelEditorScript.AllowInputs)
        {
            LevelEditorScript.MovePointerOnYAxis(callbackContext.ReadValue<float>());
        }

    }
    public void PointerYHold(CallbackContext callbackContext)
    {
        if (LevelEditorScript != null && callbackContext.performed && LevelEditorScript.AllowInputs)
        {
            LevelEditorScript.MovePointerOnYAxis(callbackContext.ReadValue<float>());
        }

    }

    #endregion

}
