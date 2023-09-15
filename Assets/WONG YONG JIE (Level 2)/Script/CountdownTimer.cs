using System.Collections;
using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    public float countdownDuration = 5f; // Duration of the countdown in seconds

    private float currentTime;
    private bool isCountingDown = false;

    private void Start()
    {
        // Initialize the countdown text to an empty string
        countdownText.text = "";
    }

    // Call this method to start the countdown
    public void StartCountdown()
    {
        if (!isCountingDown)
        {
            StartCoroutine(Countdown());
        }
    }

    private IEnumerator Countdown()
    {
        isCountingDown = true;
        currentTime = countdownDuration;

        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1f); // Update once per second
            currentTime -= 1f;
            UpdateCountdownText();
        }

        // Countdown finished
        currentTime = 0f;
        UpdateCountdownText();
        isCountingDown = false;
    }

    private void UpdateCountdownText()
    {
        if (currentTime > 0)
        {
            countdownText.text = currentTime.ToString("F0"); // Display the time as a whole number
        }
        else
        {
            countdownText.text = ""; // Set the text to an empty string
        }
    }
}
