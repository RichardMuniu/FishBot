using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Destroyer script attached to explosion objects so that they disappear after some time
public class DestroyAtTime : MonoBehaviour
{
    // Life-time of object
    public float lifeTime;

    // Start is called before the first frame update
    void Start()
    {
        // Destroy the game object attached after a certain lifetime
        Destroy(gameObject, lifeTime);
    }
}
