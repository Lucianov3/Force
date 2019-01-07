using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosition : MonoBehaviour
{
    public GameObject Player;
    private Vector3 playerPostion;

    private void Start()
    {
        playerPostion = Player.transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Player.transform.position = playerPostion;
        }
    }
}