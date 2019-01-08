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

    private Collider currentGround;
    private bool holdingObject = false;
    private GameObject objectHold;
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
            animator.SetFloat("WalkingSpeed", value);
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
        animationFlying = Input.GetAxis(trigger) > 0 ? true : false;
        animationWalkingSpeed = Mathf.Abs(Input.GetAxis(joystickX));
        if (orientation == Orientation.UP)
        {
            float floatDestination = Input.GetAxis(trigger) * maxHeight;
            float currentHeight = transform.position.y - groundValue;
            playerVelocity.y = (floatDestination - currentHeight) * verticalSpeed;
            if (Input.GetAxis(joystickX) > 0)
            {
                rb.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
            }
            else if (Input.GetAxis(joystickX) < 0)
            {
                rb.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
            }
            float horizontalMovement = Input.GetAxis(joystickX) * horizontalSpeed;
            playerVelocity.x = horizontalMovement;
        }
        else if (orientation == Orientation.DOWN)
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
        rb.AddForce(gravity, ForceMode.VelocityChange);
        if (holdingObject && Input.GetButtonDown(interactButton))
        {
            Invoke("SetHoldingObjectFalse", 0.1f);
            objectHold.transform.parent = null;
            objectHold.transform.GetComponent<Rigidbody>().isKinematic = false;
            objectHold.transform.GetComponent<Collider>().enabled = true;
            objectHold = null;
        }
    }

    private void SetHoldingObjectFalse()
    {
        holdingObject = false;
    }

    private void OnTriggerStay(Collider collider)
    {
        if (!holdingObject && collider.CompareTag("Pick Up") && Input.GetButtonDown(interactButton))
        {
            holdingObject = true;
            objectHold = collider.gameObject;
            collider.transform.SetParent(transform);
            collider.transform.GetComponent<Rigidbody>().isKinematic = true;
            collider.transform.GetComponent<Collider>().enabled = false;
        }
    }
}