using UnityEngine;

public class ChargeBehavior : BehaviorNode
{
    public Transform player;             // 玩家位置
    public float chargeSpeed = 10f;      // 冲撞速度
    public float chargeForce = 10f;      // 冲击力大小
    private bool hasCharged = false;     // 是否已经完成冲撞

    private bool isPaused = false;       // 是否处于暂停状态
    private float pauseDuration = 3f;    // 停顿时间
    private float pauseTimer = 0f;       // 停顿计时器

    private float chargeDistance = 4f;   // 冲撞触发的最小距离

    public override bool Run()
    {
        if (player == null)
        {
            Debug.LogError("Player transform is not set in ChargeBehavior!");
            return false;
        }

        // 如果处于暂停状态，等待计时器结束
        if (isPaused)
        {
            pauseTimer += Time.deltaTime;
            if (pauseTimer >= pauseDuration)
            {
                isPaused = false;
                pauseTimer = 0f;
                Debug.Log("Pause over. Charger ready to attack again.");
                return false; // 冷却完成，结束当前行为
            }
            return true; // 冷却中，不执行其他动作
        }

        // 冲撞逻辑
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0; // 去除垂直方向的分量

        // 移动敌人朝向玩家
        transform.position += direction * chargeSpeed * Time.deltaTime;

        // 如果与玩家的距离小于冲撞触发距离，执行冲撞行为
        if (Vector3.Distance(transform.position, player.position) < chargeDistance)
        {
            ApplyCollisionEffect(); // 执行冲撞效果
            isPaused = true;         // 进入暂停状态
            return false;            // 结束冲撞行为
        }

        return true; // 继续冲撞，直到接触玩家
    }

    private void ApplyCollisionEffect()
    {
        // 施加冲撞效果
        Rigidbody playerRb = player.GetComponent<Rigidbody>();
        if (playerRb != null)
        {
            // 施加冲击力，让玩家位移
            Vector3 pushForce = (player.position - transform.position).normalized * chargeForce;
            playerRb.AddForce(pushForce, ForceMode.Impulse);
        }

        // 造成伤害
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(50); // 给玩家造成伤害
            Debug.Log("Player hit by Charger! Player takes 50 damage.");
        }

        hasCharged = true; // 标记为已完成冲撞
    }

    public void ResetCharge()
    {
        hasCharged = false; // 重置状态以准备下一次冲撞
    }
}
