using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysicController : MonoBehaviour
{
    private enum playerEnum { PLAYER1, PLAYER2 }

    private Rigidbody rb;
    [SerializeField] private playerEnum player;
    [SerializeField] private float gravityStrength = 1;
    [SerializeField] private float velocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        velocity = rb.velocity.y;
        if (player == playerEnum.PLAYER1)
        {
            //rb.AddRelativeForce
        }
    }
}