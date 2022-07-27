using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Camera cam;
    private BoxCollider2D camBox;
    private float sizeX, sizeY, ratio;
    private Transform target;
    public Vector3 offset;
    private Vector3 velocity = Vector3.zero;

    public float smoothSpeed = 100f;
    private void Start()
    {
        cam = GetComponent<Camera>();
        camBox = GetComponent<BoxCollider2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    private void Update()
    {
        sizeY = cam.orthographicSize * 2;
        ratio = (float)Screen.width / (float)Screen.height;
        sizeX = sizeY * ratio;
        camBox.size = new Vector2 (sizeX, sizeY);
    }
    void LateUpdate()
    {

        Vector3 desiredPos;
        Vector3 smoothedPos;
        if (target.transform.position.x < -0.7f)
        {
            desiredPos = new Vector3(0, target.position.y, offset.z);
            smoothedPos = Vector3.SmoothDamp(transform.position, desiredPos, ref velocity, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPos;

        }
        else if (target.transform.position.x > -0.7f)
        {
            desiredPos = new Vector3(target.position.x, target.position.y, offset.z);
            smoothedPos = Vector3.SmoothDamp(transform.position, desiredPos, ref velocity, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPos;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Optimize")
        {
            for (int i = 0; i < collision.transform.childCount; i++)
            {
             
                collision.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.transform.tag == "Optimize")
        {
            for (int i = 0; i < collision.transform.childCount; i++)
            {
                collision.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
