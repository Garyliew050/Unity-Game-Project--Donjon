using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yellowPlugScript : MonoBehaviour
{
    // This variable controls whether the interaction with the plug can only occur once.
    [Tooltip("Feature for one-time use only")]
    public bool OneTime = false;

    // This is the position that the plug should follow when picked up.
    [Tooltip("Plug follows this local EmptyObject")]
    public Transform HeroHandsPosition;

    // The main socket that the plug can connect to (requires a trigger collider).
    [Tooltip("SocketObject with collider(sphere, box, etc.) (is trigger = true)")]
    public Collider SocketTrue;

    // Other sockets that the plug can connect to (requires a trigger collider).
    public Collider SocketFalse1;
    public Collider SocketFalse2;
    public Collider SocketFalse3;

    // A dictionary key used to control a door's behavior.
    public string dictionary;

    // The distance at which the object is considered 'Near' for interaction.
    [Tooltip("Distance at which the object is considered 'Near'")]
    public float nearDistance = 50f;

    // Reference to the hero character's interaction script.
    AN_HeroInteractive hero;

    // Reference to a puzzle door script.
    puzzleDoorScript2 door;

    // Cooldown duration between consecutive triggers.
    private float triggerCooldown = 1.0f;

    // The time of the last trigger to enforce the cooldown.
    private float lastTriggerTime = -1.0f;

    // Variables used for determining if the plug is near the hero's hands and being carried.
    float distance;
    float angleView;
    Vector3 direction;

    // Flags to control various states of the plug.
    bool follow = false, isConnectedTrue = false, isConnectedFalse1 = false, isConnectedFalse2 = false, isConnectedFalse3 = false, followFlag = false, youCan = true;

    // Reference to the plug's Rigidbody component.
    Rigidbody rb;

    void Start()
    {
        // Get a reference to the Rigidbody component of the plug.
        rb = GetComponent<Rigidbody>();

        // Find and store a reference to the hero character's interaction script in the scene.
        hero = FindObjectOfType<AN_HeroInteractive>();

        // Find and store a reference to the puzzle door script in the scene.
        door = FindObjectOfType<puzzleDoorScript2>();
    }

    void Update()
    {
        if (youCan)
        {
            // Perform interactions with the plug if allowed.
            Interaction();
        }

        // Determine if the plug is connected to any socket and update its position accordingly.
        if (isConnectedTrue)
        {
            // Snap the plug's position and rotation to the true socket.
            gameObject.transform.position = SocketTrue.transform.position;
            gameObject.transform.rotation = SocketTrue.transform.rotation;

            // Set a boolean in the door script based on the dictionary key.
            door.SetBool(dictionary, true);
        }
        if (isConnectedFalse1)
        {
            // Snap the plug's position and rotation to false socket 1.
            gameObject.transform.position = SocketFalse1.transform.position;
            gameObject.transform.rotation = SocketFalse1.transform.rotation;

            // Set a boolean in the door script based on the dictionary key.
            door.SetBool(dictionary, false);
        }
        if (isConnectedFalse2)
        {
            // Snap the plug's position and rotation to false socket 2.
            gameObject.transform.position = SocketFalse2.transform.position;
            gameObject.transform.rotation = SocketFalse2.transform.rotation;

            // Set a boolean in the door script based on the dictionary key.
            door.SetBool(dictionary, false);
        }
        if (isConnectedFalse3)
        {
            // Snap the plug's position and rotation to false socket 3.
            gameObject.transform.position = SocketFalse3.transform.position;
            gameObject.transform.rotation = SocketFalse3.transform.rotation;

            // Set a boolean in the door script based on the dictionary key.
            door.SetBool(dictionary, false);
        }
        else
        {
            // Handle the case where the plug is not connected to any socket.
            // door.lockYellow = false; (This line is commented out and may need further explanation)
        }
    }

    void Interaction()
    {
        // Pick up the item from a socket if near and the "E" key is pressed, and the plug is not currently being carried.
        if (NearView() && Input.GetKeyDown(KeyCode.E) && !follow)
        {
            // Record the time of this trigger.
            lastTriggerTime = Time.time;

            // Reset connection flags for all sockets.
            isConnectedTrue = false;
            isConnectedFalse1 = false;
            isConnectedFalse2 = false;
            isConnectedFalse3 = false;

            // Set the plug to follow the hero's hands and update its state.
            follow = true;
            followFlag = false;

            // Update the door state based on the dictionary key.
            door.SetBool(dictionary, false);
        }

        // Put down the carrying item if it was being carried.
        if (follow)
        {
            // Set drag and angular drag to control plug's behavior when carried.
            rb.drag = 10f;
            rb.angularDrag = 10f;

            if (followFlag)
            {
                // Calculate the distance between the plug and the hero.
                distance = Vector3.Distance(transform.position, hero.transform.position);

                // If the distance is greater than 10 units or the "E" key is pressed, stop carrying the plug.
                if (distance > 10f || Input.GetKeyDown(KeyCode.E))
                {
                    follow = false;
                }
            }

            followFlag = true;

            // Apply an explosive force to the plug to simulate carrying.
            rb.AddExplosionForce(-1000f, HeroHandsPosition.position, 10f);
            // Alternative method of following:
            // gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, objectLerp.position, 1f);
        }
        else
        {
            // Reset drag and angular drag when the plug is not being carried.
            rb.drag = 0f;
            rb.angularDrag = .5f;
        }
    }

    // Check if the hero is near the interactive object.
    bool NearView()
    {
        float distance = Vector3.Distance(transform.position, hero.transform.position);
        if (distance < nearDistance)
            return true;
        else
            return false;
    }

    // Handle collision with trigger colliders for the plug.
    private void OnTriggerEnter(Collider other)
    {
        if (other == SocketTrue && CanTrigger())
        {
            // The plug is connected to the true socket, and it should stop being carried.
            isConnectedTrue = true;
            follow = false;
        }
        if (other == SocketFalse1 && CanTrigger())
        {
            // The plug is connected to false socket 1, and it should stop being carried.
            isConnectedFalse1 = true;
            follow = false;
        }
        if (other == SocketFalse2 && CanTrigger())
        {
            // The plug is connected to false socket 2, and it should stop being carried.
            isConnectedFalse2 = true;
            follow = false;
        }
        if (other == SocketFalse3 && CanTrigger())
        {
            // The plug is connected to false socket 3, and it should stop being carried.
            isConnectedFalse3 = true;
            follow = false;
        }

        if (OneTime)
        {
            // Disable further interactions with the plug if it's set to one-time use.
            youCan = false;
        }
    }

    // Check if enough time has passed since the last trigger to allow another trigger.
    bool CanTrigger()
    {
        return Time.time - lastTriggerTime >= triggerCooldown;
    }
}