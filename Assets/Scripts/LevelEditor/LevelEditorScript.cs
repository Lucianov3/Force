using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;

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

    private bool isPointerTopSide = true;

    public Level EditLevel = new Level();
    private GameObject[,,] setObjects = new GameObject[2, 64, 19];

    private bool isObjectSelectionMenuOpen = false;
    private bool isMenuOpen = false;

    public bool AllowInputs
    {
        get
        {
            if(isObjectSelectionMenuOpen || isMenuOpen)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    [SerializeField] private GameObject objectSelectionMenu;
    [SerializeField] private Dropdown dropDownObjectSelection;
    [SerializeField] private Dropdown dropDownChannelSelection;
    [SerializeField] private GameObject menu;

    private int currentHoldObjectRotation = 0;
    private int currentObjectID = 0;

    [SerializeField] private EventSystem eventSystem;

    public SelectObjectMenuScript SelectObjectMenu;

    private void Start()
    {
        InputManager.Instance.LevelEditorScript = this;
        if(InputManager.Instance.Player1Input != null)
        {
            InputManager.Instance.Player1Input.SwitchCurrentActionMap("MapEditor");
        }
        else if(InputManager.Instance.Player2Input != null)
        {
            InputManager.Instance.Player2Input.SwitchCurrentActionMap("MapEditor");
        }
        GameManager.Instance.IsGravityOn = false;
        PointerCoordinateX = 0;
        PointerCoordinateY = 0;
        GameManager.Editor = this;
        if (TestLevel.TestLvl != null)
        {
            EditLevel = TestLevel.TestLvl;
            GameManager.LoadLevelIntoMapEditor();
        }
    }

    private void OnDisable()
    {
        GameManager.Instance.IsGravityOn = true;
    }

    //private void Update()
    //{
    //    if (Input.GetButtonDown("Back Button 1"))
    //    {
    //        OpenCloseObjectSelectionMenu();
    //    }
    //    if (Input.GetButtonDown("Start Button 1"))
    //    {
    //        OpenCloseMenu();
    //    }
    //    if (Input.GetButtonDown("B Button 1"))
    //    {
    //        if (isChannelSelectionMenuOpen || isObjectSelectionMenuOpen || isMenuOpen)
    //        {
    //            CloseAllMenus();
    //        }
    //        else
    //        {
    //            OpenCloseChannelSelectionMenu();
    //        }
    //    }
    //    if (!isMenuOpen && !isObjectSelectionMenuOpen && !isChannelSelectionMenuOpen)
    //    {
    //        if (Input.GetButtonDown("A Button 1"))
    //        {
    //            PlaceObjectInLevel(currentObjectID, currentHoldObjectRotation, PointerCoordinateX, PointerCoordinateY, isPointerTopSide ? 0 : 1, 0);
    //        }
    //        if (Input.GetButtonDown("X Button 1"))
    //        {
    //            DeleteObjectInLevel();
    //        }
    //        if (Input.GetButtonDown("Y Button 1"))
    //        {
    //            SwitchLevel();
    //        }
    //        if (Input.GetButtonDown("Right Bumper 1"))
    //        {
    //            RotateHeldObject(-90);
    //        }
    //        if (Input.GetButtonDown("Left Bumper 1"))
    //        {
    //            RotateHeldObject(90);
    //        }
    //        if (allowPointerMovementX)
    //        {
    //            if (Input.GetAxis("Menu X-Axis") != 0)
    //            {
    //                PointerCoordinateX += Mathf.CeilToInt(Input.GetAxis("Menu X-Axis"));
    //                allowPointerMovementX = false;
    //            }
    //        }
    //        else
    //        {
    //            if (Input.GetAxis("Menu X-Axis") == 0)
    //            {
    //                allowPointerMovementX = true;
    //                fastPointerMovementTimerX = 0;
    //            }
    //            else if (fastPointerMovementTimerX < fastPointerMovementDelay)
    //            {
    //                fastPointerMovementTimerX += Time.deltaTime;
    //            }
    //            else
    //            {
    //                PointerCoordinateX += Mathf.CeilToInt(Input.GetAxis("Menu X-Axis"));
    //            }
    //        }
    //        if (allowPointerMovementY)
    //        {
    //            if (Input.GetAxis("Menu Y-Axis") != 0)
    //            {
    //                PointerCoordinateY += Mathf.CeilToInt(Input.GetAxis("Menu Y-Axis"));
    //                allowPointerMovementY = false;
    //            }
    //        }
    //        else
    //        {
    //            if (Input.GetAxis("Menu Y-Axis") == 0)
    //            {
    //                allowPointerMovementY = true;
    //                fastPointerMovementTimerY = 0;
    //            }
    //            else if (fastPointerMovementTimerY < fastPointerMovementDelay)
    //            {
    //                fastPointerMovementTimerY += Time.deltaTime;
    //            }
    //            else
    //            {
    //                PointerCoordinateY += Mathf.CeilToInt(Input.GetAxis("Menu Y-Axis"));
    //            }
    //        }
    //    }
    //}

    public void MovePointerOnXAxis(float value)
    {
        PointerCoordinateX += Mathf.RoundToInt(value);
    }

    public void MovePointerOnYAxis(float value)
    {
        PointerCoordinateY += Mathf.RoundToInt(value);
    }

    public void SwitchLevel()
    {
        isPointerTopSide = !isPointerTopSide;
        UpdatePointer();
    }

    public void TestEditLevel()
    {
        TestLevel.TestLvl = EditLevel;
        SceneManager.LoadScene("LevelEditorTestLevel");
    }

    public void PlaceObjectInLevel(int id, int rotation, int xPosition, int yPosition, int level, int channel)
    {
        if(id != 0)
        {
            if(!ObjectPlacementLegitimate(id,xPosition,yPosition,rotation,level))
            {
                return;
            }
            if (!GameManager.Instance.ObjectForLoadingLevels[id - 1].CanBePlacedMultipleTimes)
            {
                if(DoesObjectAlreadyExistInLevel(id,out int oldXPosition,out int oldYPosition,out int oldLevel))
                {
                    Destroy(setObjects[oldLevel, oldXPosition, oldYPosition]);
                    setObjects[oldLevel, oldXPosition, oldYPosition] = null;
                    EditLevel.Content[oldLevel, oldXPosition, oldYPosition].SetObjectAndRotation(0, 0);
                }
            }
            while(CheckIfObjectInTheWay(id, rotation, xPosition, yPosition, level, out int deleteXPosition, out int deleteYPosition))
            {
                Destroy(setObjects[level, deleteXPosition, deleteYPosition]);
                setObjects[level, deleteXPosition, deleteYPosition] = null;
                EditLevel.Content[level, deleteXPosition, deleteYPosition].SetObjectAndRotation(0, 0);
            }
        }
        EditLevel.Content[level, xPosition, yPosition].SetObjectAndRotation(id, rotation);
        if (setObjects[level, xPosition, yPosition] != null)
        {
            Destroy(setObjects[level, xPosition, yPosition]);
            setObjects[level, xPosition, yPosition] = null;
        }
        if (id == 0)
        {
            return;
        }
        setObjects[level, xPosition, yPosition] = Instantiate(GameManager.Instance.ObjectForLoadingLevels[id - 1].Object, level == 0 ? new Vector3(-31.5f + xPosition, 1.0f + yPosition) : new Vector3(-31.5f + xPosition, -19.0f + yPosition), Quaternion.Euler(new Vector3(0, 0, rotation)));
        if (GameManager.Instance.ObjectForLoadingLevels[id - 1].HasChannel)
        {
            if(setObjects[level, xPosition, yPosition].transform.childCount == 0)
            {
                setObjects[level, xPosition, yPosition].GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", GameManager.Instance.ChannelColors[channel]);
            }
            else
            {
                setObjects[level, xPosition, yPosition].transform.GetChild(0).GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", GameManager.Instance.ChannelColors[channel]);

            }
        }
    }

    public void PlaceHeldObjectInLevel()
    {
        if(currentObjectID != 0)
        {
            if (!ObjectPlacementLegitimate(currentObjectID, PointerCoordinateX, PointerCoordinateY, currentHoldObjectRotation, isPointerTopSide ? 0 : 1))
            {
                return;
            }
            if (!GameManager.Instance.ObjectForLoadingLevels[currentObjectID - 1].CanBePlacedMultipleTimes)
            {
                if (DoesObjectAlreadyExistInLevel(currentObjectID, out int oldXPosition, out int oldYPosition, out int oldLevel))
                {
                    Destroy(setObjects[oldLevel, oldXPosition, oldYPosition]);
                    setObjects[oldLevel, oldXPosition, oldYPosition] = null;
                    EditLevel.Content[oldLevel, oldXPosition, oldYPosition].SetObjectAndRotation(0, 0);
                }
            }
            while(CheckIfObjectInTheWay(currentObjectID, currentHoldObjectRotation, PointerCoordinateX, PointerCoordinateY, isPointerTopSide ? 0 : 1,out int deleteXPosition,out int deleteYPosition))
            {
                Destroy(setObjects[isPointerTopSide ? 0 : 1, deleteXPosition, deleteYPosition]);
                setObjects[isPointerTopSide ? 0 : 1, deleteXPosition, deleteYPosition] = null;
                EditLevel.Content[isPointerTopSide ? 0 : 1, deleteXPosition, deleteYPosition].SetObjectAndRotation(0, 0);
            }
        }
        EditLevel.Content[isPointerTopSide ? 0 : 1, PointerCoordinateX, PointerCoordinateY].SetObjectAndRotation(currentObjectID, currentHoldObjectRotation);
        if (setObjects[isPointerTopSide ? 0 : 1, PointerCoordinateX, PointerCoordinateY] != null)
        {
            Destroy(setObjects[isPointerTopSide ? 0 : 1, PointerCoordinateX, PointerCoordinateY]);
            setObjects[isPointerTopSide ? 0 : 1, PointerCoordinateX, PointerCoordinateY] = null;
        }
        if (currentObjectID == 0)
        {
            return;
        }
        setObjects[isPointerTopSide ? 0 : 1, PointerCoordinateX, PointerCoordinateY] = Instantiate(GameManager.Instance.ObjectForLoadingLevels[currentObjectID - 1].Object,  isPointerTopSide ? new Vector3(-31.5f + PointerCoordinateX, 1.0f + PointerCoordinateY) : new Vector3(-31.5f + PointerCoordinateX, -19.0f + PointerCoordinateY), Quaternion.Euler(new Vector3(0, 0, currentHoldObjectRotation)));
        if (GameManager.Instance.ObjectForLoadingLevels[currentObjectID - 1].HasChannel)
        {
            if (setObjects[isPointerTopSide ? 0 : 1, PointerCoordinateX, PointerCoordinateY].transform.childCount == 0)
            {
                setObjects[isPointerTopSide ? 0 : 1, PointerCoordinateX, PointerCoordinateY].GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", GameManager.Instance.ChannelColors[EditLevel.Content[isPointerTopSide ? 0 : 1, PointerCoordinateX, PointerCoordinateY].Channel]);
            }
            else
            {
                setObjects[isPointerTopSide ? 0 : 1, PointerCoordinateX, PointerCoordinateY].transform.GetChild(0).GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", GameManager.Instance.ChannelColors[EditLevel.Content[isPointerTopSide ? 0 : 1, PointerCoordinateX, PointerCoordinateY].Channel]);

            }
        }
    }

    private bool CheckIfObjectInTheWay(int id, int rotation, int xPosition, int yPosition, int level, out int oldXPosition, out int oldYPosition)
    {
        oldXPosition = 0;
        oldYPosition = 0;
        List<Vector3> heldObjectOccupancy = new List<Vector3>();
        switch (rotation)
        {
            case 0:
                for (int i = 1; i <= GameManager.Instance.ObjectForLoadingLevels[id - 1].Width; i++)
                {
                    for (int j = 1; j <= GameManager.Instance.ObjectForLoadingLevels[id - 1].Height; j++)
                    {
                        heldObjectOccupancy.Add(new Vector3(level, xPosition + i - 1, yPosition + j - 1));
                    }
                }
                break;
            case 90:
                for (int i = 1; i <= GameManager.Instance.ObjectForLoadingLevels[id - 1].Width; i++)
                {
                    for (int j = 1; j <= GameManager.Instance.ObjectForLoadingLevels[id - 1].Height; j++)
                    {
                        heldObjectOccupancy.Add(new Vector3(level, xPosition - j + 1, yPosition + i - 1));
                    }
                }
                break;
            case 180:
                for (int i = 1; i <= GameManager.Instance.ObjectForLoadingLevels[id - 1].Width; i++)
                {
                    for (int j = 1; j <= GameManager.Instance.ObjectForLoadingLevels[id - 1].Height; j++)
                    {
                        heldObjectOccupancy.Add(new Vector3(level, xPosition - i + 1, yPosition - j + 1));
                        
                    }
                }
                break;
            case 270:
                for (int i = 1; i <= GameManager.Instance.ObjectForLoadingLevels[id - 1].Width; i++)
                {
                    for (int j = 1; j <= GameManager.Instance.ObjectForLoadingLevels[id - 1].Height; j++)
                    {
                         heldObjectOccupancy.Add(new Vector3(level, xPosition + j - 1, yPosition - i + 1));
                    }
                }
                break;
        }

        List<Vector3> ObjectOccupany = new List<Vector3>();
        
        for (int j = 0; j < EditLevel.Content.GetLength(1); j++)
        {
            for (int k = 0; k < EditLevel.Content.GetLength(2); k++)
            {
                if (EditLevel.Content[level, j, k].Object != 0)
                {
                    oldXPosition = j;
                    oldYPosition = k;
                    ObjectOccupany = new List<Vector3>();
                    switch (rotation)
                    {
                        case 0:
                            for (int x = 1; x <= GameManager.Instance.ObjectForLoadingLevels[EditLevel.Content[level, j, k].Object - 1].Width; x++)
                            {
                                for (int y = 1; y <= GameManager.Instance.ObjectForLoadingLevels[EditLevel.Content[level, j, k].Object - 1].Height; y++)
                                {
                                     ObjectOccupany.Add(new Vector3(level, j + x - 1, k + y - 1));
                                }
                            }
                            break;
                        case 90:
                            for (int x = 1; x <= GameManager.Instance.ObjectForLoadingLevels[EditLevel.Content[level, j, k].Object - 1].Width; x++)
                            {
                                for (int y = 1; y <= GameManager.Instance.ObjectForLoadingLevels[EditLevel.Content[level, j, k].Object - 1].Height; y++)
                                {
                                     ObjectOccupany.Add(new Vector3(level, j - y + 1, k + x - 1));
                                }
                            }
                            break;
                        case 180:
                            for (int x = 1; x <= GameManager.Instance.ObjectForLoadingLevels[EditLevel.Content[level, j, k].Object - 1].Width; x++)
                            {
                                for (int y = 1; y <= GameManager.Instance.ObjectForLoadingLevels[EditLevel.Content[level, j, k].Object - 1].Height; y++)
                                {
                                    ObjectOccupany.Add(new Vector3(level, j - x + 1, k - y + 1));
                                }
                            }
                            break;
                        case 270:
                            for (int x = 1; x <= GameManager.Instance.ObjectForLoadingLevels[EditLevel.Content[level, j, k].Object - 1].Width; x++)
                            {
                                for (int y = 1; y <= GameManager.Instance.ObjectForLoadingLevels[EditLevel.Content[level, j, k].Object - 1].Height; y++)
                                {
                                     ObjectOccupany.Add(new Vector3(level, j + y - 1, k - x + 1));
                                }
                            }
                            break;
                    }
                    foreach (Vector3 vector3 in heldObjectOccupancy)
                    {
                        if (ObjectOccupany.Contains(vector3))
                        {
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }
    
    private bool DoesObjectAlreadyExistInLevel(int id,out int xPosition,out int yPosition,out int level)
    {
        level = 0;
        xPosition = 0;
        yPosition = 0;
        for (int i = 0; i < EditLevel.Content.GetLength(0); i++)
        {
            for (int j = 0; j < EditLevel.Content.GetLength(1); j++)
            {
                for (int k = 0; k < EditLevel.Content.GetLength(2); k++)
                {
                    if(EditLevel.Content[i,j,k].Object == id)
                    {
                        level= i;
                        xPosition = j;
                        yPosition = k;
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private bool ObjectPlacementLegitimate(int id,int xPosition,int yPosition,int rotation,int i)
    {
        if(id == 0)
        {
            return true;
        }
        if(GameManager.Instance.ObjectForLoadingLevels[id - 1].CanOnlyBePlacedOnBottom && i == 0 || GameManager.Instance.ObjectForLoadingLevels[id - 1].CanOnlyBePlacedOnTop && i == 1)
        {
            return false;
        }
        switch (i)
        {
            case 0:
                switch (rotation)
                {
                    case 0:
                        if (xPosition + GameManager.Instance.ObjectForLoadingLevels[id - 1].Width - 1 > MaxXPosition || yPosition + GameManager.Instance.ObjectForLoadingLevels[id - 1].Height - 1 > MaxYPosition)
                        {
                            return false;
                        }
                        return true;
                    case 90:
                        if (xPosition - GameManager.Instance.ObjectForLoadingLevels[id - 1].Height + 1 < 0 || yPosition + GameManager.Instance.ObjectForLoadingLevels[id - 1].Width - 1 > MaxYPosition)
                        {
                            return false;
                        }
                        return true;
                    case 180:
                        if(xPosition - GameManager.Instance.ObjectForLoadingLevels[id - 1].Width + 1 < 0 || yPosition - GameManager.Instance.ObjectForLoadingLevels[id - 1].Height + 1 < 0)
                        {
                            return false;
                        }
                        return true;

                    case 270:
                        if (xPosition + GameManager.Instance.ObjectForLoadingLevels[id - 1].Height - 1 > MaxXPosition || yPosition - GameManager.Instance.ObjectForLoadingLevels[id - 1].Width + 1 < 0)
                        {
                            return false;
                        }
                        return true;
                }
                break;

            case 1:
                switch (rotation)
                {
                    case 0:
                        if (xPosition + GameManager.Instance.ObjectForLoadingLevels[id - 1].Width - 1 > MaxXPosition || yPosition + GameManager.Instance.ObjectForLoadingLevels[id - 1].Height - 1 > MaxYPosition)
                        {
                            return false;
                        }
                        return true;
                    case 90:
                        if (xPosition - GameManager.Instance.ObjectForLoadingLevels[id - 1].Height + 1 < 0 || yPosition + GameManager.Instance.ObjectForLoadingLevels[id - 1].Width - 1 > MaxYPosition)
                        {
                            return false;
                        }
                        return true;
                    case 180:
                        if (xPosition - GameManager.Instance.ObjectForLoadingLevels[id - 1].Width + 1 < 0 || yPosition - GameManager.Instance.ObjectForLoadingLevels[id - 1].Height + 1 < 0)
                        {
                            return false;
                        }
                        return true;

                    case 270:
                        if (xPosition + GameManager.Instance.ObjectForLoadingLevels[id - 1].Height - 1 > MaxXPosition || yPosition - GameManager.Instance.ObjectForLoadingLevels[id - 1].Width + 1 < 0)
                        {
                            return false;
                        }
                        return true;
                }
                break;
        }

        Debug.Log("ObjectPlacementLegitimate function failed");
        return false;
    }
    
    public void DeleteObjectInLevel()
    {
        EditLevel.Content[isPointerTopSide ? 0 : 1, PointerCoordinateX, PointerCoordinateY].SetObjectAndRotation(0, currentHoldObjectRotation);
        if (setObjects[isPointerTopSide ? 0 : 1, PointerCoordinateX, PointerCoordinateY] != null)
        {
            Destroy(setObjects[isPointerTopSide ? 0 : 1, PointerCoordinateX, PointerCoordinateY]);
            setObjects[isPointerTopSide ? 0 : 1, PointerCoordinateX, PointerCoordinateY] = null;
        }
    }

    public void RotateHeldObject(int value)
    {
        if (pointer.transform.childCount > 1)
        {
            if (!GameManager.Instance.ObjectForLoadingLevels[currentObjectID - 1].CanBeRotated)
            {
                return;
            }
            currentHoldObjectRotation = value > 0 ? currentHoldObjectRotation + 90 : currentHoldObjectRotation - 90;
            if(currentHoldObjectRotation == -90)
            {
                currentHoldObjectRotation = 270;
            }
            else if(currentHoldObjectRotation == 360)
            {
                currentHoldObjectRotation = 0;
            }
            pointer.transform.GetChild(1).transform.localRotation = Quaternion.Euler(pointer.transform.GetChild(1).transform.localRotation.x, pointer.transform.GetChild(1).transform.localRotation.y, currentHoldObjectRotation);
        }
    }

    public void OpenCloseMenu()
    {
        if (isMenuOpen)
        {
            isMenuOpen = false;
            menu.SetActive(false);
        }
        else
        {
            CloseAllMenus();
            isMenuOpen = true;
            menu.SetActive(true);
            eventSystem.SetSelectedGameObject(menu.transform.GetChild(0).GetChild(0).gameObject);
        }
    }

    private void CloseAllMenus()
    {
        if (isMenuOpen)
        {
            OpenCloseMenu();
        }
    }

    public void InstantiateChild(int i)
    {
        currentObjectID = i;
        if (!GameManager.Instance.ObjectForLoadingLevels[i - 1].CanBeRotated)
        {
            currentHoldObjectRotation = 0;
        }
        if(GameManager.Instance.ObjectForLoadingLevels[i - 1].IsUpsideDown)
        {
            currentHoldObjectRotation = 180;
        }
        if (pointer.transform.childCount > 1)
        {
            Destroy(pointer.transform.GetChild(1).gameObject);
        }
        if (i != 0)
        {
            Instantiate(GameManager.Instance.ObjectForLoadingLevels[i - 1].Object, pointer.transform).transform.rotation = Quaternion.Euler(new Vector3(0, 0, currentHoldObjectRotation));
        }
    }

    public void SetChannel(int value)
    {
        if(EditLevel.Content[isPointerTopSide ? 0 : 1, PointerCoordinateX, PointerCoordinateY].Object == 0)
        {
            return;
        }
        if(GameManager.Instance.ObjectForLoadingLevels[EditLevel.Content[isPointerTopSide ? 0 : 1, PointerCoordinateX, PointerCoordinateY].Object-1].HasChannel)
        {
            EditLevel.Content[isPointerTopSide ? 0 : 1, PointerCoordinateX, PointerCoordinateY].Channel += value;
            if(EditLevel.Content[isPointerTopSide ? 0 : 1, PointerCoordinateX, PointerCoordinateY].Channel == 10)
            {
                EditLevel.Content[isPointerTopSide ? 0 : 1, PointerCoordinateX, PointerCoordinateY].Channel = 0;
            }
            else if(EditLevel.Content[isPointerTopSide ? 0 : 1, PointerCoordinateX, PointerCoordinateY].Channel == -1)
            {
                EditLevel.Content[isPointerTopSide ? 0 : 1, PointerCoordinateX, PointerCoordinateY].Channel = 9;
            }
            if (setObjects[isPointerTopSide ? 0 : 1, PointerCoordinateX, PointerCoordinateY].transform.childCount == 0)
            {
                setObjects[isPointerTopSide ? 0 : 1, PointerCoordinateX, PointerCoordinateY].GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", GameManager.Instance.ChannelColors[EditLevel.Content[isPointerTopSide ? 0 : 1, PointerCoordinateX, PointerCoordinateY].Channel]);
            }
            else
            {
                setObjects[isPointerTopSide ? 0 : 1, PointerCoordinateX, PointerCoordinateY].transform.GetChild(0).GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", GameManager.Instance.ChannelColors[EditLevel.Content[isPointerTopSide ? 0 : 1, PointerCoordinateX, PointerCoordinateY].Channel]);

            }
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
        if (isPointerTopSide)
        {
            pointer.transform.position = new Vector3(-32.0f + PointerCoordinateX, 0.5f + PointerCoordinateY);
        }
        else
        {
            pointer.transform.position = new Vector3(-32.0f + PointerCoordinateX, -19.5f + PointerCoordinateY);
        }
    }
}