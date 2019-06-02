using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class Player2Input : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
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
    }

    public void Hover(CallbackContext callbackContext)
    {
        if (InputManager.Instance.Player1 != null)
        {
            InputManager.Instance.Player1.Hover = callbackContext.ReadValue<float>();
        }
    }

    public void Movement(CallbackContext callbackContext)
    {
        if (InputManager.Instance.Player2 != null)
        {
            InputManager.Instance.Player2.Movement = callbackContext.ReadValue<float>();
        }
    }

    public void Duck(CallbackContext callbackContext)
    {
        if (InputManager.Instance.Player1 != null)
        {
            InputManager.Instance.Player1.Duck = callbackContext.ReadValue<float>() == 0 ? false : true;
        }
    }
}
