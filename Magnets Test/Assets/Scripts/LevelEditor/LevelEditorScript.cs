using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LevelEditorScript : MonoBehaviour
{
    [SerializeField] private GameObject pointer;
    [SerializeField] private int MaxXPosition = 63;
    [SerializeField] private int MaxYPosition = 18;

    private int pointerCoordinateX;

    public int PointerCoordinateX
    {
        get
        {
            return this.pointerCoordinateX;
        }
        set
        {
            this.pointerCoordinateX = value;
            this.UpdatePointer();
        }
    }

    private int pointerCoordinateY;

    public int PointerCoordinateY
    {
        get
        {
            return this.pointerCoordinateY;
        }
        set
        {
            this.pointerCoordinateY = value;
            this.UpdatePointer();
        }
    }

    private bool isPointerUp = true;

    [SerializeField] private float fastPointerMovementDelay = 1f;
    private bool allowPointerMovementX = true;
    private float fastPointerMovementTimerX = 0f;
    private bool allowPointerMovementY = true;
    private float fastPointerMovementTimerY = 0f;

    public Level EditLevel = new Level();
    private GameObject[,,] setObjects = new GameObject[2, 63, 19];

    private bool isObjectSelectionMenuOpen = false;
    private bool isMenuOpen = false;

    [SerializeField] private GameObject objectSelectionMenu;
    [SerializeField] private Dropdown dropDown;
    [SerializeField] private GameObject menu;

    private int currentHoldObjectRotation = 0;
    private int currentObjectID = 0;

    [SerializeField] private EventSystem eventSystem;

    private void Start()
    {
        SetDropDown();
        Controls.TwoPlayerMode = false;
        PointerCoordinateX = 0;
        PointerCoordinateY = 0;
        GameManager.Editor = this;
        EditLevel.LoadLevelFromJson(Application.dataPath + "/Levels/Level.txt");
        GameManager.LoadLevelIntoMapEditor();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Back Button 1"))
        {
            OpenCloseObjectSelectionMenu();
        }
        if (Input.GetButtonDown("Start Button 1"))
        {
            OpenCloseMenu();
        }
        if (!isMenuOpen && !isObjectSelectionMenuOpen)
        {
            if (Input.GetButtonDown("A Button 1"))
            {
                SetObjectInLevel(currentObjectID, currentHoldObjectRotation, PointerCoordinateX, PointerCoordinateY, isPointerUp ? 0 : 1);
            }
            if (Input.GetButtonDown("X Button 1"))
            {
                DeleteObjectInLevel();
            }
            if (Input.GetButtonDown("Y Button 1"))
            {
                isPointerUp = !isPointerUp;
                UpdatePointer();
            }
            if (Input.GetButtonDown("Right Bumper 1"))
            {
                RotateHeldObject(90);
            }
            if (Input.GetButtonDown("Left Bumper 1"))
            {
                RotateHeldObject(-90);
            }
            if (allowPointerMovementX)
            {
                if (Input.GetAxis("D Pad X 1") != 0)
                {
                    PointerCoordinateX += Mathf.CeilToInt(Input.GetAxis("D Pad X 1"));
                    allowPointerMovementX = false;
                }
            }
            else
            {
                if (Input.GetAxis("D Pad X 1") == 0)
                {
                    allowPointerMovementX = true;
                    fastPointerMovementTimerX = 0;
                }
                else if (fastPointerMovementTimerX < fastPointerMovementDelay)
                {
                    fastPointerMovementTimerX += Time.deltaTime;
                }
                else
                {
                    PointerCoordinateX += Mathf.CeilToInt(Input.GetAxis("D Pad X 1"));
                }
            }
            if (allowPointerMovementY)
            {
                if (Input.GetAxis("D Pad Y 1") != 0)
                {
                    PointerCoordinateY += Mathf.CeilToInt(Input.GetAxis("D Pad Y 1"));
                    allowPointerMovementY = false;
                }
            }
            else
            {
                if (Input.GetAxis("D Pad Y 1") == 0)
                {
                    allowPointerMovementY = true;
                    fastPointerMovementTimerY = 0;
                }
                else if (fastPointerMovementTimerY < fastPointerMovementDelay)
                {
                    fastPointerMovementTimerY += Time.deltaTime;
                }
                else
                {
                    PointerCoordinateY += Mathf.CeilToInt(Input.GetAxis("D Pad Y 1"));
                }
            }
        }
    }

    public void SetObjectInLevel(int id, int rotation, int xPosition, int yPosition, int i)
    {
        EditLevel.Content[i, xPosition, yPosition].SetObjectAndRotation(id, rotation);
        if (setObjects[i, xPosition, yPosition] != null)
        {
            Destroy(setObjects[i, xPosition, yPosition]);
            setObjects[i, xPosition, yPosition] = null;
        }
        if (id == 0)
        {
            return;
        }
        setObjects[i, xPosition, yPosition] = Instantiate(GameManager.ObjectForLoadingLevels[id - 1], i == 0 ? new Vector3(-31.5f + xPosition, 1.0f + yPosition) : new Vector3(-31.5f + xPosition, -19.0f + yPosition), Quaternion.Euler(new Vector3(0, 0, rotation)));
    }

    private void DeleteObjectInLevel()
    {
        EditLevel.Content[isPointerUp ? 0 : 1, PointerCoordinateX, PointerCoordinateY].SetObjectAndRotation(0, currentHoldObjectRotation);
        if (setObjects[isPointerUp ? 0 : 1, PointerCoordinateX, PointerCoordinateY] != null)
        {
            Destroy(setObjects[isPointerUp ? 0 : 1, PointerCoordinateX, PointerCoordinateY]);
            setObjects[isPointerUp ? 0 : 1, PointerCoordinateX, PointerCoordinateY] = null;
        }
    }

    private void RotateHeldObject(int value)
    {
        if (pointer.transform.childCount > 1)
        {
            currentHoldObjectRotation = value > 0 ? currentHoldObjectRotation + 90 : currentHoldObjectRotation - 90;
            pointer.transform.GetChild(1).transform.localRotation = Quaternion.Euler(pointer.transform.GetChild(1).transform.localRotation.x, pointer.transform.GetChild(1).transform.localRotation.y, currentHoldObjectRotation);
        }
    }

    private void SetDropDown()
    {
        List<string> objectsName = new List<string>();
        foreach (GameObject obj in GameManager.ObjectForLoadingLevels)
        {
            objectsName.Add(obj.name);
        }
        dropDown.AddOptions(objectsName);
    }

    private void OpenCloseObjectSelectionMenu()
    {
        if (isObjectSelectionMenuOpen)
        {
            dropDown.Hide();
            isObjectSelectionMenuOpen = false;
            objectSelectionMenu.SetActive(false);
        }
        else
        {
            isObjectSelectionMenuOpen = true;
            objectSelectionMenu.SetActive(true);
            eventSystem.SetSelectedGameObject(objectSelectionMenu.transform.GetChild(0).gameObject);
            dropDown.Hide();
        }
    }

    private void OpenCloseMenu()
    {
        if (isMenuOpen)
        {
            isMenuOpen = false;
            menu.SetActive(false);
        }
        else
        {
            isMenuOpen = true;
            menu.SetActive(true);
            eventSystem.SetSelectedGameObject(menu.transform.GetChild(0).gameObject);
        }
    }

    public void InstantiateChild(int i)
    {
        currentObjectID = i;
        if (pointer.transform.childCount > 1)
        {
            Destroy(pointer.transform.GetChild(1).gameObject);
        }
        if (i != 0)
        {
            Instantiate(GameManager.ObjectForLoadingLevels[i - 1], pointer.transform);
        }
    }

    private void UpdatePointer()
    {
        if (PointerCoordinateX > MaxXPosition)
        {
            PointerCoordinateX = MaxXPosition;
            return;
        }
        else if (PointerCoordinateX < 0)
        {
            PointerCoordinateX = 0;
            return;
        }
        if (PointerCoordinateY > MaxYPosition)
        {
            PointerCoordinateY = MaxYPosition;
            return;
        }
        else if (PointerCoordinateY < 0)
        {
            PointerCoordinateY = 0;
            return;
        }
        if (isPointerUp)
        {
            pointer.transform.position = new Vector3(-32.0f + PointerCoordinateX, 0.5f + PointerCoordinateY);
        }
        else
        {
            pointer.transform.position = new Vector3(-32.0f + PointerCoordinateX, -19.5f + PointerCoordinateY);
        }
    }
}