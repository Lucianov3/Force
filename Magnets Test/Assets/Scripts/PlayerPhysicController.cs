using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysicController : MonoBehaviour
{
    private enum Orientation { UP, DOWN }

    private Rigidbody rb;
    [SerializeField] private Orientation orientation;
    [SerializeField] private Vector3 gravity;
    [SerializeField] private float maxHeight;
    [SerializeField] private float groundValue;
    [SerializeField] private float verticalSpeed;
    [SerializeField] private float horizontalSpeed;
    private Vector3 playerVelocity = new Vector3();

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        playerVelocity = Vector3.zero;
        if (orientation == Orientation.UP)
        {
            float floatDestination = Input.GetAxis("Right Trigger") * maxHeight;
            float currentHeight = transform.position.y  - groundValue;
            playerVelocity.y = (floatDestination - currentHeight) * verticalSpeed;

            float horizontalMovement = Input.GetAxis("Left Joystick X") * horizontalSpeed;
            playerVelocity.x = horizontalMovement;
        }
        else
        {

        }
        rb.velocity = playerVelocity;
        rb.AddForce(gravity,ForceMode.VelocityChange);
    }
}