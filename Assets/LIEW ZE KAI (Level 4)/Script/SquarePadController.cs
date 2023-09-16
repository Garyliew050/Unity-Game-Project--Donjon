using UnityEngine;
using TMPro;

public class SquarePadController : MonoBehaviour
{
    private Color greenColor = Color.green;
    private Color redColor = Color.red;
    private Color originalColor; // Store the original color to reset to it.
    private bool isGreen = false; // Track if the pad is green.
    private bool allPadsGreen = false; // Track if all pads are green.

    public GameObject[] textCanvasObjects; // Attach the three canvas objects in the Inspector.
    private TextMeshProUGUI[] textMeshes; // Store the TextMeshPro components of the canvas objects.

    private void Start()
    {
        originalColor = GetComponent<Renderer>().material.color; // Store the initial color.

        textMeshes = new TextMeshProUGUI[textCanvasObjects.Length];
        for (int i = 0; i < textCanvasObjects.Length; i++)
        {
            textMeshes[i] = textCanvasObjects[i].GetComponentInChildren<TextMeshProUGUI>();
            textMeshes[i].gameObject.SetActive(false); // Hide text initially.
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Make sure your player has the "Player" tag.
        {
            // Toggle between green and red.
            if (isGreen)
            {
                // If the pad is green, turn it red.
                GetComponent<Renderer>().material.color = redColor;
            }
            else
            {
                // If the pad is red, turn it green.
                GetComponent<Renderer>().material.color = greenColor;
            }

            isGreen = !isGreen; // Toggle the state.

            // Check if all pads are green.
            allPadsGreen = CheckAllPadsGreen();

            if (allPadsGreen)
            {
                // All pads are green, show the text on the canvas objects.
                foreach (var textMesh in textMeshes)
                {
                    textMesh.gameObject.SetActive(true);
                }
            }
            else
            {
                // At least one pad is not green, hide the text on the canvas objects.
                foreach (var textMesh in textMeshes)
                {
                    textMesh.gameObject.SetActive(false);
                }
            }
        }
    }

    // Method to reset the pads and hide the text, which can be called when needed.
    public void ResetPads()
    {
        foreach (var pad in FindObjectsOfType<SquarePadController>())
        {
            pad.GetComponent<Renderer>().material.color = originalColor; // Restore the original color when resetting.
            pad.isGreen = false;
        }

        // Hide all canvas objects.
        foreach (var textMesh in textMeshes)
        {
            textMesh.gameObject.SetActive(false);
        }
    }

    private bool CheckAllPadsGreen()
    {
        foreach (var pad in FindObjectsOfType<SquarePadController>())
        {
            if (!pad.isGreen)
            {
                return false;
            }
        }
        return true;
    }
}
