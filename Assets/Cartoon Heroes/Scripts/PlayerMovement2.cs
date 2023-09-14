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
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) )
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetBool("IsJump", true);
            Jumping = true;
            SoundManagerScript.PlaySound("jumping");
            if (Jumping == true)
            {
                animator.SetBool("IsFall", true);
                Falling = true;
                if (Falling == true)
                {
                    animator.SetBool("IsLand", true);
                    Landing = true;
                }
                else
                {
                    animator.SetBool("IsJump", false);
                    Jumping = false; // Player is not walking
                }
            }
            else
            {
                animator.SetBool("IsJump", false);
                Jumping = false; // Player is not walking
            }
        }
        else
        {
            animator.SetBool("IsJump", false);
            Jumping = false; // Player is not walking
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            rb.AddForce(transform.forward * rollForce, ForceMode.Impulse);
            animator.SetBool("IsRoll", true);  
            SoundManagerScript.PlaySound("rolling");
        }
        if(Input.GetKeyUp(KeyCode.C))
        {
            animator.SetBool("IsRoll", false);
        }

    }
}
