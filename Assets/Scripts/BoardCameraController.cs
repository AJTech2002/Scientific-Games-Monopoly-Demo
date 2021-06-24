using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCameraController : MonoBehaviour
{
    private Vector3 currentMouse;
    private Vector3 mouseDelta;
    private Vector3 lastMouse;

    public Vector3 center;

    public float mouseDeltaSpeed;

    public float speed;


    public static bool activeRotate = false;
    public static Vector3 pivotPoint;

    [Header("Zooming")]
    public float scrollSpeed;

    private void OnDrawGizmos()
    {
        //Center Position Debugged
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(center, 1f);

    }

    private void Update()
    {
        mouseDelta = lastMouse - currentMouse;

        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {

                pivotPoint = hit.point;
            }

            activeRotate = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            activeRotate = false;
        }

        transform.position += (-transform.position).normalized*Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
       

        currentMouse = Input.mousePosition;

        mouseDelta = lastMouse - currentMouse;

    }
    void LateUpdate()
    {
        if (activeRotate)
        {
            transform.RotateAround(center, Vector3.up, mouseDelta.x * -mouseDeltaSpeed);
            transform.RotateAround(center, transform.right, mouseDelta.y * mouseDeltaSpeed);
        }

        lastMouse = Input.mousePosition;
    }
}
