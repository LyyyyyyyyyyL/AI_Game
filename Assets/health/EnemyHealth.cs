using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public Slider healthSlider;  // ��Ѫ�� Slider
    public float maxHealth = 100f;  // ���Ѫ��
    private float currentHealth;  // ��ǰѪ��

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
