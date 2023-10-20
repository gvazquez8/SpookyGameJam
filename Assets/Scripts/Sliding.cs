using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Credit to: 'Dave / GameDevelopment' on YouTube

public class Sliding : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform PlayerObj;
    private Rigidbody rb;
    private PlayerMovement pm;

    [Header("Sliding")]
    public float maxSlideTime;
    public float slideForce;
    private float slideTimer;

    public float slideYScale;
    private float startYScale;

    [Header("Input")]
    public KeyCode slideKey = KeyCode.LeftControl;
    private float horizontalInput;
    private float verticalInput;

    //private bool sliding;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement>();

        startYScale = PlayerObj.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(slideKey) && (horizontalInput != 0 || verticalInput != 0)) 
            StartSlide();

        if(Input.GetKeyUp(slideKey) && pm.sliding)
            StopSlide();
    }

    private void FixedUpdate()
    {
        if(pm.sliding)
            SlidingMovement();
    }

    // Handle Slide ==================
    private void StartSlide()
    {
        pm.sliding = true;
        
        // drop player height to crouch level and push player down
        PlayerObj.localScale = new Vector3(PlayerObj.localScale.x, slideYScale, PlayerObj.localScale.z);
        rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);

        slideTimer = maxSlideTime; //set timer
    }

    private void SlidingMovement()
    {
        // send player into a slide in whichever direction they're heading
        Vector3 inputDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // sliding on ground
        if(!pm.OnSlope() || rb.velocity.y > -0.1f)
        {
            // Calling the GetSlopeMovementDirection function from the PlayerMovement script
            rb.AddForce(inputDirection.normalized * slideForce, ForceMode.Force);
            
            slideTimer -= Time.deltaTime; // count down the timer
        }
        else 
        {
            // Calling the GetSlopeMovementDirection function from the PlayerMovement script
            rb.AddForce(pm.GetSlopeMovementDirection(inputDirection) * slideForce, ForceMode.Force);
        }

        if(slideTimer <= 0) // stop slide when timer ends
            StopSlide();
        
    }

    private void StopSlide()
    {
        pm.sliding = false;
        
        // reset player height
        PlayerObj.localScale = new Vector3(PlayerObj.localScale.x, startYScale, PlayerObj.localScale.z);
    }
    // ===============================
}
