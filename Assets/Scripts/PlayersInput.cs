using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem.Plugins.PlayerInput;
using static UnityEngine.InputSystem.InputAction;

public class PlayersInput : MonoBehaviour
{
    public int Player = 1;

    public string ActionMap;

    private PlayerInput playerInput;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        playerInput = GetComponent<PlayerInput>();
        if (SceneManager.GetActiveScene().name == "LevelEditor")
        {
            playerInput.SwitchCurrentActionMap("MapEditor");
        }
    }

    private void Update()
    {
        if (playerInput != null)
        {
            ActionMap = playerInput.currentActionMap.name;
        }
    }

    public void DeviceLostEvent(PlayerInput playerInput)
    {
        Destroy(playerInput.gameObject);
    }

    public void Pause(CallbackContext callbackContext)
    {
        if (InputManager.Instance.IngameMenu != null)
        {
            if (callbackContext.ReadValue<float>() == 1)
            {
                InputManager.Instance.IngameMenu.OpenCloseMenu();
            }
        }
        if(SceneManager.GetActiveScene().name == "LevelEditorTestLevel")
        {
            SceneManager.LoadScene("LevelEditor");
        }
    }

    #region SinglePlayerEvents
    public void TopHover(CallbackContext callbackContext)
    {
        if (InputManager.Instance.Player1 != null)
        {
            InputManager.Instance.Player1.Hover = callbackContext.ReadValue<float>();
        }
    }

    public void BottomHover(CallbackContext callbackContext)
    {
        if (InputManager.Instance.Player2 != null)
        {
            InputManager.Instance.Player2.Hover = callbackContext.ReadValue<float>();
        }
    }

    public void TopMovement(CallbackContext callbackContext)
    {
        if (InputManager.Instance.Player1 != null)
        {
            InputManager.Instance.Player1.Movement = callbackContext.ReadValue<float>();
        }
    }

    public void BottomMovement(CallbackContext callbackContext)
    {
        if (InputManager.Instance.Player2 != null)
        {
            InputManager.Instance.Player2.Movement = callbackContext.ReadValue<float>();
        }
    }

    public void TopDuck(CallbackContext callbackContext)
    {
        if (InputManager.Instance.Player1 != null)
        {
            InputManager.Instance.Player1.Duck = callbackContext.ReadValue<float>() == 0 ? false : true;
        }
    }

    public void BottomDuck(CallbackContext callbackContext)
    {
        if (InputManager.Instance.Player2 != null)
        {
            InputManager.Instance.Player2.Duck = callbackContext.ReadValue<float>() == 0 ? false : true;
        }
    }
    #endregion

    #region MultiPlayerEvents
    public void Hover(CallbackContext callbackContext)
    {
        if(Player == 1)
        {
            if (InputManager.Instance.Player2 != null)
            {
                InputManager.Instance.Player2.Hover = callbackContext.ReadValue<float>();
            }
        }
        else
        {
            if (InputManager.Instance.Player1 != null)
            {
                InputManager.Instance.Player1.Hover = callbackContext.ReadValue<float>();
            }
        }
    }

    public void Movement(CallbackContext callbackContext)
    {
        if (Player == 1)
        {  
            if (InputManager.Instance.Player1 != null)
            {
                InputManager.Instance.Player1.Movement = callbackContext.ReadValue<float>();
            }
        }
        else
        {
            if (InputManager.Instance.Player2 != null)
            {
                InputManager.Instance.Player2.Movement = callbackContext.ReadValue<float>();
            }
        }
    }

    public void Duck(CallbackContext callbackContext)
    {
        if (Player == 1)
        {
            if (InputManager.Instance.Player2 != null)
            {
                InputManager.Instance.Player2.Duck = callbackContext.ReadValue<float>() == 0 ? false : true;
            }
        }
        else
        {
            if (InputManager.Instance.Player1 != null)
            {
                InputManager.Instance.Player1.Duck = callbackContext.ReadValue<float>() == 0 ? false : true;
            }
        }
    }

    #endregion

    #region LevelEditorEvents

    public void Menu(CallbackContext callbackContext)
    {
        if (InputManager.Instance.LevelEditorScript != null)
        {
            InputManager.Instance.LevelEditorScript.OpenCloseMenu();
        }
    }
    public void SwitchChannel(CallbackContext callbackContext)
    {
        if (InputManager.Instance.LevelEditorScript != null && callbackContext.performed)
        {
            InputManager.Instance.LevelEditorScript.SetChannel(Mathf.RoundToInt(callbackContext.ReadValue<float>()));
        }
    }
    public void RotateRight(CallbackContext callbackContext)
    {
        if (InputManager.Instance.LevelEditorScript != null && InputManager.Instance.LevelEditorScript.AllowInputs)
        {
            InputManager.Instance.LevelEditorScript.RotateHeldObject(-90);
        }
    }
    public void RotateLeft(CallbackContext callbackContext)
    {
        if (InputManager.Instance.LevelEditorScript != null && InputManager.Instance.LevelEditorScript.AllowInputs)
        {
            InputManager.Instance.LevelEditorScript.RotateHeldObject(90);
        }

    }
    public void PlaceObject(CallbackContext callbackContext)
    {
        if (InputManager.Instance.LevelEditorScript != null && InputManager.Instance.LevelEditorScript.AllowInputs)
        {
            InputManager.Instance.LevelEditorScript.PlaceHeldObjectInLevel();
        }

    }
    public void DeleteObject(CallbackContext callbackContext)
    {
        if (InputManager.Instance.LevelEditorScript != null && InputManager.Instance.LevelEditorScript.AllowInputs)
        {
            InputManager.Instance.LevelEditorScript.DeleteObjectInLevel();
        }
    }
    public void SwitchLevel(CallbackContext callbackContext)
    {
        if (InputManager.Instance.LevelEditorScript != null && InputManager.Instance.LevelEditorScript.AllowInputs)
        {
            InputManager.Instance.LevelEditorScript.SwitchLevel();
        }
    }
    public void PointerXPress(CallbackContext callbackContext)
    {
        if (InputManager.Instance.LevelEditorScript != null && InputManager.Instance.LevelEditorScript.AllowInputs)
        {
            InputManager.Instance.LevelEditorScript.MovePointerOnXAxis(callbackContext.ReadValue<float>());
        }
    }
    public void PointerXHold(CallbackContext callbackContext)
    {
        if (InputManager.Instance.LevelEditorScript != null && callbackContext.performed && InputManager.Instance.LevelEditorScript.AllowInputs)
        {
            InputManager.Instance.LevelEditorScript.MovePointerOnXAxis(callbackContext.ReadValue<float>());
        }
    }
    public void PointerYPress(CallbackContext callbackContext)
    {
        if (InputManager.Instance.LevelEditorScript != null && InputManager.Instance.LevelEditorScript.AllowInputs)
        {
            InputManager.Instance.LevelEditorScript.MovePointerOnYAxis(callbackContext.ReadValue<float>());
        }

    }
    public void PointerYHold(CallbackContext callbackContext)
    {
        if (InputManager.Instance.LevelEditorScript != null && callbackContext.performed && InputManager.Instance.LevelEditorScript.AllowInputs)
        {
            InputManager.Instance.LevelEditorScript.MovePointerOnYAxis(callbackContext.ReadValue<float>());
        }

    }
    public void SelectObjectMenuScroll(CallbackContext callbackContext)
    {
        InputManager.Instance.LevelEditorScript.SelectObjectMenu.Scroll(Mathf.CeilToInt(callbackContext.ReadValue<float>()));
    }

    #endregion
}
