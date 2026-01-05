using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ThrowController))]
[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
[RequireComponent(typeof(Taggable))]
public class EnemyController : MonoBehaviour
{
    // References
    public Transform player; // Reference to the player
    private UnityEngine.AI.NavMeshAgent enemy; // Reference to the enemy's navMesh
    private Animator animator;

    // Enemy movement
    public float fleeDistance = 10f;  // How far the enemy will run away
    public float stopRange = 5f; // So it doesn't run into the player

    // Shooting Settings
    private ThrowController throwController; // Giving the enemy the ability to throw/tag the player
    private Taggable taggable;  // Will be used tto check if the enemy is it
    
    // To tweak the enemy's shooting
    public float shootTimer = 0f; 
    public float shootCooldown = 1f;

    void Start()
    {

        // References
        enemy = GetComponent<UnityEngine.AI.NavMeshAgent>(); 
        taggable = GetComponent<Taggable>();
        animator = GetComponent<Animator>();
        throwController = GetComponent<ThrowController>();

    }

    void Update()
    {
        // Checks if the tagManager and taggable exists 
        if (TagManager.Instance == null || taggable == null) return;

        // Increment shoot timer
        shootTimer += Time.deltaTime;

        // Calculates the distance from the player to determine when the player will need to stop running
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Animation control
        bool isWalking = enemy.velocity.magnitude > 0.1f;
        animator.SetBool("IsWalking", isWalking);

        if (taggable.canTagOthers)
        {
            // Enemy is it -> chases the player
            // Checks if the enemy reaches a certain distance from the player
            if (distanceToPlayer > stopRange)
                enemy.SetDestination(player.position);
            else
                enemy.ResetPath(); // Stop moving once its too close
            
            if (shootTimer >= shootCooldown && throwController != null)
            {
                throwController.Shoot();
                shootTimer = 0f;
            }
        }
        else
        {
            // Player is it -> run away from the player
            Vector3 directionAway = transform.position - player.position; // direction opposite the player
            Vector3 fleeTarget = transform.position + directionAway.normalized * fleeDistance;

            UnityEngine.AI.NavMeshHit hit;
            if (UnityEngine.AI.NavMesh.SamplePosition(fleeTarget, out hit, 5f, UnityEngine.AI.NavMesh.AllAreas))
            {
                enemy.SetDestination(hit.position);
            }
        }
    }
}
