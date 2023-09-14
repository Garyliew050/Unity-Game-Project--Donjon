using UnityEngine;

public class FireDamage : MonoBehaviour
{
    public int damagePerSecond = 10; // Adjust the damage amount per second

    // This method is called when a particle collides with another object.
    private void OnParticleCollision(GameObject other)
    {
        // Check if the colliding object has a PlayerController component.
        PlayerController playerController = other.GetComponent<PlayerController>();

        if (playerController != null)
        {
            // Apply damage to the colliding object (assuming it has a TakeDamage method).
            playerController.TakeDamage(damagePerSecond);
        }
    }
}
