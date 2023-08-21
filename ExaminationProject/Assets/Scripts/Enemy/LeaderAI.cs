using System.Collections;
using UnityEngine;

public class LeaderAI : MonoBehaviour
{
    public delegate void PlayerSpotted(Vector3 playerPosition, bool isThreat);
    public static event PlayerSpotted OnPlayerSpotted;

    private bool isPatrolling = true;
    private Vector3 patrolTarget;

    public Transform player;
    private bool isChasing;

    private void Start()
    {
        StartCoroutine(PatrolRoutine());
    }

    private IEnumerator PatrolRoutine()
    {
        while (true)
        {
            if (isPatrolling)
            {
                PatrolRandomPoint();
            }
            yield return new WaitForSeconds(5f); // Change the interval as needed
        }
    }

    private void PatrolRandomPoint()
    {
        patrolTarget = GenerateRandomPoint();
        // Move towards the patrolTarget
        // ...
    }

    private Vector3 GenerateRandomPoint()
    {
        // Generate random X and Z coordinates within the patrol area
        float randomX = Random.Range(-10f, 10f);
        float randomZ = Random.Range(-10f, 10f);

        // Create a random point within the patrol area
        Vector3 randomPoint = new Vector3(randomX, transform.position.y, randomZ);

        return randomPoint;
    }

    private void Update()
    {
        if (isChasing)
        {
            // Chasing behavior
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            // Move towards the player's position
            // ...

            if (Vector3.Distance(transform.position, player.position) < 2f)
            {
                if (OnPlayerSpotted != null)
                {
                    OnPlayerSpotted(player.position, true);
                }
            }
        }
    }

        public void HandlePlayerSpotted(Vector3 playerPosition, bool isThreat)
    {
        if (OnPlayerSpotted != null)
        {
            OnPlayerSpotted(playerPosition, isThreat);
        }
    }
}
