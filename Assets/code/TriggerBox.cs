using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBox : MonoBehaviour
{
    public Light pointLight;  // 指向场景中的 Point Light
    public AudioSource audioSource;  // 指向场景中的 Audio Source
    public Color triggeredLightColor = Color.red;  // 当触发时改变的光颜色
    private Color originalLightColor;  // 保存 Point Light 的原始颜色

    void Start()
    {
        // 保存 Point Light 的原始颜色
        if (pointLight != null)
        {
            originalLightColor = pointLight.color;
        }
    }

    // 当玩家进入触发区域时
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // 确保只有玩家触发
        {
            // 改变 Point Light 的颜色
            if (pointLight != null)
            {
                pointLight.color = triggeredLightColor;
            }

            // 循环播放音频
            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.loop = true;  // 设置音频循环
                audioSource.Play();  // 播放音频
            }
        }
    }

    // 当玩家离开触发区域时
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))  // 确保只有玩家触发
        {
            // 恢复 Point Light 的原始颜色
            if (pointLight != null)
            {
                pointLight.color = originalLightColor;
            }

            // 停止音频播放
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();  // 停止音频
                audioSource.loop = false;  // 取消音频循环
            }
        }
    }
}
