using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles random rotation of hazard boxes
public class RandomRotator : MonoBehaviour
{
    // Speed of rotation
    public float tumble;

    // Rigidbody of the box
    private Rigidbody box;

    // Get the rigidbody and give it an angular velocity
    void Start ()
    {
        box = GetComponent<Rigidbody>();
        box.angularVelocity = Random.insideUnitSphere * tumble; // Vector3 with random x y z
    }
}
