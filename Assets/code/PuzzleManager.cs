using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public Transform playerTransform;
    public GameObject targetPosition;
    public BookHolder[] placeHolders;

    public void CheckAllBooksCollected()
    {
        bool allCollected = true;

        foreach (BookHolder bookHolder in placeHolders)
        {
            if (!bookHolder.IsFilled())
            {
                allCollected = false;
                break;
            }
        }

        if (allCollected)
        {
            TeleportPlayer();
        }
    }

    private void TeleportPlayer()
    {
        Vector3 newPosition = new Vector3(targetPosition.transform.position.x,
                                          playerTransform.position.y,
                                          targetPosition.transform.position.z);
        playerTransform.position = newPosition;
        Debug.Log("Player has been teleported to target position!");
    }
}
