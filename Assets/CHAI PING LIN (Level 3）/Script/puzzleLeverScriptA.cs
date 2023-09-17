using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzleLeverScriptA : MonoBehaviour
{
    // The distance at which the object is considered 'Near' for interaction.
    [Tooltip("Distance at which the object is considered 'Near'")]
    public float nearDistance = 30f;

    // Particle systems for fire effects.
    public ParticleSystem fireEffectA;
    public ParticleSystem fireEffectB;

    // Reference variables.
    float distance;
    float angleView;
    Vector3 direction;
    AN_HeroInteractive hero;
    puzzleDoorScript door;

    // Cooldown duration between consecutive triggers.
    private float triggerCooldown = 1.0f;

    // The time of the last trigger to enforce the cooldown.
    private float lastTriggerTime = -1.0f;

    // Animator component for controlling the lever's animation.
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Animator component attached to this object.
        anim = GetComponent<Animator>();

        // Stop the fire particle effects initially.
        fireEffectA.Stop();
        fireEffectB.Stop();

        // Find and store a reference to the hero character's interaction script in the scene.
        hero = FindObjectOfType<AN_HeroInteractive>();

        // Find and store a reference to the puzzle door script in the scene.
        door = FindObjectOfType<puzzleDoorScript>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check for player input ("E" key) and proximity to the lever, and ensure trigger cooldown has passed.
        if (Input.GetKeyDown(KeyCode.E) && NearView() && CanTrigger())
        {
            // Toggle the lever's state based on its current state.
            if (anim.GetBool("LeverUp") == true)
            {
                anim.SetBool("LeverUp", false);

                // Manage the fire effect for fireEffectA.
                if (fireEffectA != null)
                {
                    if (!fireEffectA.isPlaying)
                    {
                        door.lockA = true;
                        // Start the fire effect if it's not playing.
                        fireEffectA.Play();
                    }
                    else
                    {
                        door.lockA = false;
                        // Stop the fire effect if it's playing.
                        fireEffectA.Stop();
                    }
                }

                // Manage the fire effect for fireEffectB.
                if (fireEffectB != null)
                {
                    if (!fireEffectB.isPlaying)
                    {
                        door.lockB = true;
                        // Start the fire effect if it's not playing.
                        fireEffectB.Play();
                    }
                    else
                    {
                        door.lockB = false;
                        // Stop the fire effect if it's playing.
                        fireEffectB.Stop();
                    }
                }
            }
            else
            {
                anim.SetBool("LeverUp", true);

                // Manage the fire effect for fireEffectA (similar to above).
                if (fireEffectA != null)
                {
                    if (!fireEffectA.isPlaying)
                    {
                        door.lockA = true;
                        fireEffectA.Play();
                    }
                    else
                    {
                        door.lockA = false;
                        fireEffectA.Stop();
                    }
                }

                // Manage the fire effect for fireEffectB (similar to above).
                if (fireEffectB != null)
                {
                    if (!fireEffectB.isPlaying)
                    {
                        door.lockB = true;
                        fireEffectB.Play();
                    }
                    else
                    {
                        door.lockB = false;
                        fireEffectB.Stop();
                    }
                }
            }

            // Record the time of this trigger.
            lastTriggerTime = Time.time;
        }
    }

    // Check if the player character is near the interactive object.
    bool NearView()
    {
        float distance = Vector3.Distance(transform.position, hero.transform.position);
        if (distance < nearDistance)
            return true;
        else
            return false;
    }

    // Check if enough time has passed since the last trigger to allow another trigger.
    bool CanTrigger()
    {
        return Time.time - lastTriggerTime >= triggerCooldown;
    }
}
