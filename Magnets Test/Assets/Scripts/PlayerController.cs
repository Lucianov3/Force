using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static PlayerController player1;
    private static PlayerController player2;

    private enum Player { PLAYER1, PLAYER2 };

    [SerializeField] private Player player;
    [SerializeField] private float horizontalSpeed = 1;
    [SerializeField] private float verticalSpeed = 1;
    [SerializeField] private float groundValue;
    [SerializeField] private float ceilingValue;
    protected Transform transform;
    private float ogGroundValue;

    private void Start()
    {
        if (player == Player.PLAYER1)
        {
            player1 = this;
        }
        else
        {
            player2 = this;
        }
        transform = GetComponent<Transform>();
        groundValue = transform.position.y;
        ogGroundValue = transform.position.y;
    }

    private void Update()
    {
        RaycastHit hitInfo;
        string joystick;
        Vector3 movementX = new Vector3();
        if (player == Player.PLAYER1)
        {
            joystick = "Left Joystick X";
            movementX.x += Input.GetAxis("D Pad X");
            if (Physics.Raycast(new Vector3(transform.position.x + 0.5f, transform.position.y), Vector3.down, out hitInfo) || Physics.Raycast(new Vector3(transform.position.x - 0.5f, transform.position.y), Vector3.down, out hitInfo))
            {
                groundValue = hitInfo.transform.position.y + 1.0f;
            }
            if (player2 != null)
            {
                float destination = Mathf.Min((Input.GetAxis("Left Trigger") * -player2.ceilingValue), player2.groundValue);
                player2.transform.position = new Vector3(player2.transform.position.x, Mathf.MoveTowards(player2.transform.position.y, destination, player2.verticalSpeed));
            }
        }
        else
        {
            joystick = "Right Joystick X";
            if (Input.GetButton("X Button"))
            {
                movementX.x += -1;
            }
            if (Input.GetButton("B Button"))
            {
                movementX.x += 1;
            }
            if (Physics.Raycast(new Vector3(transform.position.x + 0.5f, transform.position.y), Vector3.up, out hitInfo) || Physics.Raycast(new Vector3(transform.position.x - 0.5f, transform.position.y), Vector3.up, out hitInfo))
            {
                groundValue = hitInfo.transform.position.y - 1.0f;
            }
            if (player1 != null)
            {
                float destination = Mathf.Max((Input.GetAxis("Right Trigger") * player1.ceilingValue), player1.groundValue);
                player1.transform.position = new Vector3(player1.transform.position.x, Mathf.MoveTowards(player1.transform.position.y, destination, player1.verticalSpeed));
            }
        }
        movementX.x += Input.GetAxis(joystick);
        movementX.Normalize();
        if (Physics.Raycast(transform.position, Vector3.right, out hitInfo, 0.6f) && movementX.x > 0)
        {
            movementX = Vector3.zero;
            transform.position = new Vector3(hitInfo.point.x - 0.55f, transform.position.y);
        }
        if (Physics.Raycast(transform.position, Vector3.left, out hitInfo, 1.0f) && movementX.x < 0)
        {
            movementX = Vector3.zero;
            transform.position = new Vector3(hitInfo.point.x + 0.55f, transform.position.y);
        }
        transform.position += (movementX * horizontalSpeed);
    }
}