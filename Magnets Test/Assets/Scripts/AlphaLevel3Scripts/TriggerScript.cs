using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    public GameObject GateOne;
    public GameObject GateTwo;

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
            GateOne.SetActive(false);
        }
        if (other.CompareTag("Player"))
        {
            GateTwo.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GateOne.SetActive(true);
        }
        if (other.CompareTag("Player"))
        {
            GateTwo.SetActive(false);
        }
    }
}