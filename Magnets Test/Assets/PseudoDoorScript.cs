using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PseudoDoorScript : MonoBehaviour
{
    private bool activated = false;

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
        activated = true;
    }

    private void OnTriggerExit(Collider other)
    {
    }
}