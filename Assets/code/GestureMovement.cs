using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureMovement : MonoBehaviour
{
    public Transform xrOrigin;
    public Transform gestureTransform; // L_Wrist的Transform，用于跟踪手势
    public float baseSpeed = 1.0f;
    public float maxSpeed = 5.0f; // 最大速度
    public float minSpeed = 0.5f; // 最小速度
    public float maxDistance = 0.4f; // 手势距离最大值（40厘米）
    public float minDistance = 0.3f; // 手势距离最小值（25厘米）

    private bool isMovingForward = false;
    private bool isMovingBackward = false;

    void Update()
    {
        if (isMovingForward)
        {
            Move(true);
        }
        else if (isMovingBackward)
        {
            Move(false);
        }
    }

    public void OnForwardGesturePerformed()
    {
        isMovingForward = true;
    }

    public void OnBackwardGesturePerformed()
    {
        isMovingBackward = true;
    }

    public void OnGestureEnded()
    {
        isMovingForward = false;
        isMovingBackward = false;
    }

    void Move(bool forward)
    {
        Vector3 direction = Camera.main.transform.forward;
        if (!forward)
        {
            direction = -direction; // 反向移动
        }

        direction.y = 0;
        direction.Normalize();

        // 计算手势到摄像头的距离
        float distance = Vector3.Distance(gestureTransform.position, Camera.main.transform.position);

        // 根据距离调整速度
        float adjustedSpeed;
        if (distance <= minDistance)
        {
            adjustedSpeed = minSpeed; // 距离小于最小值时，速度为最小速度
        }
        else if (distance >= maxDistance)
        {
            adjustedSpeed = maxSpeed; // 距离大于最大值时，速度为最大速度
        }
        else
        {
            // 在最小和最大距离之间，进行线性插值
            float speedFactor = Mathf.InverseLerp(minDistance, maxDistance, distance);
            adjustedSpeed = Mathf.Lerp(minSpeed, maxSpeed, speedFactor);
        }

        xrOrigin.position += direction * adjustedSpeed * Time.deltaTime;
    }
}