using UnityEngine;

public class PatrolBehavior : BehaviorNode
{
    public float wanderSpeed = 5f;  // Movement speed in meters per second
    public float wanderRadius = 5f;  // Radius of wandering
    public float wanderJitter = 0.2f;  // Random jitter magnitude
    private Transform enemyTransform;  // Stores the enemy's Transform
    public GameObject enemy;  // The enemy to be controlled
    public AudioClip zombieGrowl;  // Stores the zombie growl sound
    private AudioSource audioSource;  // Used to play sound

    private Vector3 wanderTarget;  // Current wandering target

    void Start()
    {
        if (enemy != null)
        {
            enemyTransform = enemy.transform;
        }
        else
        {
            Debug.LogError("Enemy GameObject not assigned!");
        }

        // Get the AudioSource component of the enemy
        audioSource = enemy.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // If AudioSource is not found, dynamically add one
            audioSource = enemy.AddComponent<AudioSource>();
        }

        // Set the sound clip
        if (zombieGrowl != null)
        {
            audioSource.clip = zombieGrowl;
            audioSource.loop = false;  // Set not to loop
            // Play the growl sound every 10 seconds
            InvokeRepeating("PlayGrowl", 0f, 10f);
        }
        else
        {
            Debug.LogError("Zombie growl sound not assigned!");
        }

        // Initialize wander target
        wanderTarget = enemyTransform.position;
    }

    public override bool Run()
    {
        // Debugging: Print enemy current position
        //Debug.Log("Enemy Current Position: " + enemyTransform.position);

        // Generate a new wander target
        Wander();

        // Move the enemy slowly towards the target
        MoveEnemyForward();

        // Return true, indicating the patrol behavior continues
        return true;
    }

    // Generate a new wander target point
    void Wander()
    {
        // Generate a random jitter displacement vector
        Vector3 randomDisplacement = new Vector3(
            Random.Range(-1f, 1f),
            0,  // No change in the y-axis for 2D or 3D environments
            Random.Range(-1f, 1f)
        ) * wanderJitter;

        // Add jitter to the wander target
        wanderTarget += randomDisplacement;

        // Constrain the wander target to a circle
        wanderTarget = (wanderTarget - enemyTransform.position).normalized * wanderRadius + enemyTransform.position;
    }

    // Move the enemy slowly forward and face the target
    void MoveEnemyForward()
    {
        // Move the enemy towards the target position with wanderSpeed
        enemyTransform.position = Vector3.MoveTowards(enemyTransform.position, wanderTarget, wanderSpeed * Time.deltaTime);

        // Calculate the direction towards the target
        Vector3 directionToTarget = wanderTarget - enemyTransform.position;

        // Rotate the enemy if the target is not in front
        if (directionToTarget.sqrMagnitude > 0.1f)  // Avoid division by zero error
        {
            // Calculate the new rotation angle
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

            // Smoothly rotate the enemy towards the target direction
            enemyTransform.rotation = Quaternion.RotateTowards(enemyTransform.rotation, targetRotation, 360f * Time.deltaTime);
        }
    }

    // Play the zombie growl sound
    void PlayGrowl()
    {
        audioSource.Play();
    }
}
