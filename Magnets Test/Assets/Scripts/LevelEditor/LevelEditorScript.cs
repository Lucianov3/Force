using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Start()
    {
        Controls.TwoPlayerMode = false;
        PointerCoordinateX = 0;
        PointerCoordinateY = 0;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Y Button 1"))
        {
            isPointerUp = !isPointerUp;
            UpdatePointer();
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