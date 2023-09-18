using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyCounterUI : MonoBehaviour
{
    public Text keyCountText; // Reference to the UI Text component for displaying the key count
    public AN_HeroInteractive hero; // Reference to the player's interactive script

    private void Start()
    {
        UpdateKeyCounterUI();
    }

    public void UpdateKeyCounterUI()
    {
        keyCountText.text = "Red Keys: " + hero.RedKeyCount.ToString();
        // You can customize the text format as needed
    }
}

