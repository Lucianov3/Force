using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class IngameMenu : MonoBehaviour
{
    [SerializeField] private EventSystem eventSystem;

    private void Start()
    {
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        InputManager.Instance.IngameMenu = this;
    }

    public void OpenCloseMenu()
    {
        if (transform.GetChild(0).gameObject.activeSelf)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(true);
            eventSystem.SetSelectedGameObject(transform.GetChild(0).GetChild(0).gameObject);
            Time.timeScale = 0;
        }
    }

    public void LoadMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}