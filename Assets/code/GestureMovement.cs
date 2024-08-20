using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureMovement : MonoBehaviour
{
    public Transform xrOrigin; // ָ��XR Origin��XR Rig
    public float speed = 1.0f;
    private bool isMoving = false;

    void Update()
    {
        // ������ƶ���������ƶ�XR Origin
        if (isMoving)
        {
            MoveForward();
        }
    }

    // �����Ʊ�ʶ��ʱ�����������
    public void OnGesturePerformed()
    {
        isMoving = true; // ��ʼ�ƶ�
    }

    // �����ƽ���ʱ�����������
    public void OnGestureEnded()
    {
        isMoving = false; // ֹͣ�ƶ�
    }

    void MoveForward()
    {
        // ��ȡ����ͷ��ǰ����
        Vector3 forwardDirection = Camera.main.transform.forward;
        // ����YΪ0��ȷ��ֻ��ˮƽ���ƶ�
        forwardDirection.y = 0;
        forwardDirection.Normalize(); // ��һ����ȷ�����������Ĵ�СΪ1

        // �ƶ�XR Origin��λ��
        xrOrigin.position += forwardDirection * speed * Time.deltaTime;
    }
}
