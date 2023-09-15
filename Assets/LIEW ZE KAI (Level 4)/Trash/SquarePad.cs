using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SquarePad : MonoBehaviour
{
    public TextMeshPro[] textMeshPros; // Reference to your TextMeshPro objects
    private List<GameObject> padsInSequence = new List<GameObject>();
    private int currentPadIndex = 0;

    private void OnTriggerEnter(Collider other)
    {
        GameObject pad = other.gameObject;

        // Check if the pad is the next one in the sequence.
        if (pad == padsInSequence[currentPadIndex])
        {
            // Change the pad color to green.
            Renderer padRenderer = pad.GetComponent<Renderer>();
            padRenderer.material.color = Color.green;

            currentPadIndex++;

            // Check if the player has completed the sequence.
            if (currentPadIndex == padsInSequence.Count)
            {
                // All pads are green, so show the TextMeshPro objects.
                ShowTextMeshPros();
            }
        }
        else
        {
            // Incorrect pad, reset the puzzle.
            ResetPuzzle();
        }
    }

    private void ShowTextMeshPros()
    {
        // Show the TextMeshPro objects when the correct sequence is completed.
        foreach (TextMeshPro textMesh in textMeshPros)
        {
            textMesh.gameObject.SetActive(true);
        }
    }

    private void ResetPuzzle()
    {
        // Reset the puzzle when the player steps on the wrong pad.
        currentPadIndex = 0;
        padsInSequence.Clear();

        // Set all pads to their initial state (e.g., red).
        Renderer[] padRenderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer padRenderer in padRenderers)
        {
            padRenderer.material.color = Color.red;
            padsInSequence.Add(padRenderer.gameObject);
        }

        // Hide all TextMeshPro objects.
        foreach (TextMeshPro textMesh in textMeshPros)
        {
            textMesh.gameObject.SetActive(false);
        }
    }
}
