using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmitterEventManager : MonoBehaviour
{
    public delegate void OnLevelStartEvent(int channel);

    public static OnLevelStartEvent OnLevelStart;

    public delegate void OnTransmitterActivationEvent(int channel);

    public static OnTransmitterActivationEvent OnTransmitterActivation;

    public delegate void OnTransmitterDeactivationEvent(int channel);

    public static OnTransmitterDeactivationEvent OnTransmitterDeactivation;

    public static bool[] IsChannelInMulitMode = new bool[10];
    public static int[] NumberOfTransmitterPerChannel = new int[10];
    public static int[] NumberOfActivatedTransmitterPerChannel = new int[10];
}