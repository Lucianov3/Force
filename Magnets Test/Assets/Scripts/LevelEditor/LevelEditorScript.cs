using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorScript : MonoBehaviour
{
    [SerializeField] private GameObject pointer;
    public int PointerCoordinateX
    {
        set
        {
            PointerCoordinateX = value;
            UpdatePointer();
        }

    }
    private int pointerCoordinateY = 0;

    private void Start()
    {
        Controls.TwoPlayerMode = false;
    }

    private void Update()
    {
    }

    private void UpdatePointer()
    {

    }
}