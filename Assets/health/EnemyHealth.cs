using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public Slider healthSlider;  // 绑定血条 Slider
    public float maxHealth = 100f;  // 最大血量
    private float currentHealth;  // 当前血量

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;
        healthSlider.value = currentHealth;
        Debug.Log("hitted. Reducing HP.TakeDamageworking");
    }
}
