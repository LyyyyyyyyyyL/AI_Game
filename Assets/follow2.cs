using UnityEngine;
using UnityEngine.AI;

public class TeammateAI2 : MonoBehaviour
{
    public Transform player;  // 玩家位置
    public Transform enemy;   // 敌人位置
    private NavMeshAgent agent;

    public float detectionRange = 10f;  // 距离玩家的检测范围
    public float shootingRange = 5f;   // 开枪的最大距离
    public float shootInterval = 1f;   // 开枪间隔时间

    private float timeSinceLastShot = 0f; // 上次开枪时间

    // 定义状态枚举
    private enum State
    {
        Idle,      // 闲置状态
        Follow,    // 跟随玩家
        Attack     // 攻击敌人
    }

    private State currentState; // 当前状态

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // 动态查找敌人（假设敌人有 "Enemy" 标签）
        if (enemy == null)
        {
            GameObject enemyObject = GameObject.FindWithTag("Enemy");
            if (enemyObject != null)
            {
                enemy = enemyObject.transform;
            }
            else
            {
                Debug.LogError("Enemy not found in the scene! Ensure there is a GameObject with the tag 'Enemy'.");
            }
        }

        currentState = State.Idle; // 初始化为闲置状态
    }

    void Update()
    {
        // 检测当前状态并执行对应逻辑
        switch (currentState)
        {
            case State.Idle:
                HandleIdleState();
                break;

            case State.Follow:
                HandleFollowState();
                break;

            case State.Attack:
                HandleAttackState();
                break;
        }

        // 更新时间
        timeSinceLastShot += Time.deltaTime;
    }

    // 闲置状态处理
    void HandleIdleState()
    {
        Debug.Log("Teammate is idle.");
        if (player != null)
        {
            // 如果玩家存在，切换到跟随状态
            currentState = State.Follow;
        }
    }

    // 跟随状态处理
    void HandleFollowState()
    {
        if (agent != null && agent.isOnNavMesh && player != null)
        {
            agent.SetDestination(player.position); // 跟随玩家

            // 检查是否需要切换到攻击状态
            if (enemy != null && Vector3.Distance(transform.position, enemy.position) <= detectionRange)
            {
                currentState = State.Attack; // 敌人进入检测范围，切换到攻击状态
            }
        }
        else
        {
            Debug.LogWarning("NavMeshAgent or Player is not assigned, or NavMesh is invalid.");
            Debug.LogWarning(player);
        }
    }

    // 攻击状态处理
    void HandleAttackState()
    {
        if (enemy != null)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.position);

            // 如果敌人离开检测范围，切换回跟随状态
            if (distanceToEnemy > detectionRange)
            {
                currentState = State.Follow;
                return;
            }

            // 如果敌人在射程范围内，尝试射击
            if (distanceToEnemy <= shootingRange && CanSeeEnemy())
            {
                if (timeSinceLastShot >= shootInterval)
                {
                    ShootAtEnemy();
                    timeSinceLastShot = 0f; // 重置开枪间隔
                }
            }
            else
            {
                // 如果敌人不在射程内，靠近敌人
                agent.SetDestination(enemy.position);
            }
        }
        else
        {
            // 如果敌人丢失，切换回跟随状态
            currentState = State.Follow;
        }
    }

    // 检测是否能看到敌人
    bool CanSeeEnemy()
    {
        RaycastHit hit;
        Vector3 directionToEnemy = enemy.position - transform.position;

        // 射线检测
        if (Physics.Raycast(transform.position, directionToEnemy, out hit, detectionRange))
        {
            if (hit.collider != null && hit.collider.transform == enemy)
            {
                return true; // 射线命中敌人
            }
        }
        return false;
    }

    // 执行开枪逻辑
    void ShootAtEnemy()
    {
        Debug.Log("Shooting at enemy!");

        // 获取枪械脚本（确保枪械是作为子物体挂在队友上）
        Gun gun = GetComponentInChildren<Gun>();
        if (gun != null)
        {
            gun.FireBullet(); // 调用实际的开枪方法
        }
        else
        {
            Debug.LogWarning("Gun component not found!");
        }
    }
}
