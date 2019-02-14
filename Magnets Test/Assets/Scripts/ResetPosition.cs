using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosition : MonoBehaviour
{
    public delegate void OnPlayerCollision(string name);

    public static event OnPlayerCollision onPlayerCollision;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.isTrigger && collision.collider.CompareTag("Player"))
        {
            onPlayerCollision(collision.collider.transform.parent.gameObject.name);
        }
    }
}