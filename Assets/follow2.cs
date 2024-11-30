using UnityEngine;
using UnityEngine.AI;

public class HealingTeammate : MonoBehaviour
{
    public Transform player; // 玩家位置
    public float healingAmount = 10f; // 每次回血的量
    public float healInterval = 1f; // 回复间隔时间
    public float healingRange = 5f; // 回复的最大距离

    private NavMeshAgent agent;
    private float timeSinceLastHeal = 0f; // 上次回复的时间
    private float remainingLife = 100f; // 机器人生命值
    private bool isHealingCompleted = false; // 判断是否完成了回血任务

    // 定义状态枚举
    private enum State
    {
        Follow,    // 跟随玩家
        Heal,      // 给玩家回血
        Dying      // 自我销毁
    }

    private State currentState; // 当前状态

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // 初始状态为跟随
        currentState = State.Follow;

        // 检查玩家是否分配
        if (player == null)
        {
            Debug.LogError("Player not assigned! Ensure there is a player object in the scene.");
        }
    }

    void Update()
    {
        // 检测当前状态并执行对应逻辑
        switch (currentState)
        {
            case State.Follow:
                HandleFollowState();
                break;

            case State.Heal:
                HandleHealState();
                break;

            case State.Dying:
                HandleDyingState();
                break;
        }

        // 更新时间
        timeSinceLastHeal += Time.deltaTime;
    }

    // 跟随状态处理
    void HandleFollowState()
    {
        if (agent != null && agent.isOnNavMesh && player != null)
        {
            agent.SetDestination(player.position); // 跟随玩家

            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // 如果玩家血量不足且在回血范围内，切换到回血状态
                if (playerHealth.currentHealth < playerHealth.maxHealth &&
                    Vector3.Distance(transform.position, player.position) <= healingRange)
                {
                    currentState = State.Heal;
                }
            }
            else
            {
                Debug.LogWarning("PlayerHealth component not found!");
            }
        }
        else
        {
            Debug.LogWarning("NavMeshAgent or Player is not assigned, or NavMesh is invalid.");
        }
    }

    // 回血状态处理
    void HandleHealState()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // 如果玩家离开回血范围，切换回跟随状态
            if (distanceToPlayer > healingRange)
            {
                currentState = State.Follow;
                return;
            }

            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // 如果玩家满血，切换回跟随状态
                if (playerHealth.currentHealth >= playerHealth.maxHealth)
                {
                    currentState = State.Follow;
                    return;
                }

                // 如果可以回血，执行回血逻辑
                if (timeSinceLastHeal >= healInterval)
                {
                    HealPlayer(playerHealth);
                    timeSinceLastHeal = 0f; // 重置回血间隔
                }

                // 如果机器人生命值耗尽，进入死亡状态
                if (isHealingCompleted)
                {
                    currentState = State.Dying;
                }
            }
            else
            {
                Debug.LogWarning("PlayerHealth component not found!");
                currentState = State.Follow;
            }
        }
        else
        {
            Debug.LogWarning("Player is not assigned.");
            currentState = State.Follow;
        }
    }

    // 机器人死亡逻辑
    void HandleDyingState()
    {
        Debug.Log("Healing robot has completed its task and is self-destructing.");
        Destroy(gameObject); // 销毁机器人
    }

    // 执行回血逻辑
    void HealPlayer(PlayerHealth playerHealth)
    {
        float missingHealth = playerHealth.maxHealth - playerHealth.currentHealth;
        if (missingHealth > 0)
        {
            // 计算本次实际回复的血量
            float healAmount = Mathf.Min(healingAmount, missingHealth);
            playerHealth.AddHealth((int)healAmount); // 调用 PlayerHealth 的回血方法

            remainingLife -= healAmount; // 消耗机器人的生命值
            Debug.Log($"Player healed for {healAmount}. Robot remaining life: {remainingLife}");

            // 如果机器人生命值用完，标记回血完成
            if (remainingLife <= 0)
            {
                isHealingCompleted = true;
            }
        }
        else
        {
            Debug.Log("Player is already at full health. Healing task complete.");
            isHealingCompleted = true; // 玩家满血则标记完成任务
        }
    }
}
