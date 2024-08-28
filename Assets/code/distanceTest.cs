using UnityEngine;
using TMPro;

public class DistanceDisplay : MonoBehaviour
{
    public Transform gestureTransform; // Transform of the gesture object
    public Transform targetObject; // Transform of the target GameObject
    public TMP_Text distanceText; // TextMesh Pro component to display the distance
    public float distanceMultiplier = 100.0f; // Multiplier to scale the distance for display purposes

    void Update()
    {
        // Calculate the distance between the gesture object and the target object
        float distance = Vector3.Distance(gestureTransform.position, targetObject.position);

        // Scale the distance for a more noticeable display
        float displayDistance = distance * distanceMultiplier;

        // Update the TextMesh Pro component to show the distance value
        distanceText.text = "Distance to Target: " + displayDistance.ToString("F2") + " cm";
    }
}