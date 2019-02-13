using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityScript : MonoBehaviour
{
    private Rigidbody rb;
    public bool IsObjectHeld = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!IsObjectHeld)
        {
            if (GameManager.isGravityOn && rb.isKinematic)
            {
                rb.isKinematic = false;
            }
            else if (!GameManager.isGravityOn && !rb.isKinematic)
            {
                rb.isKinematic = true;
            }
            if (rb.position.y > 0)
            {
                rb.velocity = new Vector3(0, -15f);
            }
            else
            {
                rb.velocity = new Vector3(0, 15f);
            }
        }
    }
}