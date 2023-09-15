using UnityEngine;

public class ParticleControl : MonoBehaviour
{
    private ParticleSystem particleSystem;
    public float proximityDistance = 2f; // Adjust this to set how close the player needs to be to the switch.
    public KeyCode activationKey = KeyCode.E; // Define the key to activate the particle system.

    private Transform switchTransform; // Reference to the switch GameObject.
    private Transform playerTransform; // Reference to the player GameObject.

    private void Awake()
    {
        // Get the Particle System component
        particleSystem = GetComponent<ParticleSystem>();
        // Disable emission immediately
        particleSystem.Stop();

        // Find the switch GameObject and player GameObject in your scene.
        switchTransform = GameObject.Find("Detonator").transform;
        playerTransform = GameObject.Find("Female A").transform; // Replace "Player" with the actual name of your player GameObject.
    }

    private void Update()
    {
        // Check if the player is near the switch and the activation key is pressed.
        float distanceToSwitch = Vector3.Distance(playerTransform.position, switchTransform.position);
        if (distanceToSwitch <= proximityDistance && Input.GetKeyDown(activationKey))
        {
            // Enable emission when the player is near the switch and presses the key.
            particleSystem.Play();
        }
    }
}
