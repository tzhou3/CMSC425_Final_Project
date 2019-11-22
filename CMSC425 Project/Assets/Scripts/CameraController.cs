using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public Vector3 offset;
    public float zoomSpeed = 4f;
    public float zoomMin = 5f;
    public float zoomMax = 15f;
    public float pitch = 2f;
    public float currZoom = 10f;


    // Update is called once per frame
    void Update()
    {
        currZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currZoom = Mathf.Clamp(currZoom, zoomMin, zoomMax);
    }

    void LateUpdate()
    {
        transform.position = target.position - offset * currZoom;
        transform.LookAt(target.position + Vector3.up * pitch);
    }
}
