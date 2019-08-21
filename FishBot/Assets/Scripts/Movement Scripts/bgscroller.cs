using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles scrolling the background
public class bgscroller : MonoBehaviour
{
    // Speed of scrolling
    public float scrollSpeed;
    // Tile width
    public float tileSizeZ;

    // Starting position
    private Vector3 startPosition;

    void Start () 
    {
        startPosition = transform.position;
    }
    
    void Update ()
    {
        // Loops the value of the new position based on the width of the tile
        float newPosition = Mathf.Repeat (Time.time * scrollSpeed, tileSizeZ);
        // Linear update of position moving to the right
        transform.position = startPosition + Vector3.right * newPosition;
    }
}


    
