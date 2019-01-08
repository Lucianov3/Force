using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteScript : MonoBehaviour
{
    static public int entered = 0;

    [SerializeField]
    private string levelLoad;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            entered++;
            if (entered >= 2)
            {
                SceneManager.LoadScene(levelLoad);
            }
            Debug.Log(entered);
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