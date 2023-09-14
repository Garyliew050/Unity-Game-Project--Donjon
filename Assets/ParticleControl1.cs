using UnityEngine;

public class ParticleControl1 : MonoBehaviour
{
    private ParticleSystem particleSystem;

    private void Start()
    {
        // Get the Particle System component
        particleSystem = GetComponent<ParticleSystem>();
        // Disable emission initially
        particleSystem.Stop();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Enable emission when the 'E' key is pressed
            particleSystem.Play();
        }
    }
}
