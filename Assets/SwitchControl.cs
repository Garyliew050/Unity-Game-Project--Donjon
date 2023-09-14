using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchControl : MonoBehaviour
{
    public float interactionDistance = 2f; // Adjust this to set the interaction distance.
    public KeyCode interactKey = KeyCode.E; // Define the key to interact with the switch.
    private Animator switchAnimator;
    private bool isOpen = false;
    private bool hasBeenActivated = false;

    private void Start()
    {
        switchAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Check if the player is near the switch and presses the interact key.
        if (!hasBeenActivated && Input.GetKeyDown(interactKey) && IsPlayerNear())
        {
            // Set the "Open" parameter and play the animation.
            isOpen = true;
            switchAnimator.SetBool("Open", isOpen);
            hasBeenActivated = true;
        }
    }

    private bool IsPlayerNear()
    {
        // Replace "Player" with the actual tag or layer of your player GameObject.
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer <= interactionDistance;
        }

        return false;
    }
}
