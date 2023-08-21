using UnityEngine;

public class CannibalAI : MonoBehaviour
{
    private Transform leaderTransform;  // Reference to the LeaderAI's transform
    private bool isFollowingLeader = false;
    private bool isStraying = false;
    private Vector3 strayTarget;

    private void Start()
    {
        leaderTransform = GameObject.FindGameObjectWithTag("Leader").transform;
    }

    private void Update()
    {
        if (isFollowingLeader)
        {
            // Implement following behavior
            Vector3 direction = (leaderTransform.position - transform.position).normalized;
            // Move towards the leader's position
            // ...
        }
        else if (isStraying)
        {
            // Implement straying behavior
            Vector3 direction = (strayTarget - transform.position).normalized;
            // Move towards the stray target
            // ...

            // Check if the AI has reached the stray target
            if (Vector3.Distance(transform.position, strayTarget) < 1f)
            {
                StartFollowingLeader(); // Return to following the leader
            }
        }
        else
        {
            // Implement other behavior, such as attacking the player
        }
    }

    private void StartFollowingLeader()
    {
        isFollowingLeader = true;
        isStraying = false;
    }

    private void StopFollowingLeader()
    {
        isFollowingLeader = false;
        isStraying = false;
    }

    private void StartStraying(Vector3 target)
    {
        isFollowingLeader = false;
        isStraying = true;
        strayTarget = target;
    }

    private void OnEnable()
    {
        LeaderAI.OnPlayerSpotted += HandlePlayerSpotted;
    }

    private void OnDisable()
    {
        LeaderAI.OnPlayerSpotted -= HandlePlayerSpotted;
    }

    private void HandlePlayerSpotted(Vector3 playerPosition, bool isThreat)
    {
        if (isThreat)
        {
            // Inform the leader about the player
            leaderTransform.GetComponent<LeaderAI>().HandlePlayerSpotted(playerPosition, isThreat);

            if (!isFollowingLeader)
            {
                // Stray towards the player and consult the leader
                StartStraying(playerPosition);
            }
        }
    }
}
