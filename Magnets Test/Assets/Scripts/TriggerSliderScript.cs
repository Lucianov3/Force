using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSliderScript : MonoBehaviour
{
    private bool onPosition = false;
    private bool activated = false;
    private Transform childObject;
    [SerializeField] private float timeTillMaxLength = 2f;
    [SerializeField] private float count = 0;
    public int Channel = 0;

    private void Start()
    {
        childObject = transform.GetChild(0);
        TransmitterEventManager.NumberOfTransmitterPerChannel[Channel]++;
        TransmitterEventManager.IsChannelInMulitMode[Channel] = true;
    }

    private void OnDisable()
    {
        TransmitterEventManager.NumberOfTransmitterPerChannel[Channel]--;
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
            else if (!activated)
            {
                activated = true;
                TransmitterEventManager.NumberOfActivatedTransmitterPerChannel[Channel]++;
                if (TransmitterEventManager.OnTransmitterActivation != null)
                {
                    TransmitterEventManager.OnTransmitterActivation(Channel);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !other.isTrigger || other.CompareTag("Pick Up"))
        {
            onPosition = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !other.isTrigger || other.CompareTag("Pick Up"))
        {
            onPosition = false;
            count = 0;
            childObject.localScale = new Vector3(count, childObject.localScale.y, childObject.localScale.z);
            if (activated)
            {
                activated = false;
                TransmitterEventManager.NumberOfActivatedTransmitterPerChannel[Channel]--;
                if (TransmitterEventManager.OnTransmitterDeactivation != null)
                {
                    TransmitterEventManager.OnTransmitterDeactivation(Channel);
                }
            }
        }
    }
}