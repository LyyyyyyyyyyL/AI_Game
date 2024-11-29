using UnityEngine;
using UnityEngine.AI;

public class TeammateAI2 : MonoBehaviour
{
    public Transform player;  // ���λ��
    public Transform enemy;   // ����λ��
    private NavMeshAgent agent;

    public float detectionRange = 10f;  // ������ҵļ�ⷶΧ
    public float shootingRange = 5f;   // ��ǹ��������
    public float shootInterval = 1f;   // ��ǹ���ʱ��

    private float timeSinceLastShot = 0f; // �ϴο�ǹʱ��

    // ����״̬ö��
    private enum State
    {
        Idle,      // ����״̬
        Follow,    // �������
        Attack     // ��������
    }

    private State currentState; // ��ǰ״̬

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // ��̬���ҵ��ˣ���������� "Enemy" ��ǩ��
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

        currentState = State.Idle; // ��ʼ��Ϊ����״̬
    }

    void Update()
    {
        // ��⵱ǰ״̬��ִ�ж�Ӧ�߼�
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

        // ����ʱ��
        timeSinceLastShot += Time.deltaTime;
    }

    // ����״̬����
    void HandleIdleState()
    {
        Debug.Log("Teammate is idle.");
        if (player != null)
        {
            // �����Ҵ��ڣ��л�������״̬
            currentState = State.Follow;
        }
    }

    // ����״̬����
    void HandleFollowState()
    {
        if (agent != null && agent.isOnNavMesh && player != null)
        {
            agent.SetDestination(player.position); // �������

            // ����Ƿ���Ҫ�л�������״̬
            if (enemy != null && Vector3.Distance(transform.position, enemy.position) <= detectionRange)
            {
                currentState = State.Attack; // ���˽����ⷶΧ���л�������״̬
            }
        }
        else
        {
            Debug.LogWarning("NavMeshAgent or Player is not assigned, or NavMesh is invalid.");
            Debug.LogWarning(player);
        }
    }

    // ����״̬����
    void HandleAttackState()
    {
        if (enemy != null)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.position);

            // ��������뿪��ⷶΧ���л��ظ���״̬
            if (distanceToEnemy > detectionRange)
            {
                currentState = State.Follow;
                return;
            }

            // �����������̷�Χ�ڣ��������
            if (distanceToEnemy <= shootingRange && CanSeeEnemy())
            {
                if (timeSinceLastShot >= shootInterval)
                {
                    ShootAtEnemy();
                    timeSinceLastShot = 0f; // ���ÿ�ǹ���
                }
            }
            else
            {
                // ������˲�������ڣ���������
                agent.SetDestination(enemy.position);
            }
        }
        else
        {
            // ������˶�ʧ���л��ظ���״̬
            currentState = State.Follow;
        }
    }

    // ����Ƿ��ܿ�������
    bool CanSeeEnemy()
    {
        RaycastHit hit;
        Vector3 directionToEnemy = enemy.position - transform.position;

        // ���߼��
        if (Physics.Raycast(transform.position, directionToEnemy, out hit, detectionRange))
        {
            if (hit.collider != null && hit.collider.transform == enemy)
            {
                return true; // �������е���
            }
        }
        return false;
    }

    // ִ�п�ǹ�߼�
    void ShootAtEnemy()
    {
        Debug.Log("Shooting at enemy!");

        // ��ȡǹе�ű���ȷ��ǹе����Ϊ��������ڶ����ϣ�
        Gun gun = GetComponentInChildren<Gun>();
        if (gun != null)
        {
            gun.FireBullet(); // ����ʵ�ʵĿ�ǹ����
        }
        else
        {
            Debug.LogWarning("Gun component not found!");
        }
    }
}
