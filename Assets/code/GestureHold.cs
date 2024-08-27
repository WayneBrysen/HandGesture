using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureHold : MonoBehaviour
{
    public Transform xrOrigin;
    public Transform handTransform;
    public GameObject railingPrefab;
    public float rotationSensitivity = 100.0f; 
    public float railingOffset = 10f; 

    private Vector3 initialHandPosition;
    private bool isGestureActive = false;
    private GameObject spawnedRailing;

    void Update()
    {
        if (isGestureActive)
        {
            float handDeltaX = handTransform.position.x - initialHandPosition.x;

            Debug.Log("Hand Delta X: " + handDeltaX);

            float rotationAngle = -handDeltaX * rotationSensitivity;

            Debug.Log("Rotation Angle: " + rotationAngle);

            xrOrigin.Rotate(Vector3.up, rotationAngle * Time.deltaTime);

            Debug.Log("XR Origin Rotation after Update: " + xrOrigin.rotation.eulerAngles);

            initialHandPosition = handTransform.position;

            if (spawnedRailing == null)
            {
                SpawnRailing();
            }
        }
        else
        {
            if (spawnedRailing != null)
            {
                Destroy(spawnedRailing);
            }
        }
    }

    public void OnGestureStart()
    {
        isGestureActive = true;
        initialHandPosition = handTransform.position;

        Debug.Log("Gesture Started. Initial Hand Position: " + initialHandPosition);
    }

    public void OnGestureEnd()
    {
        isGestureActive = false;

        Debug.Log("Gesture Ended.");
    }

    void SpawnRailing()
    {
        Vector3 spawnPosition = handTransform.position + handTransform.forward * railingOffset - handTransform.up * 0.04f;

        // 实例化栏杆并将其旋转90度
        spawnedRailing = Instantiate(railingPrefab, spawnPosition, handTransform.rotation);

        // 旋转栏杆，使其在生成时旋转90度
        spawnedRailing.transform.Rotate(0, 0, 90);

        // 将栏杆附加到手部，以便随手部移动
        spawnedRailing.transform.SetParent(handTransform);

        Debug.Log("Railing Spawned at: " + spawnPosition);
    }
}