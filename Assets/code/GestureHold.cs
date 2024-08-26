using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureHold : MonoBehaviour
{
    public Transform xrOrigin; // ����ӽǻ��ɫ�ĸ�����
    public Transform handTransform; // �ֲ����������Transform
    public float rotationSensitivity = 100.0f; // ��ת���ж�

    private Vector3 initialHandPosition; // �ֲ��ĳ�ʼλ��
    private bool isGestureActive = false; // �Ƿ񱣳�����

    void Update()
    {
        if (isGestureActive)
        {
            // �����ֲ�����ڳ�ʼλ�õ�ˮƽλ��
            float handDeltaX = handTransform.position.x - initialHandPosition.x;

            // Debug: ����ֲ�ˮƽλ�Ƶı仯
            Debug.Log("Hand Delta X: " + handDeltaX);

            // ���ֲ�λ��ת��Ϊ��ת�Ƕȣ��������ƶ�������������ת����֮��Ȼ
            float rotationAngle = -handDeltaX * rotationSensitivity;

            // Debug: �������õ�����ת�Ƕ�
            Debug.Log("Rotation Angle: " + rotationAngle);

            // ��Ӱ��Y����ת
            xrOrigin.Rotate(Vector3.up, rotationAngle * Time.deltaTime);

            // Debug: �����ת���XR Origin�Ƕ�
            Debug.Log("XR Origin Rotation after Update: " + xrOrigin.rotation.eulerAngles);

            // ���³�ʼ�ֲ�λ��
            initialHandPosition = handTransform.position;
        }
    }

    // ���ƿ�ʼʱ���ô˷���
    public void OnGestureStart()
    {
        isGestureActive = true;
        initialHandPosition = handTransform.position; // ��¼��ʼ�ֲ�λ��

        // Debug: ������ƿ�ʼ����Ϣ�ͳ�ʼ�ֲ�λ��
        Debug.Log("Gesture Started. Initial Hand Position: " + initialHandPosition);
    }

    // ���ƽ���ʱ���ô˷���
    public void OnGestureEnd()
    {
        isGestureActive = false;

        // Debug: ������ƽ�������Ϣ
        Debug.Log("Gesture Ended.");
    }
}