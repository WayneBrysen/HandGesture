using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureMovement : MonoBehaviour
{
    public Transform xrOrigin;
    public Transform gestureTransform; // L_Wrist��Transform�����ڸ�������
    public float baseSpeed = 1.0f;
    public float maxSpeed = 5.0f; // ����ٶ�
    public float minSpeed = 0.5f; // ��С�ٶ�
    public float maxDistance = 0.4f; // ���ƾ������ֵ��40���ף�
    public float minDistance = 0.3f; // ���ƾ�����Сֵ��25���ף�

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
}