using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTrap : MonoBehaviour
{
    public float trapDuration = 1f; // Duration of the trap effect in seconds
    public float trapActivationDelay = 5f; // Delay before activating the trap effect
    private bool isTrapped = false;

    // Reference to the CountdownTimer script
    private CountdownTimer countdownTimer;

    private void Start()
    {
        // Find the CountdownTimer script by name (assuming it's in the scene)
        countdownTimer = GameObject.Find("CountdownTimer").GetComponent<CountdownTimer>();

        // Check if the CountdownTimer script was found
        if (countdownTimer == null)
        {
            Debug.LogError("CountdownTimer script not found. Make sure it's attached to an appropriate GameObject.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTrapped) // Check if it's a collision with the player and not already trapped
        {
            PlayerController player = other.GetComponent<PlayerController>();
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            PlayerMovement2 playerMovement2 = other.GetComponent<PlayerMovement2>();

            if (player != null)
            {
                // Trigger the "IsTrap" animation in the player's Animator component.
                player.GetComponent<Animator>().SetBool("IsTrap", true);

                // Set isTrapped to true to prevent repeated trapping
                isTrapped = true;

                // Disable player movement
                if (playerMovement != null)
                {
                    playerMovement.enabled = false;
                }

                if (playerMovement2 != null)
                {
                    playerMovement2.enabled = false;
                }

                // Start a coroutine to activate the trap effect after a delay
                StartCoroutine(ActivateTrapEffect(player, playerMovement, playerMovement2));
            }
        }
    }

    private IEnumerator ActivateTrapEffect(PlayerController player, PlayerMovement playerMovement, PlayerMovement2 playerMovement2)
    {
        countdownTimer.StartCountdown();
        // Wait for the specified delay before activating the trap effect
        yield return new WaitForSeconds(trapActivationDelay);

        // Trigger "IsTrapping" to true
        player.GetComponent<Animator>().SetBool("IsTrapping", true);

        // Start the countdown timer


        // Wait for trapDuration seconds
        yield return new WaitForSeconds(trapDuration);

        // Set "IsTrap" and "IsTrapping" to false to release the player from the trap
        player.GetComponent<Animator>().SetBool("IsTrap", false);
        player.GetComponent<Animator>().SetBool("IsTrapping", false);

        // Re-enable player movement
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }

        if (playerMovement2 != null)
        {
            playerMovement2.enabled = true;
        }

        // Reset isTrapped to false to allow trapping again after the cooldown
        isTrapped = false;
    }
}
