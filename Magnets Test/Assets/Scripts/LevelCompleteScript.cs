using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteScript : MonoBehaviour
{
    static public int entered = 0;

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            entered++;
            Debug.Log(entered);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            entered--;
        }
    }
}