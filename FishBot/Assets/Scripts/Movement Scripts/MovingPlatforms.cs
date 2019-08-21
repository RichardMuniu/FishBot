using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles moving platforms
public class MovingPlatforms : MonoBehaviour
{
    public Transform movingPlatform; // Moving platform
    public Transform pos1;           // Position 1
    public Transform pos2;           // Position 2
    public Vector3 newPosition;      // Goal position of the platform
    public string currentState;      // Can be platform moving of position 1 or position 2 (position 2 by defualt)
    public float smooth;             // Smoothness of movement
    public float resetTime;          // Waiting time between switching tagets

    // Start is called before the first frame update
    void Start()
    {
        ChangeTarget();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Keep updating position
        movingPlatform.position = Vector3.Lerp(movingPlatform.position,newPosition,smooth*Time.deltaTime);
    }

    // Switches targets
    void ChangeTarget()
    {
        if(currentState == "Moving To Position 1"){
            currentState = "Moving To Position 2";
            newPosition = pos2.position;
        }
        else if(currentState == "Moving To Position 2"){
            currentState = "Moving To Position 1";
            newPosition = pos1.position;
        }
        else if(currentState == ""){
            currentState = "Moving To Position 2";
            newPosition = pos2.position;
        }
        // Change target after some time
        Invoke("ChangeTarget", resetTime);
    }
}
