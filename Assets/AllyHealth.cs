using UnityEngine;
using UnityEngine.UI;

public class AllyHealth : MonoBehaviour
{
    public Slider healthSlider1;  // 绑定血条 Slider
    public float maxHealth = 100f;  // 最大血量
    public float currentHealth;  // 当前血量
    public Image fillImage;       // 血条的填充部分
    public GameObject deathEffect; // 可选：死亡效果的预制体
    public bool isDead = false;   // 是否已经死亡

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider1.maxValue = maxHealth;
        healthSlider1.value = currentHealth;

        // 获取血条的填充部分Image
        fillImage = healthSlider1.fillRect.GetComponent<Image>();
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return; // 如果已经死亡，不再处理伤害

        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        healthSlider1.value = currentHealth;
        Debug.Log("Hitted. Reducing HP. TakeDamage working");

        if (currentHealth == 0)
        {
            Die(); // 调用死亡方法
        }
    }

    void Die()
    {
        if (isDead) return; // 防止多次触发死亡逻辑

        isDead = true;
        Debug.Log("Ally has died!");

        // 可选：播放死亡效果
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        // 可选：禁用角色或执行其他清理逻辑
        gameObject.SetActive(false); // 暂时禁用对象，模拟死亡效果

        // 如果有更多逻辑，例如通知队友或玩家，可以在这里扩展
    }
}
