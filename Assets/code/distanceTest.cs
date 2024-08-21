using UnityEngine;
using TMPro;

public class DistanceDisplay : MonoBehaviour
{
    public Transform gestureTransform; // ���Ƶ�Transform
    public Transform targetObject; // Ŀ��GameObject��Transform
    public TMP_Text distanceText; // ������ʾ�����TextMesh Pro���
    public float distanceMultiplier = 100.0f; // ���ڷŴ����ı���

    void Update()
    {
        // �������Ƶ�Ŀ�����ľ���
        float distance = Vector3.Distance(gestureTransform.position, targetObject.position);

        // �Ŵ������ʾ���Ա������
        float displayDistance = distance * distanceMultiplier;

        // ����TextMesh Pro�����ʾ��������
        distanceText.text = "Distance to Target: " + displayDistance.ToString("F2") + " cm";
    }
}