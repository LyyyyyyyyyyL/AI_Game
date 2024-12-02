using UnityEngine;

public class BehaviorManager : MonoBehaviour
{
    private BehaviorNode currentBehavior; // Current behavior node

    [SerializeField] private BehaviorNode patrolBehavior; // Patrol behavior
    [SerializeField] private BehaviorNode runBehavior;    // Run (chase) behavior
    [SerializeField] private BehaviorNode hurtBehavior;   // Hurt behavior
    [SerializeField] private BehaviorNode attackBehavior; // Attack behavior
    [SerializeField] private BehaviorNode dieBehavior;    // Die behavior

    [SerializeField] private enemyIsTraggered enemyIsTraggered; // Used to detect subcomponent status
    [SerializeField] private EnemyHealth EnemyHealth;           // Used to detect enemy health

    void Start()
    {
        // If not assigned from the Inspector, try to find the enemyIsTraggered component on the enemy object
        if (enemyIsTraggered == null)
        {
            enemyIsTraggered = GetComponent<enemyIsTraggered>();
            if (enemyIsTraggered == null)
            {
                Debug.LogError("enemyIsTraggered component not found on the enemy object!");
            }
        }

        // Initialize with patrol behavior
        if (patrolBehavior != null)
        {
            currentBehavior = patrolBehavior;
        }
    }

    void Update()
    {
        // Run the current behavior every frame
        if (currentBehavior != null)
        {
            bool taskCompleted = currentBehavior.Run();
            if (!taskCompleted)
            {
                // If the current behavior returns false, switch to the next behavior
                SwitchToNextBehavior();
            }
        }

        // If the enemy's health is 0, switch to the die behavior
        if (EnemyHealth != null && EnemyHealth.currentHealth <= 0)
        {
            if (dieBehavior != null)
            {
                currentBehavior = dieBehavior;
                Debug.Log("Enemy health is 0. Switching to DieBehavior.");
                return;
            }
        }

        // Check if the enemy is in a hurt state
        if (enemyIsTraggered != null && enemyIsTraggered.isInBulletTrigger)
        {
            // If hit by a bullet, switch to the hurt behavior
            if (hurtBehavior != null)
            {
                currentBehavior = hurtBehavior;
                Debug.Log("Enemy hit by bullet! Switching to HurtBehavior.");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is tagged as "Player"
        if (other.gameObject.CompareTag("Player") && currentBehavior != runBehavior && currentBehavior != attackBehavior && currentBehavior != hurtBehavior)
        {
            if (runBehavior != null)
            {
                Debug.Log("Player detected! Switching to RunBehavior.");
                currentBehavior = runBehavior;
            }
        }

        // Check if the object is an attack trigger and switch to attack behavior
        if (other.gameObject.CompareTag("Attack") && currentBehavior == runBehavior)
        {
            Debug.Log("Player detected! Switching to Attack.");
            currentBehavior = attackBehavior;
        }
    }

    void SwitchToNextBehavior()
    {
        // Switch to another behavior based on the current behavior
        if (currentBehavior == attackBehavior)
        {
            Debug.Log("Player left AttackTrigger. Switching to RunBehavior.");
            currentBehavior = runBehavior;  // Switch to the run (chase) behavior
        }
        else if (currentBehavior == hurtBehavior)
        {
            Debug.Log("Player left hurtBehavior. Switching to RunBehavior.");
            currentBehavior = runBehavior;  // After being hurt, switch to run (chase) behavior
        }
    }
}
