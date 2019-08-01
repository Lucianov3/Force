using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SelectObjectMenuScript : MonoBehaviour
{
    private int currentIndex = 0;
    private int currentPage = 0;
    [SerializeField] private int objectsPerPage = 7;
    [SerializeField] private RectTransform menuPointer;
    [SerializeField] private Sprite defaultImage;
    [SerializeField] private List<Image> Images;



    private void Start()
    {
        InputManager.Instance.LevelEditorScript.SelectObjectMenu = this;
        ChangeImagesInMenu();
        InputManager.Instance.LevelEditorScript.InstantiateChild(currentIndex+1);
    }

    private void ChangeImagesInMenu()
    {
        for (int i = 0; i < Images.Count; i++)
        {
            if (GameManager.Instance.ObjectForLoadingLevels.Count <= i + (currentPage * objectsPerPage))
            {
                Images[i].enabled = false;
                Images[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().enabled = false;
                continue;
            }
            Images[i].enabled = true;
            Images[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().enabled = true;
            Images[i].gameObject.name = GameManager.Instance.ObjectForLoadingLevels[i + (currentPage * objectsPerPage)].Name;
            if (GameManager.Instance.ObjectForLoadingLevels[i + (currentPage * objectsPerPage)].Image != null)
            {
                Images[i].sprite = GameManager.Instance.ObjectForLoadingLevels[i + (currentPage * objectsPerPage)].Image;
                Images[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = GameManager.Instance.ObjectForLoadingLevels[i + (currentPage * objectsPerPage)].Name;
            }
            else
            {
                Images[i].sprite = defaultImage;
            }
        }
    }

    public void Scroll(int value)
    {
        currentIndex += value;
        if(currentIndex < 0)
        {
            currentIndex = GameManager.Instance.ObjectForLoadingLevels.Count - 1;
        }
        else if (currentIndex > GameManager.Instance.ObjectForLoadingLevels.Count - 1)
        {
            currentIndex = 0;
        }
        int page = currentPage;
        currentPage = Mathf.FloorToInt(currentIndex / objectsPerPage);
        if(currentPage != page)
        {
            ChangeImagesInMenu();
        }
        UpdatePointer();
        InputManager.Instance.LevelEditorScript.InstantiateChild(currentIndex+1);
    }

    private void UpdatePointer()
    {
        menuPointer.anchoredPosition = new Vector2(0, 450 - (150 * (currentIndex - (objectsPerPage * currentPage))));
    }

}
