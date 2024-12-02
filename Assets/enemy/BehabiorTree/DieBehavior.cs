using UnityEngine;

public class DieBehavior : BehaviorNode
{
    public GameObject enemy; // The enemy object that needs to fall and be destroyed
    public float destroyDelay = 2f; // Delay before destroying the object
    public Vector3 rotationAngle = new Vector3(90f, 0f, 0f); // Rotation angle when falling
    public float rotationSpeed = 2f; // Rotation speed, defines the angle rotated per second

    public override bool Run()
    {
        if (enemy != null)
        {
            Debug.Log("DieBehavior is running");

            // Start a coroutine to gradually rotate the enemy to the target angle
            Quaternion targetRotation = Quaternion.Euler(rotationAngle);
            enemy.GetComponent<MonoBehaviour>().StartCoroutine(RotateToTarget(enemy.transform, targetRotation));

            // Delay the destruction of the enemy object
            Destroy(enemy, destroyDelay);

            return true; // Behavior completed
        }
        else
        {
            Debug.LogError("Enemy GameObject is not assigned!");
            return false; // Behavior failed
        }
    }

    private System.Collections.IEnumerator RotateToTarget(Transform enemyTransform, Quaternion targetRotation)
    {
        // Rotate until the enemy reaches the target rotation
        while (Quaternion.Angle(enemyTransform.rotation, targetRotation) > 0.1f)
        {
            enemyTransform.rotation = Quaternion.RotateTowards(
                enemyTransform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
            yield return null;
        }

        // Ensure the final rotation matches the target angle
        enemyTransform.rotation = targetRotation;
    }
}
