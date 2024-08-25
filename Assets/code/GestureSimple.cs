using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureSimple : MonoBehaviour
{
    public Transform xrOrigin;
    public Transform gestureTransform;
    public float baseSpeed = 1.0f;
    public float maxSpeed = 5.0f;
    public float minSpeed = 0.5f;
    public float maxDistance = 0.5f;
    public float minDistance = 0.3f;

    public float maxRotationSpeed = 50.0f;
    public float minRotationSpeed = 10.0f;

    private bool isMovingForward = false;
    private bool isMovingBackward = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;

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

        if (isRotatingLeft)
        {
            Rotate(true);
        }
        else if (isRotatingRight)
        {
            Rotate(false);
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

    public void OnLeftRotationGesturePerformed()
    {
        isRotatingLeft = true;
    }

    public void OnRightRotationGesturePerformed()
    {
        isRotatingRight = true;
    }

    public void OnGestureEnded()
    {
        isMovingForward = false;
        isMovingBackward = false;
        isRotatingLeft = false;
        isRotatingRight = false;
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

    void Rotate(bool left)
    {
        // 计算手势到摄像头的距离
        float distance = Vector3.Distance(gestureTransform.position, Camera.main.transform.position);

        // 根据距离调整旋转速度
        float adjustedRotationSpeed;
        if (distance <= minDistance)
        {
            adjustedRotationSpeed = minRotationSpeed; // 距离小于最小值时，旋转速度为最小速度
        }
        else if (distance >= maxDistance)
        {
            adjustedRotationSpeed = maxRotationSpeed; // 距离大于最大值时，旋转速度为最大速度
        }
        else
        {
            // 在最小和最大距离之间，进行线性插值
            float speedFactor = Mathf.InverseLerp(minDistance, maxDistance, distance);
            adjustedRotationSpeed = Mathf.Lerp(minRotationSpeed, maxRotationSpeed, speedFactor);
        }

        // 计算旋转方向
        float rotationDirection = left ? -1 : 1;

        // 以调整后的速度执行旋转
        xrOrigin.Rotate(Vector3.up, rotationDirection * adjustedRotationSpeed * Time.deltaTime);
    }
}