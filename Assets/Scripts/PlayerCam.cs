using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX; // detects mouse x-axis sensitivity
    public float sensY; // detects mouse y-axis sensitivity

    public Transform orientation; // players orientation

    float xRotation;
    float yRotation; // storing player direction

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // make the mouse invis & locked to the center of the screen
    }

    // Update is called once per frame
    void Update()
    {
        // get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        // multiply raw mouse input by time & sensitivity

        yRotation += mouseX; // add X input to your Y rotation
        xRotation -= mouseY; // subtract Y input to your X rotation
        
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        //keeps the camera from looking passed straight up and down

        //rotate cam & orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0); // cam up and down
        orientation.rotation = Quaternion.Euler(0, yRotation, 0); // Rotates along y axis
        
        /* I think the Unity X axis is the vertical plane while the Y axis is along the horizontal */
        
    }
}
