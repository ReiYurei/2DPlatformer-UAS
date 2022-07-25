using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    public float smoothSpeed = 6f; 
    void FixedUpdate()
    {

        Vector3 desiredPos;
        Vector3 smoothedPos;
        if (target.transform.position.x <= 4)
        {
            desiredPos = new Vector3(0, target.position.y, offset.z);
            smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPos;

        }
        else if (target.transform.position.x >= 4)
        {
            desiredPos = new Vector3(target.position.x, target.position.y, offset.z);
            smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPos;
        }

    }
}
