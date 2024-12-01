using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public enum State
    {
        Patrolling,    // Ѳ��״̬
        Chasing,       // ׷�����״̬
        Attacking,     // ����״̬
        Fleeing        // ����״̬
    }

    public State currentState = State.Patrolling;

    public Transform[] patrolPoints;  // Ѳ�ߵ�
    private int currentPatrolIndex = 0;

    public float detectionRange = 15f;    // ��ҿɱ����ֵķ�Χ
    public float attackRange = 10f;       // ��ǹ�ľ���
    public float fleeRange = 5f;          // ��ҹ���ʱ�����ܾ���

    private Transform player;     // ��� Transform
    private NavMeshAgent navMeshAgent; // ���ڿ��Ƶ����ƶ�

    public TeammateGun teammateGun; // ������ǹ�Ľű���������������ű���
    public float fleeSpeed = 5f;   // ����ʱ���ٶ�

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;  // ��ȡ��ҵ� Transform
        navMeshAgent = GetComponent<NavMeshAgent>();

        // ����Ĭ��Ѳ�ߵ�
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
        // ��������ҵľ���
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= detectionRange)
        {
            // ����׷��״̬
            currentState = State.Chasing;
            return;
        }

        // �����Ҳ���׷�ٷ�Χ������Ѳ��
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
            // ���빥��״̬
            currentState = State.Attacking;
        }
        else if (distanceToPlayer > detectionRange)
        {
            // ���Զ�룬���½���Ѳ��״̬
            currentState = State.Patrolling;
        }
        else
        {
            // ����׷�����
            navMeshAgent.destination = player.position;
        }

        if (distanceToPlayer <= fleeRange)
        {
            // ��ҹ�������������״̬
            currentState = State.Fleeing;
        }
    }

    private void AttackPlayer()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer > attackRange)
        {
            // ��������Զ���л���׷��״̬
            currentState = State.Chasing;
        }
        else
        {
            // ������ڣ�ִ�п�ǹ
            if (teammateGun != null)
            {
                teammateGun.FireBullet(); // ��ǹ
            }
        }
    }

    private void Flee()
    {
        // ��ȡ������෴�ķ���
        Vector3 directionAwayFromPlayer = transform.position - player.position;
        Vector3 fleeDestination = transform.position + directionAwayFromPlayer.normalized * fleeSpeed;

        // ��������Ŀ�ĵ�
        navMeshAgent.destination = fleeDestination;

        // �����Ҿ����Զ��ֹͣ����
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);
        if (distanceToPlayer > fleeRange)
        {
            currentState = State.Patrolling;
        }
    }
}
