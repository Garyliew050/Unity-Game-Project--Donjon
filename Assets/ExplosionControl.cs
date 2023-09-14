using UnityEngine;

public class ExplosionControl : MonoBehaviour
{
    private GameObject Explosion;
    private bool hasExploded = false;

    private void Start()
    {
        // Find the explosion parent object by name
        Explosion = transform.Find("Explosion").gameObject;
        // Disable emission for all child particle systems initially
        DisableParticleSystems();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !hasExploded)
        {
            // Enable emission for all child particle systems
            EnableParticleSystems();

            // Apply physics force to the explosion parent
            ApplyPhysicsForce();

            hasExploded = true;
        }
    }

    private void DisableParticleSystems()
    {
        // Disable emission for all child particle systems
        ParticleSystem[] particleSystems = Explosion.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem ps in particleSystems)
        {
            ps.Stop();
        }
    }

    private void EnableParticleSystems()
    {
        // Enable emission for all child particle systems
        ParticleSystem[] particleSystems = Explosion.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem ps in particleSystems)
        {
            ps.Play();
        }
    }

    private void ApplyPhysicsForce()
    {
        // Apply physics force to the explosion parent
        Rigidbody explosionRigidbody = Explosion.GetComponent<Rigidbody>();
        if (explosionRigidbody != null)
        {
            // Add force or any other physics behavior here
        }
    }
}
