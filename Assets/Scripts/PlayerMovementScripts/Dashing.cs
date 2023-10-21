using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Credit to: 'Dave / GameDevelopment' on YouTube for this great information and code.

public class Dashing : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform playerCam;
    private Rigidbody rb;
    private PlayerMovement pm;

    [Header("Dashing")]
    public float dashForce;
    public float dashUpwardForce;
    public float dashDuration;

    public int dashCounter;

    [Header("Cooldown")]
    public float dashCd;
    private float dashCdTimer;

    [Header("Input")]
    public KeyCode dashKey = KeyCode.E; // Subject to change


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement>();    

        dashCounter = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(dashKey))
            Dash();
        
        if(dashCdTimer > 0)
            dashCdTimer -= Time.deltaTime;

    }

    private void Dash()
    {
        if(dashCdTimer > 0 || dashCounter == 0) return;
        else dashCdTimer = dashCd;

        pm.dashing = true;
        if(dashCounter > 0) dashCounter--;

        Vector3 forceToApply = orientation.forward * dashForce + orientation.up * dashUpwardForce;
        
        delayedForceToApply = forceToApply;
        Invoke(nameof(DelayedDashForce), 0.025f);

        Invoke(nameof(ResetDash), dashDuration);

    }

    private Vector3 delayedForceToApply;
    private void DelayedDashForce()
    {
        rb.AddForce(delayedForceToApply, ForceMode.Impulse); 
    }

    private void ResetDash()
    {
        pm.dashing = false;
    }
}
