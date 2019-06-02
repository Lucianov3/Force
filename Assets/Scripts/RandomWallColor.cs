using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWallColor : MonoBehaviour
{
    public Color[] colors;

    void Start () {
        GetComponent<MeshRenderer>().material.color = colors[Random.Range(0,colors.Length)];
	}
}
