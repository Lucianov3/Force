using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PseudoDoorScript : MonoBehaviour
{
    public bool activated = false;

    [SerializeField] private float timeTillActivatedDoor = 2f;
    [SerializeField] private GameObject door;

    private float count = 0f;

    public PseudoDoorScript activator;

    // Use this for initialization
    private void Start()
    {
        if (door != null && door.GetComponent<PseudoDoorScript>() != null)
        {
            door.GetComponent<PseudoDoorScript>().activator = this;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (activated)
        {
            if (count <= timeTillActivatedDoor)
            {
                count += Time.deltaTime;
            }
            else
            {
                ActivateDoor();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            activated = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            activated = false;
            count = 0;
            DeactivateDoor();
            if (activator != null)
            {
                if (!activator.activated)
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }

    private void ActivateDoor()
    {
        door.SetActive(true);
    }

    private void DeactivateDoor()
    {
        if (door.GetComponent<PseudoDoorScript>() != null)
        {
            if (!door.GetComponent<PseudoDoorScript>().activated)
            {
                door.SetActive(false);
            }
        }
        else
        {
            door.SetActive(false);
        }
    }
}