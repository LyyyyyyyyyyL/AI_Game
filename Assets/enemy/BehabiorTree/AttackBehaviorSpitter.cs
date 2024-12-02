using UnityEngine;
using UnityEngine.AI;

public class AttackBehaviorSpitterObject : BehaviorNode
{
    // Venom spawn timer and interval
    private float lastVenomSpawnTime = 0f; // Last venom spawn time
    public float venomSpawnInterval = 2f; // Interval between venom spawns (seconds)
    public GameObject enemy;      // The entire enemy object
    public GameObject enemyArm;   // The enemy's arm
    public float rotationSpeed;   // Arm rotation speed, in degrees per second
    public int damage = 10;       // Damage dealt each time

    private Transform targetTransform;  // Stores the target Transform
    private NavMeshAgent navMeshAgent;  // Enemy's NavMeshAgent component
    private PlayerHealth playerHealth; // Player's health script
    private float lastCheckTime = 0f; // Last check time
    public float checkInterval = 1f; // Check interval in seconds

    // New field
    private enemyIsTraggered enemyTriggerScript;  // To access enemyIsTraggered script

    // Venom area related
    public GameObject venomPrefab;    // Venom area prefab
    public float venomLifetime = 5f;  // Venom area's lifetime

    void Start()
    {
        Debug.Log("Entering the attack behavior");

        // Get NavMeshAgent component
        navMeshAgent = enemy.GetComponent<NavMeshAgent>();
        if (navMeshAgent == null)
        {
            Debug.LogError("Enemy does not have a NavMeshAgent component!");
        }

        // Get enemyIsTraggered script
        enemyTriggerScript = enemy.GetComponent<enemyIsTraggered>();
        if (enemyTriggerScript == null)
        {
            Debug.LogError("Enemy does not have the enemyIsTraggered component!");
        }

        // Find target object
        GameObject target = GameObject.FindGameObjectWithTag("Player");
        if (target != null)
        {
            targetTransform = target.transform;
            playerHealth = target.GetComponent<PlayerHealth>();
            if (playerHealth == null)
            {
                Debug.LogError("Player does not have a PlayerHealth component!");
            }
        }
        else
        {
            Debug.LogError("Target with tag 'Player' not found!");
        }
    }

    public override bool Run()
    {
        // Ensure enemy, enemy's arm, and NavMeshAgent exist
        if (enemy == null || enemyArm == null || navMeshAgent == null || targetTransform == null || playerHealth == null || enemyTriggerScript == null || venomPrefab == null)
        {
            Debug.LogError("Missing required components!");
            return false;
        }

        // Get isInPlayerInAttackTrigger value from enemyIsTraggered script
        bool isPlayerInAttackTrigger = enemyTriggerScript.isInPlayerInAttackTrigger;

        // Rotate the arm
        RotateArmOnXAxis();

        // Check every second
        if (Time.time - lastCheckTime >= checkInterval)
        {
            lastCheckTime = Time.time;

            // If the player is within attack range
            if (isPlayerInAttackTrigger)
            {
                // Deal damage to the player
                playerHealth.TakeDamage(damage);
                Debug.Log("Player hit! Remaining health: " + playerHealth.currentHealth);

                // Check venom spawn interval
                if (Time.time - lastVenomSpawnTime >= venomSpawnInterval)
                {
                    SpawnVenomAtPlayerPosition();
                    lastVenomSpawnTime = Time.time; // Update last venom spawn time
                }
            }
            else
            {
                // Player is out of range, switch to chasing behavior
                Debug.Log("Returning false");
                return false;
            }
        }

        // Set the NavMeshAgent's destination
        navMeshAgent.SetDestination(targetTransform.position);

        return true; // Behavior continues to run
    }

    // Method to rotate the arm on the X-axis
    void RotateArmOnXAxis()
    {
        if (enemyArm != null)
        {
            enemyArm.transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime, Space.Self);
        }
    }

    // Spawn venom at the player's position
    void SpawnVenomAtPlayerPosition()
    {
        if (venomPrefab != null && targetTransform != null)
        {
            Vector3 spawnPosition = targetTransform.position; // Get the player's position
            spawnPosition.y = 0.5f; // Ensure venom spawns on the ground
            GameObject venom = Instantiate(venomPrefab, spawnPosition, Quaternion.identity); // Instantiate venom
            Destroy(venom, venomLifetime); // Destroy venom after the specified lifetime
            Debug.Log("Venom spawned at: " + spawnPosition);
        }
    }
}
