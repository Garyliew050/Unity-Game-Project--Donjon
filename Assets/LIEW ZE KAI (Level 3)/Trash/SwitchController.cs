using UnityEngine;

public class SwitchController : MonoBehaviour
{
    public ParticleSystem particleEffect;
    public GameObject bomb;
    public KeyCode activationKey = KeyCode.E;

    private bool isActivated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isActivated && other.CompareTag("Player") && Input.GetKeyDown(activationKey))
        {
            // Play the particle effect
            particleEffect.Play();

            // Disable the bomb and the switch
            bomb.SetActive(false);
            gameObject.SetActive(false);

            isActivated = true;
        }
    }
}

