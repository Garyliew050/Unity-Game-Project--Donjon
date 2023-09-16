using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDecreaseObject : MonoBehaviour
{
    public float timeDecreaseAmount = 5f;

    private bool hasBeenTouched = false;


    private void OnTriggerEnter(Collider other)
    {
        if (!hasBeenTouched && other.CompareTag("Player"))
        {
            Timer timer = FindObjectOfType<Timer>();

            if (timer != null)
            {
                timer.currentTime -= timeDecreaseAmount;

                if (timer.currentTime < 0f)
                {
                    timer.currentTime = 0f;
                }

                // Call the UpdateTimerText method to update the timer's text
                timer.SetTimerText();

                hasBeenTouched = true;
                gameObject.SetActive(false);

            }
        }
    }
}
