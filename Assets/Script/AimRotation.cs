using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimRotation : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
   
    void Start()
    {
        mainCam =GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

   
    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;

        //Convert the value as Degree
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        //To make sure the rotation is pivoting around Z axis
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
        //Debug.Log(rotZ);
    }
}
