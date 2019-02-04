using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteScript : MonoBehaviour
{
    static public int entered = 0;

    [SerializeField]
    private string levelLoad;

    private void Start()
    {
        entered = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            entered++;
            if (entered >= 2)
            {
                TransmitterEventManager.IsChannelInMulitMode = new bool[10];
                TransmitterEventManager.NumberOfTransmitterPerChannel = new int[10];
                TransmitterEventManager.NumberOfActivatedTransmitterPerChannel = new int[10];
                SceneManager.LoadScene(levelLoad);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            entered--;
        }
    }
}