using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GestureSpider : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform cameraTransform;
    public LayerMask raycastLayerMask;
    public float arrowDistance = 20f;
    public float arrowBufferDistance = 0.5f;
    public GameObject xrOrigin;
    public float arrowYPosition = 1.0f;
    public Transform handTransform;
    private bool useLerpForMovement = true;
    private GameObject currentArrow;
    private bool isGestureActive = false;
    private bool isRotatingGestureActive = false;
    private float gestureMaintainTimer = 0f;
    private float gestureDurationThreshold = 2f;
    private Vector3 initialHandPosition;
    public float rotationSensitivity = 1000f; 

    void Update()
    {
        if (!isGestureActive)
        {
            gestureMaintainTimer += Time.deltaTime;

            if (gestureMaintainTimer >= gestureDurationThreshold)
            {
                RemoveArrow();
            }
        }

        if (isRotatingGestureActive && currentArrow != null)
        {
            UpdateArrowRotationBasedOnHandMovement();
        }
    }

    private void InstantiateArrow()
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        RaycastHit hit;

        Vector3 spawnPosition;

        if (Physics.Raycast(ray, out hit, 100f, raycastLayerMask))
        {
            spawnPosition = hit.point - ray.direction * arrowBufferDistance;
        }
        else
        {
            spawnPosition = cameraTransform.position + cameraTransform.forward * arrowDistance;
        }

        spawnPosition.y = arrowYPosition;

        currentArrow = Instantiate(arrowPrefab, spawnPosition, Quaternion.LookRotation(new Vector3(cameraTransform.forward.x, 0, cameraTransform.forward.z)));
    }

    public void GestureActive()
    {
        if (currentArrow == null)
        {
            InstantiateArrow();
        }

        isGestureActive = true;
        gestureMaintainTimer = 0f;
    }

    public void GestureEnd()
    {
        isGestureActive = false;
        gestureMaintainTimer = 0f;
    }

    public void StartRotatingGesture()
    {
        isRotatingGestureActive = true;
        initialHandPosition = handTransform.position;
    }

    public void EndRotatingGesture()
    {
        isRotatingGestureActive = false;
        if (currentArrow != null)
        {
            if (useLerpForMovement)
            {
                StartCoroutine(SmoothMoveToArrow());
            }
            else
            {
                TeleportToArrow();
            }
        }
    }

    private void RemoveArrow()
    {
        if (currentArrow != null)
        {
            Destroy(currentArrow);
            currentArrow = null;
        }
    }

    private void TeleportToArrow()
    {
        Vector3 targetPosition = new Vector3(
            currentArrow.transform.position.x,
            xrOrigin.transform.position.y,
            currentArrow.transform.position.z);
        Quaternion targetRotation = currentArrow.transform.rotation;

        xrOrigin.transform.position = targetPosition;
        xrOrigin.transform.rotation = targetRotation;

        Debug.Log("Player teleported instantly!");
        RemoveArrow();
    }

    private IEnumerator SmoothMoveToArrow()
    {
        float duration = 1.0f;
        float elapsed = 0f;
        Vector3 startPosition = xrOrigin.transform.position;
        Quaternion startRotation = xrOrigin.transform.rotation;
        Vector3 targetPosition = new Vector3(
        currentArrow.transform.position.x,
        xrOrigin.transform.position.y,
        currentArrow.transform.position.z);
        Quaternion targetRotation = currentArrow.transform.rotation;

        while (elapsed < duration)
        {

            xrOrigin.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsed / duration);
            xrOrigin.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsed / duration);

            elapsed += Time.deltaTime;
            yield return null; 
        }

        xrOrigin.transform.position = targetPosition;
        xrOrigin.transform.rotation = targetRotation;

        RemoveArrow();
    }

    private void UpdateArrowRotationBasedOnHandMovement()
    {
        if (currentArrow == null) return;

        float handMovementDelta = handTransform.position.x - initialHandPosition.x;

        currentArrow.transform.Rotate(Vector3.up, handMovementDelta * rotationSensitivity);

        initialHandPosition = handTransform.position;
    }

    public void SetLerpFalse()
    {
        useLerpForMovement = false;
    }

    public void SetLerpTrue()
    {
        useLerpForMovement = true;
    }
}

