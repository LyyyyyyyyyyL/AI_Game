using UnityEngine;
using UnityEngine.AI;

public class RunBehavior : BehaviorNode
{
    public GameObject enemy; // 需要移动的敌人
    public float runSpeed = 5f; // 备用追赶速度
    public float rotationSpeed = 5f; // 备用旋转速度
    private Transform enemyTransform; // 缓存敌人的 Transform
    private Transform playerTransform; // 缓存玩家的 Transform
    private NavMeshAgent navMeshAgent; // 自动寻路组件

    void Start()
    {
        // 获取敌人的 Transform 和 NavMeshAgent
        if (enemy != null)
        {
            enemyTransform = enemy.transform;
            navMeshAgent = enemy.GetComponent<NavMeshAgent>();
            if (navMeshAgent == null)
            {
                Debug.LogError("NavMeshAgent component is missing from the enemy GameObject!");
            }
        }
        else
        {
            Debug.LogError("Enemy GameObject not assigned!");
        }

        // 尝试获取玩家的 Transform
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (playerTransform == null)
        {
            Debug.LogError("Player not found! Make sure the player has the 'Player' tag.");
        }

        Debug.Log("Run Behavior initialized.");
    }

    public override bool Run()
    {
        // 检查必要条件
        if (enemyTransform == null)
        {
            Debug.LogError("Enemy Transform is not assigned!");
            return false;
        }

        if (playerTransform == null)
        {
            Debug.LogWarning("Player not found, enemy cannot run towards player.");
            return false;
        }

        if (navMeshAgent != null)
        {
            // 使用 NavMeshAgent 自动寻路
            navMeshAgent.speed = runSpeed;
            navMeshAgent.angularSpeed = rotationSpeed * 100f; // 转换为角速度
            navMeshAgent.destination = playerTransform.position;

            // 返回状态，根据 NavMeshAgent 是否正在移动确定运行成功
            return !navMeshAgent.isStopped;
        }
        else
        {
            // 如果 NavMeshAgent 不可用，使用备用逻辑
            Vector3 directionToPlayer = playerTransform.position - enemyTransform.position;

            // 移动和旋转
            enemyTransform.Translate(directionToPlayer.normalized * runSpeed * Time.deltaTime, Space.World);
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            enemyTransform.rotation = Quaternion.Slerp(enemyTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            return true;
        }
    }
}
