using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureMovement : MonoBehaviour
{
    public Transform xrOrigin; // 指向XR Origin或XR Rig
    public float speed = 1.0f;
    private bool isMoving = false;

    void Update()
    {
        // 如果在移动，则持续移动XR Origin
        if (isMoving)
        {
            MoveForward();
        }
    }

    // 当手势被识别时调用这个方法
    public void OnGesturePerformed()
    {
        isMoving = true; // 开始移动
    }

    // 当手势结束时调用这个方法
    public void OnGestureEnded()
    {
        isMoving = false; // 停止移动
    }

    void MoveForward()
    {
        // 获取摄像头的前向方向
        Vector3 forwardDirection = Camera.main.transform.forward;
        // 保持Y为0，确保只在水平面移动
        forwardDirection.y = 0;
        forwardDirection.Normalize(); // 归一化以确保方向向量的大小为1

        // 移动XR Origin的位置
        xrOrigin.position += forwardDirection * speed * Time.deltaTime;
    }
}
