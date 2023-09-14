using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yellowPlugScript : MonoBehaviour
{
    [Tooltip("Feature for one using only")]
    public bool OneTime = false;
    [Tooltip("Plug follow this local EmptyObject")]
    public Transform HeroHandsPosition;
    [Tooltip("SocketObject with collider(shpere, box etc.) (is trigger = true)")]
    public Collider SocketTrue; // need Trigger
    public Collider SocketFalse1;
    public Collider SocketFalse2;
    public Collider SocketFalse3;
    public string dictionary;
    [Tooltip("Distance at which the object is considered 'Near'")]
    public float nearDistance = 50f;

    AN_HeroInteractive hero;

    puzzleDoorScript2 door;

    private float triggerCooldown = 1.0f;
    private float lastTriggerTime = -1.0f; // Initialize it to a negative value to ensure the first trigger can happen immediately.

    // NearView()
    float distance;
    float angleView;
    Vector3 direction;

    bool follow = false, isConnectedTrue = false, isConnectedFalse1 = false, isConnectedFalse2 = false, isConnectedFalse3 = false, followFlag = false, youCan = true;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        hero = FindObjectOfType<AN_HeroInteractive>();
        door = FindObjectOfType<puzzleDoorScript2>();
    }

    void Update()
    {
        if (youCan) Interaction();

        // frozen if it is connected to PowerOut
        if (isConnectedTrue)
        {
            gameObject.transform.position = SocketTrue.transform.position;
            gameObject.transform.rotation = SocketTrue.transform.rotation;
            door.SetBool(dictionary,true);
        }
        if (isConnectedFalse1)
        {
            gameObject.transform.position = SocketFalse1.transform.position;
            gameObject.transform.rotation = SocketFalse1.transform.rotation;
            door.SetBool(dictionary, false);
        }
        if (isConnectedFalse2)
        {
            gameObject.transform.position = SocketFalse2.transform.position;
            gameObject.transform.rotation = SocketFalse2.transform.rotation;
            door.SetBool(dictionary, false);
        }
        if (isConnectedFalse3)
        {
            gameObject.transform.position = SocketFalse3.transform.position;
            gameObject.transform.rotation = SocketFalse3.transform.rotation;
            door.SetBool(dictionary, false);
        }
        else
        {
            //door.lockYellow = false;
        }
    }

    void Interaction()
    {
        if (NearView() && Input.GetKeyDown(KeyCode.E) && !follow)
        {
            lastTriggerTime = Time.time;
            isConnectedTrue = false; // unfrozen
            isConnectedFalse1 = false;
            isConnectedFalse2 = false;
            isConnectedFalse3 = false;
            follow = true;
            followFlag = false;
            door.SetBool(dictionary, false);
        }

        if (follow)
        {
            rb.drag = 10f;
            rb.angularDrag = 10f;
            if (followFlag)
            {
                distance = Vector3.Distance(transform.position, hero.transform.position);
                if (distance > 10f || Input.GetKeyDown(KeyCode.E))
                {
                    follow = false;
                }
            }

            followFlag = true;
            rb.AddExplosionForce(-1000f, HeroHandsPosition.position, 10f);
            // second variant of following
            //gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, objectLerp.position, 1f);
        }
        else
        {
            rb.drag = 0f;
            rb.angularDrag = .5f;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other == SocketTrue && CanTrigger())
        {
            isConnectedTrue = true;
            follow = false;
        }
        if (other == SocketFalse1 && CanTrigger())
        {
            isConnectedFalse1 = true;
            follow = false;
        }
        if (other == SocketFalse2 && CanTrigger())
        {
            isConnectedFalse2 = true;
            follow = false;
        }
        if (other == SocketFalse3 && CanTrigger())
        {
            isConnectedFalse3 = true;
            follow = false;
        }

        if (OneTime) youCan = false;
    }

    bool CanTrigger()
    {
        // Check if enough time has passed since the last trigger.
        return Time.time - lastTriggerTime >= triggerCooldown;
    }
}
