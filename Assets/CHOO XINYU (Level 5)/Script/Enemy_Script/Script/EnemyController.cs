using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    //Freeze function
    private Color originalColor;
    private bool isFrozen = false;
    private Animator animator;
    public NavMeshAgent enemy;
    private float originalAgentSpeed;
    private float originalAcceleration;

    // Start is called before the first frame update
    void Start()
    {

        animator = GetComponent <Animator> ();
        originalColor = GetComponent<Renderer>().material.color;
        originalAgentSpeed = enemy.speed;
        originalAcceleration = enemy.acceleration;
        Debug.Log("originalAgentSpeed   " + originalAgentSpeed);
    }


    // // Update is called once per frame
    // void Update()
    // {
        
    // }
    
    public void Freeze(){
        if (!isFrozen)
        {
            // Turn the enemy blue
            GetComponent<Renderer>().material.color = Color.blue;

            enemy.isStopped = true; 
            isFrozen = true;
        }
    }

    public void Unfreeze()
    {
        if (isFrozen)
        {
            // Restore the enemy's original color
            GetComponent<Renderer>().material.color = originalColor;
enemy.isStopped = false;
            // // Allow the enemy to move again
            // enemyRigidbody.isKinematic = false;
            isFrozen = false;
        }
    }
}
