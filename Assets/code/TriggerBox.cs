using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBox : MonoBehaviour
{
    public Light pointLight;  // ָ�򳡾��е� Point Light
    public AudioSource audioSource;  // ָ�򳡾��е� Audio Source
    public Color triggeredLightColor = Color.red;  // ������ʱ�ı�Ĺ���ɫ
    private Color originalLightColor;  // ���� Point Light ��ԭʼ��ɫ

    void Start()
    {
        // ���� Point Light ��ԭʼ��ɫ
        if (pointLight != null)
        {
            originalLightColor = pointLight.color;
        }
    }

    // ����ҽ��봥������ʱ
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // ȷ��ֻ����Ҵ���
        {
            // �ı� Point Light ����ɫ
            if (pointLight != null)
            {
                pointLight.color = triggeredLightColor;
            }

            // ѭ��������Ƶ
            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.loop = true;  // ������Ƶѭ��
                audioSource.Play();  // ������Ƶ
            }
        }
    }

    // ������뿪��������ʱ
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))  // ȷ��ֻ����Ҵ���
        {
            // �ָ� Point Light ��ԭʼ��ɫ
            if (pointLight != null)
            {
                pointLight.color = originalLightColor;
            }

            // ֹͣ��Ƶ����
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();  // ֹͣ��Ƶ
                audioSource.loop = false;  // ȡ����Ƶѭ��
            }
        }
    }
}
