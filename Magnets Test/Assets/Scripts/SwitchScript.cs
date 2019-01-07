using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScript : MonoBehaviour
{
    public GameObject Gate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !other.isTrigger || other.CompareTag("Pick Up"))
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            Gate.SetActive(!Gate.activeSelf);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !other.isTrigger || other.CompareTag("Pick Up"))
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            Gate.SetActive(!Gate.activeSelf);
        }
    }
}