using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class puzzleLeverScriptC : MonoBehaviour
{
    [Tooltip("Distance at which the object is considered 'Near'")]
    public float nearDistance = 30f;
    public ParticleSystem fireEffectA;
    public ParticleSystem fireEffectB;

    // NearView()
    float distance;
    float angleView;
    Vector3 direction;
    AN_HeroInteractive hero;

    puzzleDoorScript door;

    private float triggerCooldown = 1.0f;
    private float lastTriggerTime = -1.0f; // Initialize it to a negative value to ensure the first trigger can happen immediately.

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        fireEffectA.Stop();
        fireEffectB.Stop();
        hero = FindObjectOfType<AN_HeroInteractive>();
        door = FindObjectOfType<puzzleDoorScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && NearView() && CanTrigger())
        {
            if (anim.GetBool("LeverUp") == true)
            {
                anim.SetBool("LeverUp", false);
                if (fireEffectA != null)
                {
                    if (!fireEffectA.isPlaying)
                    {
                        door.lockB = true;
                        // Start the fire effect if it's not playing
                        fireEffectA.Play();
                        
                    }
                    else
                    {
                        door.lockB = false;
                        // Stop the fire effect if it's playing
                        fireEffectA.Stop();
                        
                    }
                }

                if (fireEffectB != null)
                {
                    if (!fireEffectB.isPlaying)
                    {
                        door.lockC = true;
                        // Start the fire effect if it's not playing
                        fireEffectB.Play();
                        
                    }
                    else
                    {
                        door.lockC = false;
                        // Stop the fire effect if it's playing
                        fireEffectB.Stop();
                        
                    }
                }
            }
            else
            {
                anim.SetBool("LeverUp", true);
                if (fireEffectA != null)
                {
                    if (!fireEffectA.isPlaying)
                    {
                        door.lockB = true;
                        // Start the fire effect if it's not playing
                        fireEffectA.Play();
                        
                    }
                    else
                    {
                        door.lockB = false;
                        // Stop the fire effect if it's playing
                        fireEffectA.Stop();
                        
                    }
                }

                if (fireEffectB != null)
                {
                    if (!fireEffectB.isPlaying)
                    {
                        door.lockC = true;
                        // Start the fire effect if it's not playing
                        fireEffectB.Play();
                        
                    }
                    else
                    {
                        door.lockC = false;
                        // Stop the fire effect if it's playing
                        fireEffectB.Stop();
                        
                    }
                }
            }
            lastTriggerTime = Time.time;
        }
    }

    bool NearView() // it is true if you near interactive object
    {
        float distance = Vector3.Distance(transform.position, hero.transform.position);
        if (distance < nearDistance)
            return true;
        else
            return false;
    }

    bool CanTrigger()
    {
        // Check if enough time has passed since the last trigger.
        return Time.time - lastTriggerTime >= triggerCooldown;
    }

}
