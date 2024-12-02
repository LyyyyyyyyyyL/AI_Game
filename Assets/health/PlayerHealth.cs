using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;  // Maximum health
    public float currentHealth;     // Current health
    public Slider playerHealthSlider; // Player health slider
    public Image fillImage; // Image to modify health bar color

    private Coroutine damageCoroutine; // Coroutine to store damage handling for stopping purposes

    void Start()
    {
        // Initialize health
        currentHealth = maxHealth;
        // Set health slider's maximum and initial values
        playerHealthSlider.maxValue = maxHealth;
        playerHealthSlider.value = currentHealth;

        // Get the Image component of the health slider's fill area
        fillImage = playerHealthSlider.fillRect.GetComponent<Image>();
    }

    void Update()
    {
        // Update the slider value according to current health
        playerHealthSlider.value = currentHealth;

        // Change health bar color based on current health
        if (currentHealth <= maxHealth / 2f)
        {
            fillImage.color = Color.red;
        }
        else
        {
            fillImage.color = Color.white;  // Default color
        }

        // Check if health is 0, and end the game
        if (currentHealth <= 0f)
        {
            PlayerDie();
        }
    }

    // Method to take damage
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);  // Ensure health does not drop below 0
    }

    // Method to add health
    public void AddHealth(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Ensure health does not exceed the maximum value
    }

    // Method to handle player death
    void PlayerDie()
    {
        Debug.Log("Game Over");

        // Ensure time flow (prevent pause state affecting scene switching)
        Time.timeScale = 1;

        // Switch to the GameOverScene
        SceneManager.LoadScene("GameOverScene");
    }
}
