
using UnityEngine;

// Handles the camera following the player
public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    //ran after update
    void Update()
    {
        if(transform.gameObject.activeSelf)
        {
            transform.position = new Vector3(target.transform.position.x + offset.x, transform.position.y, transform.position.z);
        }
    }
}

////// DIFFERENT CODE //////
/*
    float angle = 0;
    Vector3 relative = transform.InverseTransformPoint(target.position);
    angle = Mathf.Atan2(relative.z, relative.y)*Mathf.Rad2Deg;
    transform.Rotate(0,0,-angle);
    //transform.LookAt(target, Vector3.up);
    //Vector3 desiredPosition = target.position + offset;
    //Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    //target.position = smoothedPosition;
    //var lookAtPoint = target.position;
    //lookAtPoint.y = transform.position.y;
    //transform.LookAt(lookAtPoint);
*/