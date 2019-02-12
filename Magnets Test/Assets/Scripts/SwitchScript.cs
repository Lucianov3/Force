using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScript : MonoBehaviour
{
    public int Channel = 0;

    private void Start()
    {
        TransmitterEventManager.NumberOfTransmitterPerChannel[Channel]++;
        TransmitterEventManager.IsChannelInMulitMode[Channel] = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !other.isTrigger || other.CompareTag("Pick Up"))
        {
            ActivateSwitch();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !other.isTrigger || other.CompareTag("Pick Up"))
        {
            DeactivateSwitch();
        }
    }

    private void ActivateSwitch()
    {
        TransmitterEventManager.NumberOfActivatedTransmitterPerChannel[Channel]++;
        TransmitterEventManager.OnTransmitterActivation(Channel);
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    private void DeactivateSwitch()
    {
        TransmitterEventManager.NumberOfActivatedTransmitterPerChannel[Channel]--;
        TransmitterEventManager.OnTransmitterDeactivation(Channel);
        gameObject.GetComponent<MeshRenderer>().enabled = true;
    }
}