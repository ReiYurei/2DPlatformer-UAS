using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimRotation : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    private PlayerMoveController playerScript;
    private float rotZ;
    private bool isFacingRight;
    private float objectAngle;
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMoveController>();
    }


    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;

        //Convert the value as Degree
        rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        //Aim target rotation


        /*If player grounded, rotation were limitted to 180 degree(only straight dash allowed
          depend on where the player cursor facing*/
        if (rotZ < 0 && rotZ > -90 && playerScript.Grounded == true)
        {
            rotZ = rotZ + -rotZ;
            transform.rotation = Quaternion.Euler(0, 0, rotZ);

        }
        if (rotZ < -90 && playerScript.Grounded == true)
        {
            rotZ = 180;
            transform.rotation = Quaternion.Euler(0, 0, rotZ);
        }
        //By default it could aim 360 degree
        else
        {
            //To make sure the rotation is pivoting around Z axis
            transform.rotation = Quaternion.Euler(0, 0, rotZ);
        }



    }
    public void Angle()
    {
        if (playerScript.state == PlayerState.ATTACKING)
        {
            objectAngle = rotZ;
        }
    }
    public void FacingState()  //If it's coordinate is 90 to -90, it's facing right

    {
        if (playerScript.state == PlayerState.ATTACKING)
        {
            if (rotZ <= 90 && rotZ >= -90)
            {
                isFacingRight = true;
            }
            else
            {
                isFacingRight = false;
            }
        }

    }
    public bool FacingRight
    {
        get { return isFacingRight; }
        set { isFacingRight = value; }
    }
    public float mouseAngle
    {
        get { return objectAngle; }
        set { objectAngle = value; }
    }
}
