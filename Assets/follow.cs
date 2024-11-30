using UnityEngine;
using UnityEngine.AI;

public class Follow : MonoBehaviour
{
    public Transform player;       // 玩家位置
    public float detectionRange = 15f; // 队友与敌人间的检测距离
    public float shootingRange = 10f;  // 队友的射击范围
    public float shootInterval = 1f;   // 射击的间隔时间
    public float maxDistanceFromPlayer = 20f;  // 队友与玩家之间的最大允许距离
    public float normalSpeed = 3.5f;  // 正常的移动速度
    public float increasedSpeed = 10.5f; // 离玩家过远时的三倍速度

    private float timeSinceLastShot = 0f; // 上次开枪时间
    private TeammateGun gun;              // 队友的枪

    private Transform nearestEnemy; // 最近的敌人
    private NavMeshAgent agent;

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

        // 获取TeammateGun脚本
        gun = GetComponentInChildren<TeammateGun>();
        if (gun == null)
        {
            Debug.LogWarning("No TeammateGun component found.");
        }

        agent.speed = normalSpeed;  // 初始化时设定正常速度
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
            agent.baseOffset = 1f; // 设置 NavMeshAgent 的基准高度
            agent.SetDestination(player.position); // 跟随玩家

            // 查找最近的敌人
            FindNearestEnemy();

            // 检查是否需要切换到攻击状态
            if (nearestEnemy != null && Vector3.Distance(transform.position, nearestEnemy.position) <= detectionRange)
            {
                currentState = State.Attack; // 最近的敌人进入检测范围，切换到攻击状态
            }

            // 检查与玩家的距离，如果太远，增加移动速度
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer > maxDistanceFromPlayer)
            {
                // 如果距离过远，三倍速度寻找玩家
                agent.speed = increasedSpeed;
            }
            else
            {
                // 恢复正常速度
                agent.speed = normalSpeed;
            }
        }
        else
        {
            Debug.LogWarning("NavMeshAgent or Player is not assigned, or NavMesh is invalid.");
        }
    }

    // 查找最近的敌人
    void FindNearestEnemy()
    {
        // 查找所有敌人（假设敌人有 "Enemy" 标签）
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform closestEnemy = null;
        float minDistance = float.MaxValue;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance && distance <= detectionRange)
            {
                minDistance = distance;
                closestEnemy = enemy.transform;
            }
        }

        nearestEnemy = closestEnemy;
    }

    // 攻击状态处理
    void HandleAttackState()
    {
        if (nearestEnemy != null)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, nearestEnemy.position);
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // 如果敌人离开检测范围，切换回跟随状态
            if (distanceToEnemy > detectionRange)
            {
                currentState = State.Follow;
                return;
            }

            // 如果与玩家的距离太远，停止攻击并寻找玩家
            if (distanceToPlayer > maxDistanceFromPlayer)
            {
                currentState = State.Follow;
                return;
            }

            // 让队友面向敌人
            FaceEnemy();

            // 如果敌人在射程范围内，尝试射击
            if (distanceToEnemy <= shootingRange)
            {
                if (timeSinceLastShot >= shootInterval && gun != null)
                {
                    gun.FireBullet();  // 自动开枪
                    timeSinceLastShot = 0f;  // 重置射击间隔
                }
            }
            else
            {
                // 如果敌人不在射程内，靠近敌人
                agent.SetDestination(nearestEnemy.position);
            }
        }
        else
        {
            // 如果没有找到敌人，切换回跟随状态
            currentState = State.Follow;
        }
    }

    // 使队友面向敌人
    void FaceEnemy()
    {
        Vector3 directionToEnemy = nearestEnemy.position - transform.position;
        directionToEnemy.y = 0f; // 忽略y轴的偏差，确保只在水平面上旋转
        Quaternion targetRotation = Quaternion.LookRotation(directionToEnemy);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f); // 平滑旋转
    }
}
