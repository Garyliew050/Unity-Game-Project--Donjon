using UnityEngine;

public class PathFollowing : MonoBehaviour
{
   
    public Transform[] waypoints; // Array to store your waypoints or keyframes
    public float speed = 2.0f;    // Speed of movement between waypoints
    public float rotationSpeed = 5.0f; // Speed of rotation
    public float attackRange = 2.0f; // Range at which the object will attack the player
    public Transform player; // Reference to the player's transform
    public Animator animator; // Reference to the Animator component
    public int damagePerHit = 10; // Damage amount per hit
    public float attackCooldown = 2.0f; // Cooldown time between attacks
    private float lastAttackTime = 0.0f; // Time when the last attack occurred

    private int currentWaypointIndex = 0;

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player reference is not set!");
            enabled = false; // Disable the script if the player reference is missing
        }

        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    void Update()
    {
        if (currentWaypointIndex < waypoints.Length)
        {
            // Calculate the next position along the path
            Vector3 targetPosition = waypoints[currentWaypointIndex].position;

            // Calculate the direction to the target
            Vector3 moveDirection = targetPosition - transform.position;

            // Rotate towards the target direction
            if (moveDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

            // Check if the player is within attack range
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer <= attackRange)
            {
                // Check if enough time has passed since the last attack
                if (Time.time - lastAttackTime >= attackCooldown)
                {
                    // Trigger the attack animation
                    animator.SetTrigger("Attack");

                    // Call the TakeDamage method on the player to inflict damage
                    PlayerController playerController = player.GetComponent<PlayerController>();
                    if (playerController != null)
                    {
                        playerController.TakeDamage(damagePerHit);
                    }

                    lastAttackTime = Time.time; // Record the time of the attack
                }
            }
            else
            {
                // Move towards the target position
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            }

            // Check if the object has reached the current waypoint
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                currentWaypointIndex++; // Move to the next waypoint

                // If we reached the end of the waypoints, loop back to the first one
                if (currentWaypointIndex >= waypoints.Length)
                {
                    currentWaypointIndex = 0;
                }
            }

            // Set the IsRunning parameter in the Animator to true
            animator.SetBool("IsRunning", true);
        }
        else
        {
            // If there are no more waypoints, stop the animation and reset rotation
            animator.SetBool("IsRunning", false);
            transform.rotation = Quaternion.identity;
        }
    }
    
}
