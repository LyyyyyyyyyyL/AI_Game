using UnityEngine;
using UnityEngine.AI;

public class RunBehavior : BehaviorNode
{
    public GameObject enemy; // The enemy that needs to be moved
    public float runSpeed = 5f; // The speed for chasing
    public float rotationSpeed = 5f; // The speed for rotating
    private Transform enemyTransform; // Cache for the enemy's Transform
    private Transform playerTransform; // Cache for the player's Transform
    private NavMeshAgent navMeshAgent; // NavMeshAgent component for pathfinding

    void Start()
    {
        // Ensure the enemy object and NavMeshAgent are correctly initialized
        if (enemy != null)
        {
            enemyTransform = enemy.transform;
            navMeshAgent = enemy.GetComponent<NavMeshAgent>();  // Get the NavMeshAgent component

            if (navMeshAgent == null)
            {
                Debug.LogError("NavMeshAgent component is missing from the enemy GameObject!");
            }
            else
            {
                navMeshAgent.baseOffset = 1f; // Set the base offset for NavMeshAgent height
            }
        }
        else
        {
            Debug.LogError("Enemy GameObject not assigned!");
        }

        // Try to find the player’s Transform
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (playerTransform == null)
        {
            Debug.LogError("Player not found! Make sure the player has the 'Player' tag.");
        }

        // If the NavMeshAgent exists, adjust speed and angular speed
        if (navMeshAgent != null)
        {
            navMeshAgent.speed = runSpeed; // Adjust run speed
            navMeshAgent.angularSpeed = rotationSpeed * 100f; // Set rotation speed
            navMeshAgent.angularSpeed = Mathf.Clamp(navMeshAgent.angularSpeed, 0f, 300f); // Ensure angular speed is reasonable
        }

        Debug.Log("Run Behavior initialized.");
    }

    public override bool Run()
    {
        // Check essential conditions
        if (enemyTransform == null)
        {
            Debug.LogError("Enemy Transform is not assigned!");
            return false;
        }

        if (playerTransform == null)
        {
            Debug.LogWarning("Player not found, enemy cannot run towards player.");
            return false;
        }

        if (navMeshAgent != null)
        {
            // Use the NavMeshAgent for pathfinding
            navMeshAgent.speed = runSpeed;
            navMeshAgent.angularSpeed = rotationSpeed * 100f; // Convert to angular speed
            navMeshAgent.destination = playerTransform.position;

            // Return the status based on whether the NavMeshAgent is still moving
            return !navMeshAgent.isStopped;
        }
        else
        {
            // If NavMeshAgent is unavailable, fallback to manual movement logic
            Vector3 directionToPlayer = playerTransform.position - enemyTransform.position;

            // Move and rotate towards the player
            enemyTransform.Translate(directionToPlayer.normalized * runSpeed * Time.deltaTime, Space.World);
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            enemyTransform.rotation = Quaternion.Slerp(enemyTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            return true;
        }
    }
}
