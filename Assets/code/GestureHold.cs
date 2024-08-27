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

        // ʵ�������˲�������ת90��
        spawnedRailing = Instantiate(railingPrefab, spawnPosition, handTransform.rotation);

        // ��ת���ˣ�ʹ��������ʱ��ת90��
        spawnedRailing.transform.Rotate(0, 0, 90);

        // �����˸��ӵ��ֲ����Ա����ֲ��ƶ�
        spawnedRailing.transform.SetParent(handTransform);

        Debug.Log("Railing Spawned at: " + spawnPosition);
    }
}