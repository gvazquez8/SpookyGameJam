using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Credit to: 'Dave / GameDevelopment' on YouTube for this great information and code.

public class MoveCamera : MonoBehaviour
{
    public Transform cameraPosition;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = cameraPosition.position;  
    }
}
