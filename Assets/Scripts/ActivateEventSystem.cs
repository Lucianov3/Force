using UnityEngine;
using UnityEngine.EventSystems;

public class ActivateEventSystem : MonoBehaviour
{ 
    void Awake()
    {
        if(InputManager.Instance?.Player1Input != null)
        {
            GetComponent<EventSystem>().sendNavigationEvents = true;
        }
    }
}
