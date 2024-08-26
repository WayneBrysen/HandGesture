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
    public Transform xrOrigin; // 玩家视角或角色的根对象
    public Transform handTransform; // 手部或控制器的Transform
    public float rotationSensitivity = 100.0f; // 旋转敏感度

    private Vector3 initialHandPosition; // 手部的初始位置
    private bool isGestureActive = false; // 是否保持手势

    void Update()
    {
        if (isGestureActive)
        {
            // 计算手部相对于初始位置的水平位移
            float handDeltaX = handTransform.position.x - initialHandPosition.x;

            // Debug: 输出手部水平位移的变化
            Debug.Log("Hand Delta X: " + handDeltaX);

            // 将手部位移转换为旋转角度，手往右移动则身体向左旋转，反之亦然
            float rotationAngle = -handDeltaX * rotationSensitivity;

            // Debug: 输出计算得到的旋转角度
            Debug.Log("Rotation Angle: " + rotationAngle);

            // 仅影响Y轴旋转
            xrOrigin.Rotate(Vector3.up, rotationAngle * Time.deltaTime);

            // Debug: 输出旋转后的XR Origin角度
            Debug.Log("XR Origin Rotation after Update: " + xrOrigin.rotation.eulerAngles);

            // 更新初始手部位置
            initialHandPosition = handTransform.position;
        }
    }

    // 手势开始时调用此方法
    public void OnGestureStart()
    {
        isGestureActive = true;
        initialHandPosition = handTransform.position; // 记录初始手部位置

        // Debug: 输出手势开始的信息和初始手部位置
        Debug.Log("Gesture Started. Initial Hand Position: " + initialHandPosition);
    }

    // 手势结束时调用此方法
    public void OnGestureEnd()
    {
        isGestureActive = false;

        // Debug: 输出手势结束的信息
        Debug.Log("Gesture Ended.");
    }
}