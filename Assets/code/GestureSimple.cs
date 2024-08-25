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
            direction = -direction; // �����ƶ�
        }

        direction.y = 0;
        direction.Normalize();

        // �������Ƶ�����ͷ�ľ���
        float distance = Vector3.Distance(gestureTransform.position, Camera.main.transform.position);

        // ���ݾ�������ٶ�
        float adjustedSpeed;
        if (distance <= minDistance)
        {
            adjustedSpeed = minSpeed; // ����С����Сֵʱ���ٶ�Ϊ��С�ٶ�
        }
        else if (distance >= maxDistance)
        {
            adjustedSpeed = maxSpeed; // ����������ֵʱ���ٶ�Ϊ����ٶ�
        }
        else
        {
            // ����С��������֮�䣬�������Բ�ֵ
            float speedFactor = Mathf.InverseLerp(minDistance, maxDistance, distance);
            adjustedSpeed = Mathf.Lerp(minSpeed, maxSpeed, speedFactor);
        }

        xrOrigin.position += direction * adjustedSpeed * Time.deltaTime;
    }

    void Rotate(bool left)
    {
        // �������Ƶ�����ͷ�ľ���
        float distance = Vector3.Distance(gestureTransform.position, Camera.main.transform.position);

        // ���ݾ��������ת�ٶ�
        float adjustedRotationSpeed;
        if (distance <= minDistance)
        {
            adjustedRotationSpeed = minRotationSpeed; // ����С����Сֵʱ����ת�ٶ�Ϊ��С�ٶ�
        }
        else if (distance >= maxDistance)
        {
            adjustedRotationSpeed = maxRotationSpeed; // ����������ֵʱ����ת�ٶ�Ϊ����ٶ�
        }
        else
        {
            // ����С��������֮�䣬�������Բ�ֵ
            float speedFactor = Mathf.InverseLerp(minDistance, maxDistance, distance);
            adjustedRotationSpeed = Mathf.Lerp(minRotationSpeed, maxRotationSpeed, speedFactor);
        }

        // ������ת����
        float rotationDirection = left ? -1 : 1;

        // �Ե�������ٶ�ִ����ת
        xrOrigin.Rotate(Vector3.up, rotationDirection * adjustedRotationSpeed * Time.deltaTime);
    }
}