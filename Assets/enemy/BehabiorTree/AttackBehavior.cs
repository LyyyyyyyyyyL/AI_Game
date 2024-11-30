using UnityEngine;
using UnityEngine.AI;

public class AttackBehaviorObject : BehaviorNode
{
    public GameObject enemy;      // 整个敌人对象
    public GameObject enemyArm;   // 敌人的胳膊
    public float rotationSpeed;   // 手臂旋转速度，单位：度/秒
    public int damage = 10;       // 每次扣减的血量

    private Transform targetTransform;  // 存储目标 Transform
    private NavMeshAgent navMeshAgent;  // 敌人的 NavMeshAgent 组件
    private PlayerHealth playerHealth; // 存储玩家的血量脚本
    //private bool isPlayerInTrigger = true; // 标记玩家是否在触发范围内
    private float lastCheckTime = 0f; // 上次检测的时间
    public float checkInterval = 1f; // 每秒检测一次

    // 新增字段
    private enemyIsTraggered enemyTriggerScript;  // 用来获取 enemyIsTraggered 脚本

    void Start()
    {
        Debug.Log("get into the attack");

        // 获取 NavMeshAgent 组件
        navMeshAgent = enemy.GetComponent<NavMeshAgent>();
        if (navMeshAgent == null)
        {
            Debug.LogError("Enemy does not have a NavMeshAgent component!");
        }

        // 获取 enemyIsTraggered 脚本
        enemyTriggerScript = enemy.GetComponent<enemyIsTraggered>();
        if (enemyTriggerScript == null)
        {
            Debug.LogError("Enemy does not have the enemyIsTraggered component!");
        }

        // 查找目标对象
        GameObject target = GameObject.FindGameObjectWithTag("Player");
        if (target != null)
        {
            targetTransform = target.transform;
            playerHealth = target.GetComponent<PlayerHealth>();
            if (playerHealth == null)
            {
                Debug.LogError("Player does not have a PlayerHealth component!");
            }
        }
        else
        {
            Debug.LogError("Target with tag 'Player' not found!");
        }
    }

    public override bool Run()
    {
        // 确保敌人、敌人手臂和 NavMeshAgent 存在
        if (enemy == null || enemyArm == null || navMeshAgent == null || targetTransform == null || playerHealth == null || enemyTriggerScript == null)
        {
            Debug.LogError("Missing required components!");
            return false;
        }

        // 获取 enemyIsTraggered 脚本的 isInPlayerInAttackTrigger 值
        bool isPlayerInAttackTrigger = enemyTriggerScript.isInPlayerInAttackTrigger;
        //Debug.Log("isPlayerInAttackTrigger: " + isPlayerInAttackTrigger);

        // 旋转手臂
        RotateArmOnXAxis();

        // 每秒检测一次
        if (Time.time - lastCheckTime >= checkInterval)
        {
            lastCheckTime = Time.time;

            // 如果玩家在攻击范围内
            if (isPlayerInAttackTrigger)
            {
                // 扣减玩家血量
                playerHealth.TakeDamage(damage);
                Debug.Log("Player hit! Remaining health: " + playerHealth.currentHealth);
            }
            else
            {
                // 玩家不在范围内，切换到追逐行为

                Debug.Log("return false");
                return false;
            }
        }

        // 设置 NavMeshAgent 的目标位置
        navMeshAgent.SetDestination(targetTransform.position);

        return true; // 表示行为持续运行
    }

    // 绕 X 轴旋转的方法
    void RotateArmOnXAxis()
    {
        if (enemyArm != null)
        {
            enemyArm.transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime, Space.Self);
        }
    }
}
