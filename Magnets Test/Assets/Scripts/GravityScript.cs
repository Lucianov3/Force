using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityScript : MonoBehaviour
{
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (GameManager.isGravityOn && rb.isKinematic)
        {
            rb.isKinematic = false;
        }
        else if (!rb.isKinematic)
        {
            rb.isKinematic = true;
        }
        if (rb.position.y > 0)
        {
            rb.velocity = new Vector3(0, -9.6f);
        }
        else
        {
            rb.velocity = new Vector3(0, 9.6f);
        }
    }
}