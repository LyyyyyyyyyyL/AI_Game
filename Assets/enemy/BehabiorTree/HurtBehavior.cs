using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtBehavior : BehaviorNode
{
    public Material originalMaterial;  // Original material
    public Material hurtMaterial;      // Material when injured
    private Renderer enemyRenderer;    // Renderer used to change the material
    public GameObject enemyBody;      // Enemy body (part to change color)
    public GameObject enemy;          // The enemy object itself

    private enemyIsTraggered enemyTriggerScript;

    // Start is called before the first frame update
    void Start()
    {
        // Get the enemy's enemyIsTraggered script
        enemyTriggerScript = enemy.GetComponent<enemyIsTraggered>();
        // Get the enemy's Renderer to change the color
        enemyRenderer = enemyBody.GetComponent<Renderer>();
        if (enemyRenderer == null)
        {
            Debug.LogError("Enemy does not have a Renderer component!");
        }
        if (originalMaterial == null)
        {
            Debug.LogError("Original material is not assigned!");
        }

        // Check if the hurt material is assigned
        if (hurtMaterial == null)
        {
            Debug.LogError("Hurt material is not assigned!");
        }
        if (enemyTriggerScript == null)
        {
            Debug.LogError("Enemy does not have the enemyIsTraggered component!");
        }
    }

    // Update is called once per frame
    public override bool Run()
    {
        Debug.Log("isInBulletTrigger set to" + enemyTriggerScript.isInBulletTrigger);
        // You can apply additional force or logic here if needed

        // If the enemy's renderer and hurt material are not null
        if (enemyRenderer != null && hurtMaterial != null)
        {
            enemyRenderer.material = hurtMaterial;
            Debug.Log("Enemy material changed to hurt material.");

            // Start a coroutine to restore the original material after a delay
            StartCoroutine(RestoreMaterialAfterDelay(0.1f)); // Restore material after 0.1 seconds
        }

        // Set isInBulletTrigger to false after processing
        enemyTriggerScript.isInBulletTrigger = false;

        return false; // Behavior is complete
    }

    // Coroutine to restore the original material after a delay
    private IEnumerator RestoreMaterialAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified time
        if (enemyRenderer != null && originalMaterial != null)
        {
            enemyRenderer.material = originalMaterial;
            Debug.Log("Enemy material restored to original.");
        }
    }
}
