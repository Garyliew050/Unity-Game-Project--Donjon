using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeIncreaseObject : MonoBehaviour
{
    public float timeIncreaseAmount = 5f;
    private bool hasBeenTouched = false;
    public TextMeshProUGUI TimeIncrease; // Reference to the TextMeshProUGUI component in the Inspector
    public float timeDelay = 2f; // Delay before hiding the TimeIncrease text

    private void Start()
    {
        // Clear the TimeIncrease text at the start to ensure it's empty
        TimeIncrease.text = "";
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player has touched the object and if the tag is "Player"
        if (!hasBeenTouched && other.CompareTag("Player"))
        {
            // Find the Timer script in the scene
            Timer timer = FindObjectOfType<Timer>();

            if (timer != null)
            {
                // Increase the timer's current time
                timer.currentTime += timeIncreaseAmount;

                // Update the UI to display the time increase
                UpdateIncreaseTimeUI();

                // Call the method to update the timer's text
                timer.SetTimerText();

                // Mark the object as touched to prevent further interactions
                hasBeenTouched = true;

                // Deactivate the object to hide it
                gameObject.SetActive(false);
            }
        }
    }

    void UpdateIncreaseTimeUI()
    {
        // Check if the TimeIncrease field is assigned
        if (TimeIncrease != null)
        {
            // Update the text to show the time increase
            TimeIncrease.text = "+ " + timeIncreaseAmount + " sec";

            // Start a coroutine to hide the text after a delay
            StartCoroutine(HideIncreaseTimeText());
        }
        else
        {
            // Log an error if the TimeIncrease field is not assigned
            Debug.LogError("TimeIncrease field is not assigned in the Inspector.");
        }
    }

    private IEnumerator HideIncreaseTimeText()
    {
        // Wait for a specified duration
        yield return new WaitForSeconds(timeDelay);

        // Clear the text to make it disappear
        TimeIncrease.text = "";
    }
}
