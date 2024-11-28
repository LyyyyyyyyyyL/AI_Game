using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public Slider healthSlider;  // 绑定血条 Slider
    public float maxHealth = 100f;  // 最大血量
    public float currentHealth;  // 当前血量

    // 引用 enemyIsTraggered 脚本
    public enemyIsTraggered enemyTriggerScript;

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;

        // 获取 enemyIsTraggered 脚本
        if (enemyTriggerScript == null)
        {
            enemyTriggerScript = GetComponent<enemyIsTraggered>();
            if (enemyTriggerScript == null)
            {
                Debug.LogError("Enemy does not have the enemyIsTraggered component!");
            }
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        // 更新血条
        healthSlider.value = currentHealth;
        //Debug.Log("Hitted. Reducing HP. TakeDamage working.");

        // 将 isInBulletTrigger 设置为 true
        if (enemyTriggerScript != null)
        {
            enemyTriggerScript.isInBulletTrigger = true; // 设置为 true 表示敌人被子弹击中
            Debug.Log("enemyIsTraggered.isInBulletTrigger set to true.");
        }
    }
}
