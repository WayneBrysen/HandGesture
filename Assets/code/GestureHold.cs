using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureHold : MonoBehaviour
{
    public Transform xrOrigin;
    public Transform handTransform;
    public GameObject railingPrefab;
    public float railingSpawnDistance = 0.2f;
    public float rotationSpeed = 100.0f;

    private GameObject spawnedRailing;
    private bool isHoldingRailing = false;
    private Vector3 lastHandPosition;

    void Update()
    {
        if (isHoldingRailing)
        {
            // �����ֲ����ƶ���
            Vector3 handMovement = handTransform.position - lastHandPosition;

            // ֻʹ��ˮƽ�ƶ�����X�ᣩ
            float horizontalMovement = handMovement.x;

            // ������ת�Ƕ�
            float angle = horizontalMovement * rotationSpeed;

            // ִ����ת
            RotateAroundRailing(angle);

            // �����ֲ�λ��
            lastHandPosition = handTransform.position;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == spawnedRailing)
        {
            isHoldingRailing = true;
            lastHandPosition = handTransform.position; // ��ʼ���ֲ�λ��
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == spawnedRailing)
        {
            isHoldingRailing = false;
        }
    }

    public void OnRailingGesturePerformed()
    {
        if (spawnedRailing == null)
        {
            // ���ֲ�ǰ����������
            Vector3 spawnPosition = handTransform.position + handTransform.forward * railingSpawnDistance;
            spawnedRailing = Instantiate(railingPrefab, spawnPosition, Quaternion.identity);

            // ȷ�����˾�����ײ��͸���
            Collider railingCollider = spawnedRailing.AddComponent<BoxCollider>();
            Rigidbody railingRigidbody = spawnedRailing.AddComponent<Rigidbody>();
            railingRigidbody.isKinematic = true;

            // ��������λ�ã�ʹ�䲻���ֲ��ƶ����ƶ�
            spawnedRailing.transform.SetParent(null);
        }
    }

    void RotateAroundRailing(float angle)
    {
        if (spawnedRailing != null)
        {
            // ������Ϊ֧�������ת��ֻӰ��xrOrigin
            xrOrigin.RotateAround(spawnedRailing.transform.position, Vector3.up, angle * Time.deltaTime);
        }
    }
}