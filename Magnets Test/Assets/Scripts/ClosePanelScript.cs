using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosePanelScript : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    private void Start()
    {
        Time.timeScale = 0;
    }

    private void Update()
    {
        ClosePanel();
    }

    public void ClosePanel()
    {
        if (Input.GetButtonDown("Menu A Button"))
        {
            Time.timeScale = 1;

            panel.SetActive(false);
        }
    }
}