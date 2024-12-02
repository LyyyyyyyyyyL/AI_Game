using UnityEngine;
using UnityEngine.AI;

public class AttackBehaviorObject : BehaviorNode
{
    public GameObject enemy;      // The enemy object
    public GameObject enemyArm;   // The enemy's arm
    public float rotationSpeed;   // Arm rotation speed, in degrees per second
    public int damage = 10;       // Damage dealt per attack

    private Transform targetTransform;  // Stores the target Transform
    private NavMeshAgent navMeshAgent;  // The enemy's NavMeshAgent component
    private PlayerHealth playerHealth; // Stores the player's health script
    private float lastCheckTime = 0f;  // The time of the last check
    public float checkInterval = 1f;   // Time interval for checking each second

    // New field
    private enemyIsTraggered enemyTriggerScript;  // Used to get the enemyIsTraggered script

    void Start()
    {
        Debug.Log("Entering attack behavior");

        // Get the NavMeshAgent component
        navMeshAgent = enemy.GetComponent<NavMeshAgent>();
        if (navMeshAgent == null)
        {
            Debug.LogError("Enemy does not have a NavMeshAgent component!");
        }

        // Get the enemyIsTraggered script
        enemyTriggerScript = enemy.GetComponent<enemyIsTraggered>();
        if (enemyTriggerScript == null)
        {
            Debug.LogError("Enemy does not have the enemyIsTraggered component!");
        }

        // Find the target object
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
        // Ensure enemy, enemy arm, NavMeshAgent, target and player health are all present
        if (enemy == null || enemyArm == null || navMeshAgent == null || targetTransform == null || playerHealth == null || enemyTriggerScript == null)
        {
            Debug.LogError("Missing required components!");
            return false;
        }

        // Get the value of isInPlayerInAttackTrigger from enemyIsTraggered script
        bool isPlayerInAttackTrigger = enemyTriggerScript.isInPlayerInAttackTrigger;

        // Rotate the enemy's arm
        RotateArmOnXAxis();

        // Check every second
        if (Time.time - lastCheckTime >= checkInterval)
        {
            lastCheckTime = Time.time;

            // If player is within attack range
            if (isPlayerInAttackTrigger)
            {
                // Deal damage to the player
                playerHealth.TakeDamage(damage);
                Debug.Log("Player hit! Remaining health: " + playerHealth.currentHealth);
            }
            else
            {
                // If player is out of range, switch to pursuit behavior
                Debug.Log("Player out of attack range, returning false.");
                return false;
            }
        }

        // Set the destination for the NavMeshAgent (pursue the player)
        navMeshAgent.SetDestination(targetTransform.position);

        return true; // Behavior continues
    }

    // Method to rotate the arm around the X axis
    void RotateArmOnXAxis()
    {
        if (enemyArm != null)
        {
            // Rotate arm around the X axis
            enemyArm.transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime, Space.Self);
        }
    }
}
