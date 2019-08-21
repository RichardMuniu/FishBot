using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Destroyer script attached to the boundary
public class DestroyAtBoundary : MonoBehaviour
{
    // Destroy whatever leaves the boundary
    void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }
}
