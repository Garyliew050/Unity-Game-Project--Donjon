using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : MonoBehaviour
{
    public NavMeshAgent enemy;
    public Transform Player;
    public float timer;
    private Animator animator;
    public float attackdistance = 15f;
    //Attack point
    public float attackRange = 15f;
    public Transform attackPoint;
    public LayerMask playerLayers;
    public int attackdamage = 20;
    private float attackCooldown = 2f; // Adjust the cooldown time as needed
    private float lastAttackTime;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent <Animator> ();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(enemy.transform.position, Player.position);
        Debug.Log("distanceToPlayer  " + distanceToPlayer);
        Debug.Log("attackRange  " + attackRange);
        
        
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            if (distanceToPlayer <= attackdistance)
            {
                Debug.Log("Attack");
                Attack();
                lastAttackTime = Time.time; // Update the last attack time
            }
            else{
                ChasePlayer();
            }
        }
    }

    void ChasePlayer()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;   
        }
        else 
        {
            enemy.SetDestination(Player.position);
            animator.SetBool("IsRunning",true);
            // animator.SetTrigger(name:"ReachPlayer");
            // //enemy.SetDestination(Player.position);
            // Debug.Log("Agent chasing moving position.");
            // Debug.Log("Moving position: " + Player.position);
            // Debug.Log("Agent destination: " + enemy.destination);
            
        }
    }

    void Attack()
    {
        Debug.Log("Trigger Attack");
        animator.SetTrigger("Attack");  
        Collider [] hitPlayer = Physics.OverlapSphere(attackPoint.position, attackRange,playerLayers);
        Debug.Log("no");
        Debug.Log("Player.position  " + Player.position);
        Debug.Log("attackPoint.position: " + attackPoint.position);
        Debug.Log("attackRange: " + attackRange);
        foreach(Collider Player in hitPlayer) 
        {
            Player.GetComponent<PlayerController>().TakeDamage(attackdamage);
            Debug.Log("hit");
            Debug.Log("attackPoint.position: " + attackPoint.position);
            Debug.Log("attackRange: " + attackRange);
            SoundManagerScript.PlaySound("hit");
        }

    // Collider[] hitResults = new Collider[1];
    // int numHits = Physics.OverlapSphereNonAlloc(attackPoint.position, attackRange, hitResults, playerLayers);
    // Debug.Log("Number of players hit: " + numHits);
    // if (numHits > 0)
    // {
    //     Collider playerCollider = hitResults[0];
    //     Player.GetComponent<PlayerController>().TakeDamage(attackdamage);
    //     Debug.Log("Hit " + playerCollider.name);

    //     // Perform actions specific to hitting a player
    //     // For example: playerCollider.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
    // }
    // else
    // {
    //     Debug.Log("No players hit");
    // }
    }

    void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return; 
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
