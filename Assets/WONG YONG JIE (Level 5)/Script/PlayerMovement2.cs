using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    public Rigidbody rb;
    private Animator animator;
    public float jumpForce = 10f;
    public float rollForce = 5f;
    private bool Jumping = false;
    private bool Landing = false;
    private bool Falling = false;
    private bool Rolling = false;

    // Start is called before the first frame update
    void Start()
    {
        // Get references to the Rigidbody and Animator components.
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Apply an upward force for jumping.
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetBool("IsJump", true);
            Jumping = true;
            SoundManagerScript.PlaySound("jumping");

            if (Jumping)
            {
                // If Jumping, set the Falling state to true.
                animator.SetBool("IsFall", true);
                Falling = true;

                if (Falling)
                {
                    // If Falling, set the Landing state to true.
                    animator.SetBool("IsLand", true);
                    Landing = true;
                }
                else
                {
                    // If not Falling, reset the Jumping state.
                    animator.SetBool("IsJump", false);
                    Jumping = false; // Player is not walking
                }
            }
            else
            {
                // If not Jumping, reset the Jumping state.
                animator.SetBool("IsJump", false);
                Jumping = false; // Player is not walking
            }
        }
        else
        {
            // If not pressing Space, reset the Jumping state.
            animator.SetBool("IsJump", false);
            Jumping = false; // Player is not walking
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            // Apply a forward force for rolling.
            rb.AddForce(transform.forward * rollForce, ForceMode.Impulse);
            animator.SetBool("IsRoll", true);
            SoundManagerScript.PlaySound("rolling");
            Rolling = true; // Update the Rolling state.
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            // When releasing C, reset the Rolling state.
            animator.SetBool("IsRoll", false);
            Rolling = false;
        }
    }
}
