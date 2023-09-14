using UnityEngine;
using TMPro;

public class SquarePadController : MonoBehaviour
{
    public Color greenColor = Color.green;
    public Color redColor = Color.red;
    private Color originalColor; // Store the original color to reset to it.

    public int sequenceIndex; // Set this in the Inspector for each square pad.

    private int currentSequenceIndex = 0;
    private int[] correctSequence = { 0, 1, 2 }; // Adjust this array for your correct sequence.
    private Renderer renderer;

    public GameObject[] textCanvasObjects; // Attach the three canvas objects in the Inspector.
    private TextMeshProUGUI[] textMeshes; // Store the TextMeshPro components of the canvas objects.

    private void Start()
    {
        renderer = GetComponent<Renderer>();
        originalColor = renderer.material.color; // Store the initial color.

        textMeshes = new TextMeshProUGUI[textCanvasObjects.Length];
        for (int i = 0; i < textCanvasObjects.Length; i++)
        {
            textMeshes[i] = textCanvasObjects[i].GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Make sure your player has the "Player" tag.
        {
            if (sequenceIndex == correctSequence[currentSequenceIndex])
            {
                // If the player steps on the pad in the correct sequence, turn it green.
                renderer.material.color = greenColor;
                currentSequenceIndex++;

                if (currentSequenceIndex == correctSequence.Length)
                {
                    // All pads are green, show the text on the canvas objects.
                    foreach (var textMesh in textMeshes)
                    {
                        textMesh.gameObject.SetActive(true);
                    }
                }
            }
            else
            {
                // If the player steps on the wrong pad, turn it red and reset the sequence.
                renderer.material.color = redColor;
                currentSequenceIndex = 0;

                // Reset all canvas objects.
                foreach (var textMesh in textMeshes)
                {
                    textMesh.gameObject.SetActive(false);
                }
            }
        }
    }

    // Method to reset the sequence, which can be called when needed.
    public void ResetSequence()
    {
        currentSequenceIndex = 0;
        renderer.material.color = originalColor; // Restore the original color when resetting.

        // Reset all canvas objects.
        foreach (var textMesh in textMeshes)
        {
            textMesh.gameObject.SetActive(false);
        }
    }
}
