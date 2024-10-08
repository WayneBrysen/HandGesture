using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureSimple : MonoBehaviour
{
    public Transform xrOrigin; // Reference to the XR Origin for movement and rotation
    public Transform gestureTransform; // Reference to the transform used for gesture input
    private CharacterController characterController;

    public float baseSpeed = 1.0f; // Base speed for movement
    public float maxSpeed = 5.0f; // Maximum speed when the gesture is at max distance
    public float minSpeed = 0.5f; // Minimum speed when the gesture is at min distance
    public float maxDistance = 0.5f; // Maximum distance for gesture scaling
    public float minDistance = 0.3f; // Minimum distance for gesture scaling

    public float maxRotationSpeed = 50.0f; // Maximum rotation speed
    public float minRotationSpeed = 10.0f; // Minimum rotation speed

    private bool isMovingForward = false; // State to check if moving forward
    private bool isMovingBackward = false; // State to check if moving backward
    private bool isRotatingLeft = false; // State to check if rotating left
    private bool isRotatingRight = false; // State to check if rotating right
    void Start()
    {
        characterController = xrOrigin.GetComponent<CharacterController>();
    }

    void Update()
    {
        // Handle movement
        if (isMovingForward)
        {
            Move(true);
        }
        else if (isMovingBackward)
        {
            Move(false);
        }

        // Handle rotation
        if (isRotatingLeft)
        {
            Rotate(true);
        }
        else if (isRotatingRight)
        {
            Rotate(false);
        }
    }


    // Triggered when a forward gesture is performed
    public void OnForwardGesturePerformed()
    {
        isMovingForward = true;
    }

    // Triggered when a backward gesture is performed
    public void OnBackwardGesturePerformed()
    {
        isMovingBackward = true;
    }

    // Triggered when a left rotation gesture is performed
    public void OnLeftRotationGesturePerformed()
    {
        isRotatingLeft = true;
    }

    // Triggered when a right rotation gesture is performed
    public void OnRightRotationGesturePerformed()
    {
        isRotatingRight = true;
    }

    // Triggered when any gesture ends
    public void OnGestureEnded()
    {
        isMovingForward = false;
        isMovingBackward = false;
        isRotatingLeft = false;
        isRotatingRight = false;
    }

    // Handles movement logic
    void Move(bool forward)
    {
        Vector3 direction = Camera.main.transform.forward;
        if (!forward)
        {
            direction = -direction;
        }

        direction.y = 0; // 保持水平移动
        direction.Normalize();

        float distance = Vector3.Distance(gestureTransform.position, Camera.main.transform.position);

        float adjustedSpeed;
        if (distance <= minDistance)
        {
            adjustedSpeed = minSpeed;
        }
        else if (distance >= maxDistance)
        {
            adjustedSpeed = maxSpeed;
        }
        else
        {
            float speedFactor = Mathf.InverseLerp(minDistance, maxDistance, distance);
            adjustedSpeed = Mathf.Lerp(minSpeed, maxSpeed, speedFactor);
        }

        // 使用CharacterController移动
        characterController.Move(direction * adjustedSpeed * Time.deltaTime);
    }

    // Handles rotation logic
    void Rotate(bool left)
    {
        float distance = Vector3.Distance(gestureTransform.position, Camera.main.transform.position); // Calculate distance from gesture to camera

        // Adjust rotation speed based on distance
        float adjustedRotationSpeed;
        if (distance <= minDistance)
        {
            adjustedRotationSpeed = minRotationSpeed; // Set to minimum rotation speed if distance is below the minimum threshold
        }
        else if (distance >= maxDistance)
        {
            adjustedRotationSpeed = maxRotationSpeed; // Set to maximum rotation speed if distance exceeds the maximum threshold
        }
        else
        {
            // Linearly interpolate rotation speed between minRotationSpeed and maxRotationSpeed based on the distance
            float speedFactor = Mathf.InverseLerp(minDistance, maxDistance, distance);
            adjustedRotationSpeed = Mathf.Lerp(minRotationSpeed, maxRotationSpeed, speedFactor);
        }

        float rotationDirection = left ? -1 : 1; // Determine the rotation direction

        // Apply the rotation to the XR origin
        xrOrigin.Rotate(Vector3.up, rotationDirection * adjustedRotationSpeed * Time.deltaTime);
    }
}