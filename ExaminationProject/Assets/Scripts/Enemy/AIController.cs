using System.Collections;
using UnityEngine;

public class AIController : MonoBehaviour
{
    private Transform player;       // Reference to the player's transform
    private bool isChasing;         // Flag to indicate if AI is chasing player
    private Vector3 patrolTarget;   // Target position for patrolling

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        isChasing = false;
        patrolTarget = transform.position; // Start with the AI's initial position
        StartCoroutine(DecisionRoutine());
    }

    private IEnumerator DecisionRoutine()
    {
        while (true)
        {
            // Make decisions every few seconds
            yield return new WaitForSeconds(2f);

            // Calculate distance to player
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer < 10f)
            {
                isChasing = true;
            }
            else
            {
                isChasing = false;
                CalculatePatrolTarget();
            }

            yield return null; // Yield to the next frame
        }
    }

    private void Update()
    {
        if (isChasing)
        {
            // Implement chasing behavior here
            Vector3 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * Time.deltaTime * 5f);
        }
        else
        {
            // Implement patrolling behavior here
            Vector3 direction = (patrolTarget - transform.position).normalized;
            transform.Translate(direction * Time.deltaTime * 2f);

            // Check if AI is close to the patrol target
            if (Vector3.Distance(transform.position, patrolTarget) < 1f)
            {
                CalculatePatrolTarget(); // Choose a new random patrol target
            }
        }
    }

    private void CalculatePatrolTarget()
    {
        // Generate a random patrol target within a certain radius of the AI's current position
        float patrolRadius = 10f;
        patrolTarget = transform.position + Random.insideUnitSphere * patrolRadius;
        patrolTarget.y = transform.position.y; // Keep the same height
    }
}


