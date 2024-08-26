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
            // 计算手部的移动量
            Vector3 handMovement = handTransform.position - lastHandPosition;

            // 只使用水平移动量（X轴）
            float horizontalMovement = handMovement.x;

            // 计算旋转角度
            float angle = horizontalMovement * rotationSpeed;

            // 执行旋转
            RotateAroundRailing(angle);

            // 更新手部位置
            lastHandPosition = handTransform.position;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == spawnedRailing)
        {
            isHoldingRailing = true;
            lastHandPosition = handTransform.position; // 初始化手部位置
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
            // 在手部前方生成栏杆
            Vector3 spawnPosition = handTransform.position + handTransform.forward * railingSpawnDistance;
            spawnedRailing = Instantiate(railingPrefab, spawnPosition, Quaternion.identity);

            // 确保栏杆具有碰撞体和刚体
            Collider railingCollider = spawnedRailing.AddComponent<BoxCollider>();
            Rigidbody railingRigidbody = spawnedRailing.AddComponent<Rigidbody>();
            railingRigidbody.isKinematic = true;

            // 锁定栏杆位置，使其不随手部移动而移动
            spawnedRailing.transform.SetParent(null);
        }
    }

    void RotateAroundRailing(float angle)
    {
        if (spawnedRailing != null)
        {
            // 以栏杆为支点进行旋转，只影响xrOrigin
            xrOrigin.RotateAround(spawnedRailing.transform.position, Vector3.up, angle * Time.deltaTime);
        }
    }
}