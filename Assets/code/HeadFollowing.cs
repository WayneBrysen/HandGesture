using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadFollowing : MonoBehaviour
{
    public Transform playerTransform;
    public float rotationSpeed = 1.0f;
    public float followRange = 5.0f;

    void Update()
    {
        Vector3 directionToPlayer = playerTransform.position - transform.position;

        float distanceToPlayer = directionToPlayer.magnitude;

        if (distanceToPlayer <= followRange)
        {
            directionToPlayer.y = 0;

            if (directionToPlayer != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);

                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
}
