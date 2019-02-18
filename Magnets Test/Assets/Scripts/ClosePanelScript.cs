using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosePanelScript : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    private void Update()
    {
        ClosePanel();
    }

    public void ClosePanel()
    {
        if (Input.GetButtonDown("Menu A Button"))
        {
            panel.SetActive(false);
        }
    }
}