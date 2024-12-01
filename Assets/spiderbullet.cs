using UnityEngine;

public class Bullets1 : MonoBehaviour
{
    public float damage = 25f; // 子弹造成的伤害

    private void OnCollisionEnter(Collision collision)
    {
        // 检查是否碰撞到玩家
        if (collision.gameObject.CompareTag("Player"))
        {
            // 获取玩家的 PlayerHealth 脚本并造成伤害
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                Debug.Log("Player hit by bullet! Damage dealt: " + damage);
            }

            // 销毁子弹
            Destroy(gameObject);
        }
        else
        {
            // 如果不是玩家，销毁子弹
            Destroy(gameObject);
        }
    }
}
