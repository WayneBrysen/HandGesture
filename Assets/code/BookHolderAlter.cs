using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookHolderAlter : MonoBehaviour
{
    public string requiredBookTag;
    public Color defaultColor = Color.blue;
    public Color filledColor = Color.green;
    private Renderer placeHolderRenderer;
    private bool isFilled = false;
    public GameObject puzzleManager;

    void Start()
    {
        placeHolderRenderer = GetComponent<Renderer>();
        placeHolderRenderer.material.color = defaultColor;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(requiredBookTag) && !isFilled)
        {
            placeHolderRenderer.material.color = filledColor;
            Destroy(other.gameObject);
            isFilled = true;
            puzzleManager.GetComponent<PuzzleManager>().CheckAllBooksCollected();
        }
    }

    public bool IsFilled()
    {
        return isFilled;
    }
}
