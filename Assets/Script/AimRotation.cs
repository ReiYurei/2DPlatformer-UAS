using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimRotation : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    private PlayerMoveController playerScript;

    void Start()
    {
        mainCam =GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMoveController>();
    }

   
    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;

        //Convert the value as Degree
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        /*If player grounded, rotation were limitted to 180 degree(only straight dash allowed
        depend on where the player cursor facing*/
        if (rotZ < 0 && rotZ > -90 && playerScript.Grounded == true)
        {
            rotZ = rotZ + -rotZ;
            transform.rotation = Quaternion.Euler(0, 0, rotZ);

        }
        if (rotZ < -90 && playerScript.Grounded == true)
        {
            rotZ= 180;
            transform.rotation = Quaternion.Euler(0, 0, rotZ);
        }
        //By default it could aim 360 degree
        else
        {
            //To make sure the rotation is pivoting around Z axis
            transform.rotation = Quaternion.Euler(0, 0, rotZ);
        }
        
        Debug.Log(rotZ);
    }
}
