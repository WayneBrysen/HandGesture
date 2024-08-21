using UnityEngine;
using TMPro;

public class DistanceDisplay : MonoBehaviour
{
    public Transform gestureTransform; // 手势的Transform
    public Transform targetObject; // 目标GameObject的Transform
    public TMP_Text distanceText; // 用于显示距离的TextMesh Pro组件
    public float distanceMultiplier = 100.0f; // 用于放大距离的倍数

    void Update()
    {
        // 计算手势到目标对象的距离
        float distance = Vector3.Distance(gestureTransform.position, targetObject.position);

        // 放大距离显示，以便更明显
        float displayDistance = distance * distanceMultiplier;

        // 更新TextMesh Pro组件显示距离数据
        distanceText.text = "Distance to Target: " + displayDistance.ToString("F2") + " cm";
    }
}