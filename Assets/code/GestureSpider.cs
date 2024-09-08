using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureSpider : MonoBehaviour
{
    public GameObject markerPrefab;
    public Transform handTransform;
    public LayerMask raycastLayerMask;
    private bool isGestureActive = false;
    private GameObject currentMarker;

    void Update()
    {
        if (isGestureActive)
        {
            UpdateMarkerPosition();
        }
        else
        {
            RemoveMarker();  
        }
    }

    private void UpdateMarkerPosition()
    {
        Ray ray = new Ray(handTransform.position, handTransform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, raycastLayerMask))  
        {
            if (currentMarker == null)
            {
                currentMarker = Instantiate(markerPrefab, hit.point, Quaternion.identity);
            }
            else 
            {
                currentMarker.transform.position = hit.point;
            }
        }
    }

    public void GestureActive()
    {
        isGestureActive = true;
    }

    public void GestureEnd()
    {
        isGestureActive = false;

    }

    private void RemoveMarker()
    {
        if (currentMarker != null)
        {
            Destroy(currentMarker);
        }
    }


}
