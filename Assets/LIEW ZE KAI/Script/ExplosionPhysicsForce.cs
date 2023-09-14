using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Effects
{
    public class ExplosionPhysicsForce : MonoBehaviour
    {
        public float explosionForce = 4;
        public float proximityDistance = 2f; // Adjust this to set how close the player needs to be to the switch.
        public KeyCode explosionKey = KeyCode.E; // Define the key to trigger the explosion.
        public float delayBeforeDestroy = 2.0f; // Adjust this to set the delay before destroying objects.

        private bool hasExploded = false;
        private Transform switchTransform; // Reference to the switch GameObject.
        private Transform playerTransform; // Reference to the player GameObject.

        private void Start()
        {
            // Find the switch GameObject and player GameObject in your scene.
            switchTransform = GameObject.Find("Detonator").transform;
            playerTransform = GameObject.Find("Female A").transform; // Replace "Player" with the actual name of your player GameObject.
        }

        private void Update()
        {
            // Check if the player is near the switch and the 'E' key is pressed and the explosion hasn't happened yet.
            if (!hasExploded && Input.GetKeyDown(explosionKey))
            {
                float distanceToSwitch = Vector3.Distance(playerTransform.position, switchTransform.position);
                if (distanceToSwitch <= proximityDistance)
                {
                    // Call the Explode method.
                    Explode();
                }
            }
        }

        private void Explode()
        {
            // Rest of your explosion code remains the same.
            float multiplier = GetComponent<ParticleSystemMultiplier>().multiplier;
            float r = 10 * multiplier;
            var cols = Physics.OverlapSphere(transform.position, r);
            var rigidbodies = new List<Rigidbody>();
            foreach (var col in cols)
            {
                if (col.attachedRigidbody != null && !rigidbodies.Contains(col.attachedRigidbody))
                {
                    rigidbodies.Add(col.attachedRigidbody);
                }
            }
            foreach (var rb in rigidbodies)
            {
                rb.AddExplosionForce(explosionForce * multiplier, transform.position, r, 1 * multiplier, ForceMode.Impulse);
            }

            // Mark that the explosion has occurred.
            hasExploded = true;

            // Start a coroutine to delay the disabling or destroying of objects.
            StartCoroutine(DelayedObjectDisablingOrDestroying());
        }

        private IEnumerator DelayedObjectDisablingOrDestroying()
        {
            yield return new WaitForSeconds(delayBeforeDestroy);

            // Find the skeleton and bomb objects and disable or destroy them.
            GameObject skeletonObject = GameObject.Find("SKELETON"); // Replace with the actual name of your skeleton GameObject.
            GameObject bombObject = GameObject.Find("Oil_Drum"); // Replace with the actual name of your bomb GameObject.

            if (skeletonObject != null)
            {
                // Disable or destroy the skeleton GameObject.
                skeletonObject.SetActive(false); // Use SetActive(false) to disable.
                // Alternatively, you can destroy it if you want it to be removed from the scene.
                // Destroy(skeletonObject);
            }

            if (bombObject != null)
            {
                // Disable or destroy the bomb GameObject.
                bombObject.SetActive(false); // Use SetActive(false) to disable.
                // Alternatively, you can destroy it if you want it to be removed from the scene.
                // Destroy(bombObject);
            }
        }
    }
}
