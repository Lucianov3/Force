using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysicController : MonoBehaviour
{
    private enum Orientation { UP, DOWN }

    [SerializeField] private Orientation orientation;
    [SerializeField] private Vector3 gravity;
    [SerializeField] private float maxHeight;
    [SerializeField] private float groundValue;
    [SerializeField] private float verticalSpeed;
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private string trigger;
    [SerializeField] private string joystickX;
    [SerializeField] private string joystickY;
    [SerializeField] private string interactButton;

    private bool grounded = false;
    private Collider currentGround;
    private bool holdingObject = false;
    private Rigidbody rb;
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
        printDebugLogs();
        playerVelocity = Vector3.zero;
        animationFlying = Input.GetAxis(trigger) > 0 ? true : false;
        animationWalkingSpeed = Mathf.Abs(Input.GetAxis(joystickX));
        if (orientation == Orientation.UP)
        {
            float floatDestination = Input.GetAxis(trigger) * maxHeight;
            float currentHeight = transform.position.y  - groundValue;
            playerVelocity.y = (floatDestination - currentHeight) * verticalSpeed;
            if(Input.GetAxis(joystickX) > 0)
            {
                rb.rotation = Quaternion.Euler(new Vector3(0,-90,0));
            }
            else if(Input.GetAxis(joystickX) < 0)
            {
                rb.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
            }
            float horizontalMovement = Input.GetAxis(joystickX) * horizontalSpeed;
            playerVelocity.x = horizontalMovement;
        }
        else if(orientation == Orientation.DOWN)
        {
            float floatDestination = -Input.GetAxis(trigger) * maxHeight;
            float currentHeight = transform.position.y + groundValue;
            playerVelocity.y = (floatDestination - currentHeight) * verticalSpeed;
            if (Input.GetAxis(joystickX) > 0)
            {
                rb.rotation = Quaternion.Euler(new Vector3(180, 90, 0));
            }
            else if (Input.GetAxis(joystickX) < 0)
            {
                rb.rotation = Quaternion.Euler(new Vector3(180, -90, 0));
            }
            float horizontalMovement = Input.GetAxis(joystickX) * horizontalSpeed;
            playerVelocity.x = horizontalMovement;
        }
        rb.velocity = playerVelocity;
        rb.AddForce(gravity,ForceMode.VelocityChange);

    }

    private void printDebugLogs()
    {
        Debug.Log(grounded);
    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            
            switch (orientation)
            {
                case Orientation.UP:
                    if (contact.point.y <= transform.position.y - 0.10f)
                    {
                        grounded = true;
                        currentGround = collision.collider;
                    }
                    break;
                case Orientation.DOWN:
                    if (contact.point.y <= transform.position.y + 0.10f)
                    {
                        grounded = true;
                    }
                    break;
            }
        }
        if (!holdingObject && collision.collider.CompareTag("Pick Up") && Input.GetButton(interactButton))
        {

        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.collider == currentGround)
        {
            grounded = false;
        }
    }
}