using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;
    public Vector3 offset;
    private Vector3 velocity = Vector3.zero;

    public float smoothSpeed = 10000f;
    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    void LateUpdate()
    {

        Vector3 desiredPos;
        Vector3 smoothedPos;
        if (target.transform.position.x <= 4)
        {
            desiredPos = new Vector3(0, target.position.y, offset.z);
            smoothedPos = Vector3.SmoothDamp(transform.position, desiredPos, ref velocity, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPos;

        }
        else// if (target.transform.position.x >= 4)
        {
            desiredPos = new Vector3(target.position.x, target.position.y, offset.z);
            smoothedPos = Vector3.SmoothDamp(transform.position, desiredPos, ref velocity, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPos;
        }

    }
}
