using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class PlayerPhysicController : MonoBehaviour
{
    private enum Orientation { UP, DOWN }

    private enum Player { PLAYER1, PLAYER2 }

    [SerializeField] private Orientation orientation;
    [SerializeField] private Vector3 gravity;
    [SerializeField] private float maxHeight;
    [SerializeField] private float groundValue;
    [SerializeField] private float verticalSpeed;
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private Player player;
    [SerializeField] private GameObject particleEffect;
    [SerializeField] private float timeTillPlayerRespawn = 1.5f;
    private Vector3 startPosition;
    private string trigger;
    private string joystickX;
    private string joystickY;
    private string interactButton;
    private string duckButton;

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
        startPosition = rb.position;
        SetControlsStrings();
        ResetPosition.onPlayerCollision += PlayerHit;
        if (orientation == Orientation.UP)
        {
            rb.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        else if (orientation == Orientation.DOWN)
        {
            rb.rotation = Quaternion.Euler(new Vector3(180, 0, 0));
        }
    }

    private void OnDisable()
    {
        ResetPosition.onPlayerCollision -= PlayerHit;
    }

    private void FixedUpdate()
    {
        playerVelocity = Vector3.zero;
        animationWalkingSpeed = Mathf.Abs(Input.GetAxis(joystickX));
        if (orientation == Orientation.UP)
        {
            float floatDestination = Input.GetAxis(trigger) * maxHeight;
            if (animationCrouched)
            {
                floatDestination = 0;
            }
            float currentHeight = transform.position.y - groundValue;
            playerVelocity.y = (floatDestination - currentHeight) * verticalSpeed;
            if (Input.GetAxis(joystickX) > 0)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
            else if (Input.GetAxis(joystickX) < 0)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
            float horizontalMovement = Input.GetAxis(joystickX) * horizontalSpeed;
            playerVelocity.x = horizontalMovement;
        }
        else if (orientation == Orientation.DOWN)
        {
            float floatDestination = -Input.GetAxis(trigger) * maxHeight;
            if (animationCrouched)
            {
                floatDestination = 0;
            }
            float currentHeight = transform.position.y + groundValue;
            playerVelocity.y = (floatDestination - currentHeight) * verticalSpeed;
            if (Input.GetAxis(joystickX) > 0)
            {
                transform.rotation = Quaternion.Euler(new Vector3(180, 180, 0));
            }
            else if (Input.GetAxis(joystickX) < 0)
            {
                transform.rotation = Quaternion.Euler(new Vector3(180, 0, 0));
            }
            float horizontalMovement = Input.GetAxis(joystickX) * horizontalSpeed;
            playerVelocity.x = horizontalMovement;
        }
        if (Input.GetButton(duckButton))
        {
            animationCrouched = true;
            if (Controls.TwoPlayerMode)
            {
                GamePad.SetVibration(player == Player.PLAYER1 ? PlayerIndex.One : PlayerIndex.Two, 0.1f, 0.1f);
            }
            else
            {
                GamePad.SetVibration(PlayerIndex.One, 0.1f, 0.1f);
            }
            playerVelocity = new Vector3(playerVelocity.x / 2, playerVelocity.y);
        }
        else
        {
            animationCrouched = false;
            if (Controls.TwoPlayerMode)
            {
                GamePad.SetVibration(player == Player.PLAYER1 ? PlayerIndex.One : PlayerIndex.Two, 0.0f, 0.0f);
            }
            else
            {
                GamePad.SetVibration(PlayerIndex.One, 0.0f, 0.0f);
            }
        }
        rb.velocity = playerVelocity * Time.fixedDeltaTime;
        rb.AddForce(gravity * Time.fixedDeltaTime, ForceMode.VelocityChange);
        //if (holdingObject && Input.GetButtonDown(interactButton))
        //{
        //    Invoke("SetHoldingObjectFalse", 0.5f);
        //    objectHold.transform.parent = null;
        //    objectHold.transform.rotation = Quaternion.Euler(0, 0, 0);
        //    objectHold.transform.localPosition = new Vector3(objectHold.transform.position.x, objectHold.transform.position.y, 0);
        //    objectHold.transform.GetComponent<Rigidbody>().isKinematic = false;
        //    objectHold.transform.GetComponent<Collider>().isTrigger = false;
        //    objectHold.GetComponent<GravityScript>().IsObjectHeld = false;
        //    objectHold = null;
        //}
        if (Physics.Raycast(transform.position + (player == Player.PLAYER1 ? Vector3.up : Vector3.down), player == Player.PLAYER1 ? Vector3.down : Vector3.up, 1.5f))
        {
            animationFlying = false;
        }
        else
        {
            animationFlying = true;
        }
    }

    private void PlayerHit(string name)
    {
        if (name == gameObject.name)
        {
            KillPlayer();
            Invoke("RespawnPlayer", timeTillPlayerRespawn);
        }
    }

    private void RespawnPlayer()
    {
        rb.position = startPosition;
    }

    private void KillPlayer()
    {
        Instantiate(particleEffect, transform.position, Quaternion.identity);
        rb.position = new Vector3(99, 99, 99);
    }

    private void SetControlsStrings()
    {
        trigger = player == Player.PLAYER1 ? Controls.TopPlayerHover : Controls.BotoomPlayerHover;
        joystickX = player == Player.PLAYER1 ? Controls.TopPlayerMovementX : Controls.BottomPlayerMovementX;
        joystickY = player == Player.PLAYER1 ? Controls.TopPlayerMovementY : Controls.BottomPlayerMovementY;
        duckButton = player == Player.PLAYER1 ? Controls.TopPlayerDuck : Controls.BottomPlayerDuck;
    }

    //private void SetHoldingObjectFalse()
    //{
    //    holdingObject = false;
    //}

    //private void OnTriggerStay(Collider collider)
    //{
    //    if (!holdingObject && collider.CompareTag("Pick Up") && Input.GetButtonDown(interactButton))
    //    {
    //        holdingObject = true;
    //        objectHold = collider.gameObject;
    //        collider.transform.SetParent(transform.GetChild(0).GetChild(0).GetChild(0));
    //        objectHold.transform.localPosition = new Vector3(0, 0.4f, 0);
    //        collider.transform.GetComponent<Rigidbody>().isKinematic = true;
    //        collider.transform.GetComponent<Collider>().isTrigger = true;
    //        collider.GetComponent<GravityScript>().IsObjectHeld = true;
    //    }
    //}

    public void StartSound(AudioClip audioClip)
    {
        GetComponent<AudioSource>().clip = audioClip;
        GetComponent<AudioSource>().Play();
    }

    public void StopSound()
    {
        GetComponent<AudioSource>().Stop();
    }
}