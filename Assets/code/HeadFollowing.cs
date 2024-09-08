using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadFollowing : MonoBehaviour
{
    public Transform playerTransform;  // ��ң�XR Origin����Transform
    public float rotationSpeed = 1.0f;  // ��ת�ٶ�
    public float followRange = 5.0f;  // ͷ��������ҵķ�Χ����λ���ף�

    void Update()
    {
        // �����ͷ����ҵķ�������
        Vector3 directionToPlayer = playerTransform.position - transform.position;

        // ����ͷ�����֮��ľ���
        float distanceToPlayer = directionToPlayer.magnitude;

        // �������Ƿ��ڷ�Χ��
        if (distanceToPlayer <= followRange)
        {
            // ����ͷ��ˮƽ��ת�������ϻ�������ת
            directionToPlayer.y = 0;

            // ������������������������Ž�����ת
            if (directionToPlayer != Vector3.zero)
            {
                // ����ͷ��Ӧ����ת����Ŀ�곯��
                Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);

                // ʹ�ò�ֵ��������ƽ����ת
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
}
