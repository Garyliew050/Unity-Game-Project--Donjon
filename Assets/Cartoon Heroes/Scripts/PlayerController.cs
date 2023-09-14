using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float raycastDistance = 0.5f;
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0.0f, verticalInput);

        // Cast a ray in the movement direction to check for obstacles
        if (Physics.Raycast(transform.position, movement, raycastDistance))
        {
            // If an obstacle is detected, don't move
            movement = Vector3.zero;
        }

        // Move the player
        transform.Translate(movement * moveSpeed * Time.deltaTime);
        // if(Input.GetKeyDown(KeyCode.Space))
        // {
        //     TakeDamage(20);
        // }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("DIE !!");
        SoundManagerScript.PlaySound("lose");
        SceneManager.LoadScene("GameOver");

    }
}

