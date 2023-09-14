using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // public void TakeDamage(int damageAmount)
    // {
    //     currentHealth -= damageAmount;

    //     if (currentHealth <= 0)
    //     {
    //         currentHealth = 0;
    //         Debug.Log("Player defeated");
    //         // Implement player death or game over logic
    //     }
    // }

    // public void Heal(int healAmount)
    // {
    //     currentHealth += healAmount;

    //     if (currentHealth > maxHealth)
    //     {
    //         currentHealth = maxHealth;
    //     }
    // }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}