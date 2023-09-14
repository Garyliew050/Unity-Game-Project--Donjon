using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [Header("Component")]
    public TextMeshProUGUI timerText;

    [Header("Timer Settings")]
    public float currentTime;
    public bool countDown;

    [Header("Limit Settings")]
    public bool hasLimit;
    public float timerLimit;

    [Header("Format Settings")]
    public bool hasFormat;
    public TimerFormats format;
    private Dictionary<TimerFormats, string> timeFormats = new Dictionary<TimerFormats, string>();

    private bool isGameOver = false; // New variable to track game over state

    // Start is called before the first frame update
    void Start()
    {
        timeFormats.Add(TimerFormats.Whole, "0");
        timeFormats.Add(TimerFormats.TenthDecimal, "0.0");
        timeFormats.Add(TimerFormats.HundrethsDecimal, "0.00");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver) // Check if the game is not over
        {
            currentTime = countDown ? currentTime -= Time.deltaTime : Time.time;

            if (currentTime <= 0 && countDown)
            {
                currentTime = 0;
                SetTimerText();
                EndGame(); // Call the method to end the game
            }
            else if (hasLimit && ((countDown && currentTime <= timerLimit) || (!countDown && currentTime >= timerLimit)))
            {
                currentTime = timerLimit;
                SetTimerText();
                timerText.color = Color.red;
                EndGame(); // Call the method to end the game
            }

            SetTimerText();
        }
    }

    private void SetTimerText()
    {
        timerText.text = hasFormat ? currentTime.ToString(timeFormats[format]) : currentTime.ToString();
    }

    private void EndGame()
    {
        isGameOver = true;
        // Add your game over logic here, such as showing a game over screen, stopping gameplay, etc.
        SceneManager.LoadScene("GameOver");

    }
}

public enum TimerFormats
{
    Whole,
    TenthDecimal,
    HundrethsDecimal
}
