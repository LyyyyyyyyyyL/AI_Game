using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public int healAmount = 20; // 血包回复的血量

    void OnTriggerEnter(Collider other)
    {
        // 检查碰到的对象是否是玩家，并且是否有 PlayerHealth 组件
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            // 判断玩家当前血量是否小于最大血量
            if (playerHealth.currentHealth < playerHealth.maxHealth)
            {
                // 调用玩家的加血函数
                playerHealth.AddHealth(healAmount);

                // 删除当前血包
                Destroy(gameObject);
            }
        }
    }
}
