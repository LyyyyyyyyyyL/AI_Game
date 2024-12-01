using UnityEngine;

public class BehaviorManager1 : MonoBehaviour
{
    private BehaviorNode currentBehavior; // 当前行为节点
    private bool isCooldown = false;      // 冲撞行为的冷却状态

    [SerializeField] private BehaviorNode patrolBehavior; // 巡逻
    [SerializeField] private BehaviorNode chargeBehavior; // 冲撞

    [SerializeField] private Transform player;            // 玩家对象
    [SerializeField] private float detectionRange = 10f;  // 检测范围

    void Start()
    {
        currentBehavior = patrolBehavior != null ? patrolBehavior : chargeBehavior;
    }

    void Update()
    {
        if (!isCooldown && IsPlayerInDetectionRange())
        {
            currentBehavior = chargeBehavior;
        }

        if (currentBehavior != null && !isCooldown)
        {
            bool taskCompleted = currentBehavior.Run();
            if (!taskCompleted)
            {
                SwitchToCooldown();
            }
        }

        if (isCooldown)
        {
            isCooldown = false;
            currentBehavior = patrolBehavior;
        }
    }

    private bool IsPlayerInDetectionRange()
    {
        if (player == null) return false;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        return distanceToPlayer <= detectionRange;
    }

    private void SwitchToCooldown()
    {
        if (currentBehavior == chargeBehavior)
        {
            ChargeBehavior charge = currentBehavior as ChargeBehavior;
            if (charge != null) charge.ResetCharge();

            Debug.Log("Charge complete. Entering cooldown.");
            isCooldown = true; // 冷却状态
        }
    }
}
