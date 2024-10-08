using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonPuzzleManager : MonoBehaviour
{
    public GameObject[] buttons;
    public Transform playerTransform;
    public Transform targetPosition;

    private int buttonsPressed = 0;

    // A HashSet to keep track of pressed buttons
    private HashSet<GameObject> pressedButtons = new HashSet<GameObject>();

    // Call this method when a button is pressed
    public void OnButtonPressed(GameObject button)
    {
        // Check if the button has already been pressed
        if (!pressedButtons.Contains(button))
        {
            pressedButtons.Add(button);
            buttonsPressed++;

            // Check if all buttons have been pressed
            if (buttonsPressed == buttons.Length)
            {
                TeleportPlayer();
            }
        }
    }

    // Method to teleport the player
    private void TeleportPlayer()
    {
        Vector3 newPosition = new Vector3(targetPosition.position.x, playerTransform.position.y, targetPosition.position.z);
        playerTransform.position = newPosition;
        Debug.Log("Player has been teleported to the target position!");
    }
}
