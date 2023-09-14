using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovingTrap : MonoBehaviour
{
    public NavMeshAgent enemy;
    public Transform Player;
    private Animator animator;

    //Detect
    public float detectRange = 15f;
    public Transform detectPoint;
    public Vector3 detectorOriginLocation;
    public bool isDetect;

    //Attack point
    public float attackdistance;    
    public float attackRange = 15f;
    public Transform attackPoint;
    public LayerMask playerLayers;
    public int attackdamage = 20;
    private float attackCooldown = 2f; // Adjust the cooldown time as needed
    private float lastAttackTime;

    // Start is called before the first frame update
    void Start()
    {
        detectorOriginLocation = enemy.transform.position;
        animator = GetComponent <Animator> ();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(enemy.transform.position, Player.position);
        float distanceFromOrigin = Vector3.Distance(enemy.transform.position,detectorOriginLocation);
        Debug.Log("distanceToPlayer  " + distanceToPlayer);
        Debug.Log("attackRange  " + attackRange);
        
        if (distanceToPlayer <= detectRange)
        {
            isDetect = true;
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                if (distanceToPlayer <= attackdistance)
                {
                    Attack();
                    lastAttackTime = Time.time; // Update the last attack time
                }
                else if((detectRange * 2 ) <= distanceFromOrigin)
                {
                    ReturnToOriginLocation();
                }
                else{

                    ChasePlayer();
                }
            }
        }
    }

    void ChasePlayer()
    {
            //Player.position += attackdistance;
            enemy.SetDestination(Player.position);
            animator.SetBool("IsRunning",true);
    }

    void Attack()
    {
        animator.SetTrigger("Attack");  
        Collider [] hitPlayer = Physics.OverlapSphere(attackPoint.position, attackRange,playerLayers);
        foreach(Collider Player in hitPlayer) 
        {
            Player.GetComponent<PlayerController>().TakeDamage(attackdamage);
            SoundManagerScript.PlaySound("hit");
        }
    }

    void ReturnToOriginLocation()
    {
            enemy.SetDestination(detectorOriginLocation);
            animator.SetBool("IsRunning",false);
            isDetect = false;
    }

    void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return; 
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        if(isDetect){
            Gizmos.color = Color.red;
        } else {
            Gizmos.color = Color.green;
        }
        Gizmos.DrawWireSphere(detectPoint.position, detectRange);
    }
}
