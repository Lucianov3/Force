using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchColorSript : MonoBehaviour
{
    [SerializeField, ColorUsage(false, true)] private Color blue;
    [SerializeField, ColorUsage(false, true)] private Color red;
    [SerializeField] private bool isTop = false;
    private Material material;

    private void Start()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        if (isTop)
        {
            if (Input.GetAxis(Controls.TopPlayerHover) > 0)
            {
                material.SetColor("_EmissionColor", red);
            }
            else
            {
                material.SetColor("_EmissionColor", blue);
            }
        }
        else
        {
            if (Input.GetAxis(Controls.BottomPlayerHover) > 0)
            {
                material.SetColor("_EmissionColor", blue);
            }
            else
            {
                material.SetColor("_EmissionColor", red);
            }
        }
    }
}