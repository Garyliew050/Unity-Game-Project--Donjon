using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AN_PlugScript : MonoBehaviour
{
    [Tooltip("Feature for one using only")]
    public bool OneTime = false;
    [Tooltip("Plug follow this local EmptyObject")]
    public Transform HeroHandsPosition;
    [Tooltip("SocketObject with collider(shpere, box etc.) (is trigger = true)")]
    public Collider Socket; // need Trigger
    public AN_DoorScript DoorObject;
    [Tooltip("Distance at which the object is considered 'Near'")]
    public float nearDistance = 50f;

    AN_HeroInteractive hero;

    // NearView()
    float distance;
    float angleView;
    Vector3 direction;

    bool follow = false, isConnected = false, followFlag = false, youCan = true;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        hero = FindObjectOfType<AN_HeroInteractive>();
    }

    void Update()
    {
        if (youCan) Interaction();

        // frozen if it is connected to PowerOut
        if (isConnected)
        {
            gameObject.transform.position = Socket.transform.position;
            gameObject.transform.rotation = Socket.transform.rotation;
            DoorObject.isOpened = true;
        }
        else
        {
            DoorObject.isOpened = false;
        }
    }

    void Interaction()
    {
        if (NearView() && Input.GetKeyDown(KeyCode.E) && !follow)
        {
            isConnected = false; // unfrozen
            follow = true;
            followFlag = false;
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
        if (other == Socket)
        {
            isConnected = true;
            follow = false;
        }
        if (OneTime) youCan = false;
    }
}
