using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator animator;
    private bool walking = false;


    public AudioClip walk;
    public AudioClip sprite;
    private AudioSource player;


    public float moveSpeed = 5f;
    public float rotationSpeed = 90f;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<AudioSource>();

        if (player == null)
        {
            player = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");


        if (vertical != 0 || horizontal != 0)
        {
            Vector3 moveDirection = new Vector3(horizontal, 0, vertical).normalized;

            if (horizontal != 0)
            {
                transform.Rotate(Vector3.up * horizontal * rotationSpeed * Time.deltaTime);
            }

            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

            animator.SetBool("IsRun", true);
            walking = true;

            if (!player.isPlaying)
            {
                player.clip = walk;
                player.Play();
            }
        }
        else
        {
            animator.SetBool("IsRun", false);
            walking = false;
            player.Stop();
        }


        if (Input.GetKeyDown(KeyCode.LeftShift) && walking)
        {
            animator.SetTrigger("Sprite");
            player.clip = sprite;
            player.Play();
        }
    }
}