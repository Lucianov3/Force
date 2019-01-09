using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSliderScript : MonoBehaviour
{
    public bool onPosition = false;

    private Transform childObject;

    [SerializeField] private float timeTillMaxLength = 2f;
    [SerializeField] private float count = 0;

    private void Start()
    {
        childObject = transform.GetChild(0);
    }

    private void Update()
    {
        if (onPosition)
        {
            if (count <= timeTillMaxLength)
            {
                count += Time.deltaTime;
                childObject.localScale = new Vector3(count / timeTillMaxLength, childObject.localScale.y, childObject.localScale.z);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            onPosition = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            onPosition = false;
            count = 0;
            childObject.localScale = new Vector3(count, childObject.localScale.y, childObject.localScale.z);
        }
    }
}