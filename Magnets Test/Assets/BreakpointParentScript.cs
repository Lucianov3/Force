using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakpointParentScript : MonoBehaviour
{
    private int children;

    public int Counter;

    [SerializeField] private GameObject prefabObject;

    private void Start()
    {
        children = transform.childCount;
    }

    private void Update()
    {
        if (Counter == children)
        {
            //mach was
            prefabObject.SetActive(false);
        }
    }
}