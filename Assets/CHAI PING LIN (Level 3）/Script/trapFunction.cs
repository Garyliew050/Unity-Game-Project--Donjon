using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapFunction : MonoBehaviour
{
    // The amount of damage this trap inflicts on the player.
    public int damageAmount = 10;

    // This method is called when another collider enters the trigger zone of this object.
    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering object has the "Player" tag.
        if (other.CompareTag("Player"))
        {
            // Attempt to get the PlayerController component from the entering object.
            PlayerController player = other.GetComponent<PlayerController>();

            // If a PlayerController component is found on the entering object.
            if (player != null)
            {
                // Call the TakeDamage method on the player, passing the damageAmount.
                player.TakeDamage(damageAmount);

                // Play a sound effect named "hit" to indicate the trap's activation.
                SoundManagerScript.PlaySound("hit");
            }
        }
    }
}
