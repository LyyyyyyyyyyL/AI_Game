using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public enum State
    {
        Patrolling,    // 巡逻状态
        Chasing,       // 追踪玩家状态
        Attacking,     // 攻击状态
        Fleeing        // 逃跑状态
    }

    public State currentState = State.Patrolling;

    public Transform[] patrolPoints;  // 巡逻点
    private int currentPatrolIndex = 0;

    public float detectionRange = 15f;    // 玩家可被发现的范围
    public float attackRange = 10f;       // 开枪的距离
    public float fleeRange = 5f;          // 玩家过近时的逃跑距离

    private Transform player;     // 玩家 Transform
    private NavMeshAgent navMeshAgent; // 用于控制敌人移动

    public TeammateGun teammateGun; // 用来开枪的脚本（假设你有这个脚本）
    public float fleeSpeed = 5f;   // 逃跑时的速度

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;  // 获取玩家的 Transform
        navMeshAgent = GetComponent<NavMeshAgent>();

        // 设置默认巡逻点
        if (patrolPoints.Length > 0)
        {
            navMeshAgent.destination = patrolPoints[currentPatrolIndex].position;
        }
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Patrolling:
                Patrol();
                break;

            case State.Chasing:
                ChasePlayer();
                break;

            case State.Attacking:
                AttackPlayer();
                break;

            case State.Fleeing:
                Flee();
                break;
        }
    }

    private void Patrol()
    {
        // 检查距离玩家的距离
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= detectionRange)
        {
            // 进入追踪状态
            currentState = State.Chasing;
            return;
        }

        // 如果玩家不在追踪范围，继续巡逻
        if (Vector3.Distance(transform.position, patrolPoints[currentPatrolIndex].position) < 1f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            navMeshAgent.destination = patrolPoints[currentPatrolIndex].position;
        }
    }

    private void ChasePlayer()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= attackRange)
        {
            // 进入攻击状态
            currentState = State.Attacking;
        }
        else if (distanceToPlayer > detectionRange)
        {
            // 玩家远离，重新进入巡逻状态
            currentState = State.Patrolling;
        }
        else
        {
            // 持续追踪玩家
            navMeshAgent.destination = player.position;
        }

        if (distanceToPlayer <= fleeRange)
        {
            // 玩家过近，进入逃跑状态
            currentState = State.Fleeing;
        }
    }

    private void AttackPlayer()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer > attackRange)
        {
            // 如果距离过远，切换回追踪状态
            currentState = State.Chasing;
        }
        else
        {
            // 在射程内，执行开枪
            if (teammateGun != null)
            {
                teammateGun.FireBullet(); // 开枪
            }
        }
    }

    private void Flee()
    {
        // 获取离玩家相反的方向
        Vector3 directionAwayFromPlayer = transform.position - player.position;
        Vector3 fleeDestination = transform.position + directionAwayFromPlayer.normalized * fleeSpeed;

        // 设置逃跑目的地
        navMeshAgent.destination = fleeDestination;

        // 如果玩家距离变远，停止逃跑
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);
        if (distanceToPlayer > fleeRange)
        {
            currentState = State.Patrolling;
        }
    }
}
