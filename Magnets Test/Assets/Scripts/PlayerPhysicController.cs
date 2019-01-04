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
    private Animator animator;

    private float animationWalkingSpeed
    {
        get
        {
            if (animator == null)
            {
                animator = GetComponent<Animator>(); 
            }
            return animator.GetFloat("WalkingSpeed");
        }
        set
        {
            if (animator == null)
            {
                animator = GetComponent<Animator>();
            }
            animator.SetFloat("WalkingSpeed",value);
        }
    }
    private bool animationCrouched
    {
        get
        {
            if (animator == null)
            {
                animator = GetComponent<Animator>();
            }
            return animator.GetBool("Crouched");
        }
        set
        {
            if (animator == null)
            {
                animator = GetComponent<Animator>();
            }
            animator.SetBool("Crouched", value);
        }
    }
    private bool animationFlying
    {
        get
        {
            if (animator == null)
            {
                animator = GetComponent<Animator>();
            }
            return animator.GetBool("Flying");
        }
        set
        {
            if (animator == null)
            {
                animator = GetComponent<Animator>();
            }
            animator.SetBool("Flying", value);
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        playerVelocity = Vector3.zero;
        animationFlying = Input.GetAxis("Right Trigger") > 0 ? true : false;
        animationWalkingSpeed = Mathf.Abs(Input.GetAxis("Left Joystick X"));
        if (orientation == Orientation.UP)
        {
            float floatDestination = Input.GetAxis("Right Trigger") * maxHeight;
            float currentHeight = transform.position.y  - groundValue;
            playerVelocity.y = (floatDestination - currentHeight) * verticalSpeed;
            if(Input.GetAxis("Left Joystick X") > 0)
            {
                rb.rotation = Quaternion.Euler(new Vector3(0,-90,0));
            }
            else if(Input.GetAxis("Left Joystick X") < 0)
            {
                rb.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
            }
            Debug.Log(Input.GetAxis("Right Trigger"));
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