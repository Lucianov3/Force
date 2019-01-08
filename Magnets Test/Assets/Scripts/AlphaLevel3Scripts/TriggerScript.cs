using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    public GameObject GateOne;
    public GameObject GateTwo;
    public GameObject GateThree;
    public GameObject GateFour;
    public GameObject GateFive;

    public GameObject TriggerOne;
    public GameObject TriggerTwo;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && TriggerOne && !other.isTrigger)
        {
            GateOne.SetActive(false);
            GateThree.SetActive(false);
            GateTwo.SetActive(true);
            GateFour.SetActive(true);
            GateFive.SetActive(true);
        }
        if (other.CompareTag("Player") && TriggerTwo && !other.isTrigger)
        {
            GateOne.SetActive(true);
            GateThree.SetActive(true);
            GateTwo.SetActive(false);
            GateFour.SetActive(false);
            GateFive.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && TriggerOne && !other.isTrigger || other.CompareTag("Player") && TriggerTwo && !other.isTrigger)
        {
            GateOne.SetActive(true);
            GateThree.SetActive(true);
            GateTwo.SetActive(true);
            GateFour.SetActive(true);
            GateFive.SetActive(true);
        }
    }
}