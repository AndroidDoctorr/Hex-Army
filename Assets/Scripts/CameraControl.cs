using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private Vector3 lastMousePosition;
    private float minRotationX = 30.0f;
    private float maxRotationX = 45.0f;
    private float currentHeight = 15.0f;
    private float currentRotationX = 45.0f;
    private float minForwardOffset = -2.0f;
    private float maxForwardOffset = -12.0f;
    private float currentForwardOffset = -12.0f;
    private bool isRotating = false;

    public Transform Camera;
    public float dragSpeed = 5.0f;
    public float zoomSpeed = 5.0f;
    public float minX = -100.0f;
    public float maxX = 100.0f;
    public float minZ = -100.0f;
    public float maxZ = 100.0f;
    public float minHeight = 4.0f;
    public float maxHeight = 15.0f;
    public float rotationSpeed = 50.0f;

    void Start()
    {
        currentHeight = Mathf.Clamp(currentHeight, minHeight, maxHeight);
        currentRotationX = Mathf.Clamp(currentRotationX, minRotationX, maxRotationX);
        UpdateCameraPositionAndRotation();
    }

    void Update()
    {
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");

        if (scrollWheel != 0)
        {
            isRotating = false;
            currentHeight = Mathf.Clamp(currentHeight - scrollWheel * zoomSpeed, minHeight, maxHeight);
            float zoomAmount = (currentHeight - minHeight) / (maxHeight - minHeight);
            currentRotationX = Mathf.Lerp(minRotationX, maxRotationX, zoomAmount);
            currentForwardOffset = Mathf.Lerp(minForwardOffset, maxForwardOffset, zoomAmount);

            UpdateCameraPositionAndRotation();
        }

        if (Input.GetMouseButtonDown(0))
            lastMousePosition = Input.mousePosition;

        if (Input.GetMouseButtonDown(1))
        {
            isRotating = true;
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(1))
            isRotating = false;

        if (Input.GetMouseButton(0))
        {
            isRotating = false;
            Vector3 mouseDelta = Input.mousePosition - lastMousePosition;
            Vector3 newPosition = transform.position - new Vector3(mouseDelta.x, 0, mouseDelta.y) * dragSpeed * Time.deltaTime;
            transform.position = new Vector3(
                Mathf.Clamp(newPosition.x, minX, maxX),
                transform.position.y,
                Mathf.Clamp(newPosition.z, minZ, maxZ)
            );
            lastMousePosition = Input.mousePosition;
        }

        if (isRotating)
        {
            Vector3 mouseDelta = Input.mousePosition - lastMousePosition;
            float rotationAmount = mouseDelta.x * rotationSpeed * Time.deltaTime;
            transform.Rotate(new Vector3(0, rotationAmount, 0));
            lastMousePosition = Input.mousePosition;
        }
    }
    private void UpdateCameraPositionAndRotation()
    {
        // Set the camera's position and rotation based on current height and rotationX.
        Camera.localPosition = new Vector3(0, currentHeight, currentForwardOffset);
        Camera.localRotation = Quaternion.Euler(currentRotationX, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z);
    }
}
