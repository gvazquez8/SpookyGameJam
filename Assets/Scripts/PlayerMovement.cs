using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Credit to: 'Dave / GameDevelopment' on YouTube for this great information and code.

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    private float moveSpeed; // player movement speed
    public float walkSpeed;
    public float sprintSpeed;

    public float groundDrag; 

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope; // to allow us to jump on sloped

    public Transform orientation; // player orientation

    float horizontalInput; // horizontal inputs 
    float verticalInput; // jump / vertical inputs ??

    Vector3 moveDirection; // self exp

    Rigidbody rb; // reference to the rb

    public MovementState state; // this will store which state the player is in
    // The states we will use to modify moveSpeed (sprinting, walking, else)
    public enum MovementState
    {
        walking, 
        sprinting, 
        crouching,
        air
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // get the rb 
        rb.freezeRotation = true;    // freeze rotation so player doesn't fall
        readyToJump = true;

        startYScale = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        // Ground check
        grounded = Physics.Raycast (transform.position, Vector3.down, playerHeight * 0.5f + 0.2f,
                                    whatIsGround);
        MyInput();
        SpeedControl();
        StateHandler();
        // apply drag
        if(grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void FixedUpdate() 
    {
        MovePlayer();    
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //when to jump
        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {

            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        // start crouch
        if (Input.GetKeyDown(crouchKey))
        {
            // change the Y scale on the player, but keep X and Z as is
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse); // push the player down
        }

        // stop crouch
        if (Input.GetKeyUp(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }
    }

    private void StateHandler () // handles player movespeed state
    {
        // Mode - Crouching
        if(Input.GetKey(crouchKey))
        {
            state  = MovementState.crouching;
            moveSpeed = crouchSpeed;
        }
        // Mode - Sprinting
        else if(grounded && Input.GetKey(sprintKey))
        {
            state  = MovementState.sprinting;
            moveSpeed = sprintSpeed; 
        }
        // Mode - Walking
        else if(grounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        } 
        // Mode - else
        else 
        {
            state = MovementState.air;
        }
            
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        /* 
            This is written this way so we always walk in the direction
            we're looking
        */

        // on slope
        if(OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMovementDirection() * moveSpeed * 20f, ForceMode.Force);
            if(rb.velocity.y > 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }
        // on ground
        if(grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        // on air
        else if(!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        
        // turn off gravity while on slope
        rb.useGravity = !OnSlope();
    }// this will give player movement

    private void SpeedControl()
    {
        // speed limiter on slope
        if(OnSlope())
        {
            if (rb.velocity.magnitude > moveSpeed)
                rb.velocity = rb.velocity.normalized * moveSpeed;
        }

        // limiting speed on ground or in air
        else
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
        /* 
            if you go faster than your movespeed, you calculate what would be your max movespeed,
            then apply it
        */
        }
    }

    private void Jump()
    {
        exitingSlope = true;
        //reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
        exitingSlope = false;
    }

    private bool OnSlope()
    {   
        //sends a raycast down to check if we're on a slope
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    private Vector3 GetSlopeMovementDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }
}
