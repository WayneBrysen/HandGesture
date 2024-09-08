using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadFollowing : MonoBehaviour
{
    public Transform playerTransform;  // 玩家（XR Origin）的Transform
    public float rotationSpeed = 1.0f;  // 旋转速度
    public float followRange = 5.0f;  // 头部跟随玩家的范围（单位：米）

    void Update()
    {
        // 计算从头到玩家的方向向量
        Vector3 directionToPlayer = playerTransform.position - transform.position;

        // 计算头和玩家之间的距离
        float distanceToPlayer = directionToPlayer.magnitude;

        // 检查玩家是否在范围内
        if (distanceToPlayer <= followRange)
        {
            // 保持头的水平旋转，不向上或向下旋转
            directionToPlayer.y = 0;

            // 如果方向向量不是零向量，才进行旋转
            if (directionToPlayer != Vector3.zero)
            {
                // 计算头部应该旋转到的目标朝向
                Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);

                // 使用插值函数进行平滑旋转
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
}
