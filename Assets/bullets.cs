using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 25f;   // 子弹伤害

    private void OnCollisionEnter(Collision collision)
    {
        // 如果碰撞的是玩家
        if (collision.gameObject.CompareTag("Player"))
        {
            // 获取玩家的 Health 组件
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            if (playerHealth != null)
            {
                // 对玩家造成伤害
                playerHealth.TakeDamage(damage);
                Debug.Log("Player hit! Damage applied: " + damage);
            }
        }

        // 销毁子弹
        Destroy(gameObject);
    }
}
