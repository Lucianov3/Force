using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScript : MonoBehaviour
{
    public GameObject Gate;

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
        if (other.CompareTag("Player") || other.CompareTag("Cube"))
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            Gate.SetActive(!Gate.activeSelf);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Cube"))
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            Gate.SetActive(!Gate.activeSelf);
        }
    }

    private void ActivateDoor()
    {
    }

    private void DeactivateDoor()
    {
    }
}