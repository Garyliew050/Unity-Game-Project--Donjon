using UnityEngine;
using UnityStandardAssets.Effects;
public class ExplosionController : MonoBehaviour
{
    private ExplosionPhysicsForce explosionPhysicsForce;
    private bool hasExploded = false;

    private void Start()
    {
        explosionPhysicsForce = GetComponent<ExplosionPhysicsForce>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !hasExploded)
        {
            // Trigger the explosion physics force
            explosionPhysicsForce.enabled = true;
            hasExploded = true;
        }
    }
}
