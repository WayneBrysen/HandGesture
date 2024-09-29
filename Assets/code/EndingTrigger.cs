using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingTrigger : MonoBehaviour
{
    public Transform targetPosition;
    public Transform playerTransform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 newPosition = new Vector3(targetPosition.position.x,
                                              playerTransform.position.y,
                                              targetPosition.position.z);
            playerTransform.position = newPosition;
            Debug.Log("Player has been teleported!");
        }
    }
}
