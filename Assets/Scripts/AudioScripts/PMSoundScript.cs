using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepScript : MonoBehaviour
{
    [Header("References")]
    //public Transform orientation;
    public PlayerMovement pm;
    public Rigidbody rb;
    public Transform orientation;

    public GameObject footstep;
    public GameObject sprintstep;
    public GameObject slidesound;
    public GameObject dashsound;

    // Start is called before the first frame update
    void Start()
    {
        footstep.SetActive(false);
        sprintstep.SetActive(false);
        slidesound.SetActive(false);
        dashsound.SetActive(false);

        pm = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pm.grounded && (Mathf.Round(Mathf.Abs(rb.velocity.x + rb.velocity.y + rb.velocity.z))) != 0)
        {
            if(pm.state.ToString() == "walking")
            {
                stopSprintSteps();
                footsteps();
            }
            else if(pm.state.ToString() == "sprinting")
            {
                stopFootsteps();
                sprintSteps();
            }

            Debug.Log(pm.state.ToString());
        }
        else 
        {
            stopFootsteps();
            stopSprintSteps();
        }

        if(pm.sliding && pm.grounded)
        {
            stopFootsteps();
            stopSprintSteps();
            slideSound();
        }
        else 
        {
            stopSlideSound();
        }

        if(pm.dashing)
        {
            stopFootsteps();
            stopSprintSteps();
            stopSlideSound();
            //
            dashSound();
        }
        else 
        {
            stopDashSound();
        }
        
    }
    // walking
    void footsteps()
    {
        footstep.SetActive(true);
    }
    void stopFootsteps()
    {
        footstep.SetActive(false);
    }
    
    // sprinting
    void sprintSteps()
    {
        sprintstep.SetActive(true);
    }    
    void stopSprintSteps()
    {
        sprintstep.SetActive(false);
    }

    // slide
    void slideSound()
    {
        slidesound.SetActive(true);
    }
    void stopSlideSound()
    {
        slidesound.SetActive(false);
    }
    
    //dash
    void dashSound()
    {
        dashsound.SetActive(true);
    }
    void stopDashSound()
    {
        dashsound.SetActive(false);
    }
}
