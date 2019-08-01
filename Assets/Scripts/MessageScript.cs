using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageScript : MonoBehaviour
{
    private void Start()
    {
        Invoke("DeleteThisObject", GameManager.Instance.TimeMessageIsShown);
    }

    private void DeleteThisObject()
    {
        GameManager.Instance.Messages.Remove(gameObject);
        Destroy(gameObject);
    }
}
