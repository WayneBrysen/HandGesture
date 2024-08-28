using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureHold : MonoBehaviour
{
    public Transform xrOrigin; // Reference to the XR Origin for rotation
    public Transform handTransform; // Reference to the transform representing the hand
    public GameObject railingPrefab; // Prefab for the railing to spawn
    public float rotationSensitivity = 100.0f; // Sensitivity factor for rotation based on hand movement
    public float railingOffset = 10f; // Distance offset for spawning the railing

    private Vector3 initialHandPosition; // Initial position of the hand when the gesture starts
    private bool isGestureActive = false; // Flag to check if the gesture is currently active
    private GameObject spawnedRailing; // Reference to the spawned railing object

    void Update()
    {
        if (isGestureActive)
        {
            // Calculate horizontal movement of the hand
            float handDeltaX = handTransform.position.x - initialHandPosition.x;
            Debug.Log("Hand Delta X: " + handDeltaX);

            // Calculate rotation angle based on hand movement and sensitivity
            float rotationAngle = -handDeltaX * rotationSensitivity;
            Debug.Log("Rotation Angle: " + rotationAngle);

            // Apply rotation to the XR Origin
            xrOrigin.Rotate(Vector3.up, rotationAngle * Time.deltaTime);
            Debug.Log("XR Origin Rotation after Update: " + xrOrigin.rotation.eulerAngles);

            // Update the initial hand position for the next frame
            initialHandPosition = handTransform.position;

            // Check if railing is already spawned; if not, spawn it
            if (spawnedRailing == null)
            {
                SpawnRailing();
            }
        }
        else
        {
            // Destroy the spawned railing if the gesture is not active
            if (spawnedRailing != null)
            {
                Destroy(spawnedRailing);
            }
        }
    }

    // Called when the gesture is started
    public void OnGestureStart()
    {
        isGestureActive = true;
        initialHandPosition = handTransform.position; // Record initial hand position
        Debug.Log("Gesture Started. Initial Hand Position: " + initialHandPosition);
    }

    // Called when the gesture ends
    public void OnGestureEnd()
    {
        isGestureActive = false; // Set gesture as inactive
        Debug.Log("Gesture Ended.");
    }

    // Spawns the railing object at the calculated position
    void SpawnRailing()
    {
        // Calculate the spawn position based on hand position and offset
        Vector3 spawnPosition = handTransform.position + handTransform.forward * railingOffset - handTransform.up * 0.04f;

        // Instantiate the railing prefab at the calculated position and with the hand's rotation
        spawnedRailing = Instantiate(railingPrefab, spawnPosition, handTransform.rotation);

        // Rotate the spawned railing to match desired orientation
        spawnedRailing.transform.Rotate(0, 0, 90);

        // Set the railing as a child of the hand transform
        spawnedRailing.transform.SetParent(handTransform);

        Debug.Log("Railing Spawned at: " + spawnPosition);
    }
}