using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourthPuzzleManager : MonoBehaviour
{
    // Variables for books
    public Transform playerTransform;
    public GameObject targetPosition;
    public BookHolder[] placeHolders;

    // Variables for buttons
    public GameObject[] buttons;
    private int buttonsPressed = 0;

    // A HashSet to keep track of pressed buttons
    private HashSet<GameObject> pressedButtons = new HashSet<GameObject>();

    // Check if all books are collected
    private bool AllBooksCollected()
    {
        foreach (BookHolder bookHolder in placeHolders)
        {
            if (!bookHolder.IsFilled())
            {
                return false;
            }
        }
        return true;
    }

    // Check if all buttons are pressed
    private bool AllButtonsPressed()
    {
        return buttonsPressed == buttons.Length;
    }

    // Call this method when a button is pressed
    public void OnButtonPressed(GameObject button)
    {
        // Check if this button has already been pressed
        if (!pressedButtons.Contains(button))
        {
            pressedButtons.Add(button);
            buttonsPressed++;
            CheckPuzzleCompletion();
        }
    }

    // Check both conditions
    public void CheckPuzzleCompletion()
    {
        if (AllBooksCollected() && AllButtonsPressed())
        {
            TeleportPlayer();
        }
    }

    // Method to teleport the player
    private void TeleportPlayer()
    {
        Vector3 newPosition = new Vector3(targetPosition.transform.position.x,
                                          playerTransform.position.y,
                                          targetPosition.transform.position.z);
        playerTransform.position = newPosition;
        Debug.Log("Player has been teleported to the target position!");
    }
}