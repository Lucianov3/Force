using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchColorSript : MonoBehaviour
{
    [SerializeField] private Texture blue;
    [SerializeField] private Texture red;
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
            if (Input.GetAxis("Right Trigger 1") > 0)
            {
                material.SetTexture("_EmissionMap", red);
                material.SetTexture("_MainTex", red);
            }
            else
            {
                material.SetTexture("_EmissionMap", blue);
                material.SetTexture("_MainTex", blue);
            }
        }
        else
        {
            if (Input.GetAxis("Left Trigger 1") > 0)
            {
                material.SetTexture("_EmissionMap", blue);
                material.SetTexture("_MainTex", blue);
            }
            else
            {
                material.SetTexture("_EmissionMap", red);
                material.SetTexture("_MainTex", red);
            }
        }
    }
}