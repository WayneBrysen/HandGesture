using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonPuzzleManager : MonoBehaviour
{
    public GameObject[] buttons;
    public Transform playerTransform;
    public Transform targetPosition;

    private int buttonsPressed = 0;

    public void OnButtonPressed()
    {
        buttonsPressed++;

        if (buttonsPressed == buttons.Length)
        {
            TeleportPlayer();
        }
    }

    private void TeleportPlayer()
    {
        Vector3 newPosition = new Vector3(targetPosition.position.x, playerTransform.position.y, targetPosition.position.z);
        playerTransform.position = newPosition;
        Debug.Log("Player has been teleported to the target position!");
    }
}
