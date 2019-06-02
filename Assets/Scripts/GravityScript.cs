using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityScript : MonoBehaviour
{
    private Rigidbody rb;
    public bool IsObjectHeld = false;
    private Vector3 startPosition;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = rb.position;
        ResetPosition.onPlayerCollision += Hit;
    }

    private void OnDisable()
    {
        ResetPosition.onPlayerCollision -= Hit;
    }

    private void Hit(string name)
    {
        if (name == transform.parent.gameObject.name)
        {
            Invoke("Respawn", 1.5f);
        }
    }

    private void Respawn()
    {
        rb.position = startPosition;
    }

    private void Update()
    {
        if (!IsObjectHeld)
        {
            if (GameManager.Instance.isGravityOn && rb.isKinematic)
            {
                rb.isKinematic = false;
            }
            else if (!GameManager.Instance.isGravityOn && !rb.isKinematic)
            {
                rb.isKinematic = true;
            }
            if (rb.position.y > 0)
            {
                rb.velocity = new Vector3(0, -3000f * Time.deltaTime);
            }
            else
            {
                rb.velocity = new Vector3(0, 3000f * Time.deltaTime);
            }
        }
    }
}