using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosition : MonoBehaviour
{
    [SerializeField] private GameObject playerRed;
    private Vector3 playerRedPosition;

    [SerializeField] private GameObject playerBlue;
    private Vector3 playerBluePosition;

    private void Start()
    {
        playerRedPosition = playerRed.transform.position;
        playerBluePosition = playerBlue.transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (playerRed == collision.gameObject && !collision.collider.isTrigger)
        {
            playerRed.transform.position = playerRedPosition;
        }
        if (playerBlue == collision.gameObject && !collision.collider.isTrigger)
        {
            playerBlue.transform.position = playerBluePosition;
        }
    }
}