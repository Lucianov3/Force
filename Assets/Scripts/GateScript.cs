using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateScript : MonoBehaviour
{
    public int Channel = 0;

    private MeshRenderer meshRenderer;
    private Collider collider;

    private void Start()
    {
        TransmitterEventManager.OnTransmitterActivation += ActivateGate;
        TransmitterEventManager.OnTransmitterDeactivation += DeactivateGate;
        meshRenderer = GetComponent<MeshRenderer>();
        collider = GetComponent<Collider>();
    }

    private void OnDisable()
    {
        TransmitterEventManager.OnTransmitterActivation -= ActivateGate;
        TransmitterEventManager.OnTransmitterDeactivation -= DeactivateGate;
    }

    private void ActivateGate(int TransmitterChannel)
    {
        if (TransmitterChannel == Channel)
        {
            if (TransmitterEventManager.IsChannelInMulitMode[Channel])
            {
                if (TransmitterEventManager.NumberOfActivatedTransmitterPerChannel[Channel] == TransmitterEventManager.NumberOfTransmitterPerChannel[Channel])
                {
                    collider.enabled = false;
                    meshRenderer.enabled = false;
                }
            }
            else
            {
                collider.enabled = false;
                meshRenderer.enabled = false;
            }
        }
    }

    private void DeactivateGate(int TransmitterChannel)
    {
        if (TransmitterChannel == Channel)
        {
            if (TransmitterEventManager.IsChannelInMulitMode[Channel])
            {
                if (TransmitterEventManager.NumberOfActivatedTransmitterPerChannel[Channel] < TransmitterEventManager.NumberOfTransmitterPerChannel[Channel])
                {
                    collider.enabled = true;
                    meshRenderer.enabled = true;
                }
            }
            else if (TransmitterEventManager.NumberOfActivatedTransmitterPerChannel[Channel] < 1)
            {
                collider.enabled = true;
                meshRenderer.enabled = true;
            }
        }
    }
}