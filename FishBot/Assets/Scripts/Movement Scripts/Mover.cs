using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles moving the projectile and the hazard box
public class Mover : MonoBehaviour
{
    public float speed;   // Speed of movement

    private Rigidbody rb; // Private rigidbody variable

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb.tag == "Projectile")
        {
            rb.velocity = new Vector3(20, 0, 0);
        } else if (rb.tag == "Hazardbox")
        {
            rb.velocity = new Vector3(0, -5, 0);
        } 
    }

}

////////////// DIFFERENT CODE //////////////
/*
    //Rotate the sprite about the Y axis in the positive direction
    //projectile.transform.Rotate(new Vector3(0, 90, 0));
    //Move the Rigidbody forwards constantly at speed you define (the blue arrow axis in Scene view)
    // projectile.velocity = transform.forward * speed;
    //rb.transform.Translate(Vector3.down * speed * Time.deltaTime);

    //Rotate the sprite about the Y axis in the positive direction
    //projectile.transform.Rotate(new Vector3(0, 90, 0));
    //Move the Rigidbody forwards constantly at speed you define (the blue arrow axis in Scene view)
    // projectile.velocity = transform.forward * speed;
    //rb.transform.Translate(Vector3.left * speed * Time.deltaTime);
*/