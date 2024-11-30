using UnityEngine;
using UnityEngine.AI;

public class Follow : MonoBehaviour
{
    public Transform player;       // ���λ��
    public float detectionRange = 15f; // ��������˼�ļ�����
    public float shootingRange = 10f;  // ���ѵ������Χ
    public float shootInterval = 1f;   // ����ļ��ʱ��
    public float maxDistanceFromPlayer = 20f;  // ���������֮�������������
    public float normalSpeed = 3.5f;  // �������ƶ��ٶ�
    public float increasedSpeed = 10.5f; // ����ҹ�Զʱ�������ٶ�

    private float timeSinceLastShot = 0f; // �ϴο�ǹʱ��
    private TeammateGun gun;              // ���ѵ�ǹ

    private Transform nearestEnemy; // ����ĵ���
    private NavMeshAgent agent;

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

        // ��ȡTeammateGun�ű�
        gun = GetComponentInChildren<TeammateGun>();
        if (gun == null)
        {
            Debug.LogWarning("No TeammateGun component found.");
        }

        agent.speed = normalSpeed;  // ��ʼ��ʱ�趨�����ٶ�
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
            agent.baseOffset = 1f; // ���� NavMeshAgent �Ļ�׼�߶�
            agent.SetDestination(player.position); // �������

            // ��������ĵ���
            FindNearestEnemy();

            // ����Ƿ���Ҫ�л�������״̬
            if (nearestEnemy != null && Vector3.Distance(transform.position, nearestEnemy.position) <= detectionRange)
            {
                currentState = State.Attack; // ����ĵ��˽����ⷶΧ���л�������״̬
            }

            // �������ҵľ��룬���̫Զ�������ƶ��ٶ�
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer > maxDistanceFromPlayer)
            {
                // ��������Զ�������ٶ�Ѱ�����
                agent.speed = increasedSpeed;
            }
            else
            {
                // �ָ������ٶ�
                agent.speed = normalSpeed;
            }
        }
        else
        {
            Debug.LogWarning("NavMeshAgent or Player is not assigned, or NavMesh is invalid.");
        }
    }

    // ��������ĵ���
    void FindNearestEnemy()
    {
        // �������е��ˣ���������� "Enemy" ��ǩ��
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

    // ����״̬����
    void HandleAttackState()
    {
        if (nearestEnemy != null)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, nearestEnemy.position);
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // ��������뿪��ⷶΧ���л��ظ���״̬
            if (distanceToEnemy > detectionRange)
            {
                currentState = State.Follow;
                return;
            }

            // �������ҵľ���̫Զ��ֹͣ������Ѱ�����
            if (distanceToPlayer > maxDistanceFromPlayer)
            {
                currentState = State.Follow;
                return;
            }

            // �ö����������
            FaceEnemy();

            // �����������̷�Χ�ڣ��������
            if (distanceToEnemy <= shootingRange)
            {
                if (timeSinceLastShot >= shootInterval && gun != null)
                {
                    gun.FireBullet();  // �Զ���ǹ
                    timeSinceLastShot = 0f;  // ����������
                }
            }
            else
            {
                // ������˲�������ڣ���������
                agent.SetDestination(nearestEnemy.position);
            }
        }
        else
        {
            // ���û���ҵ����ˣ��л��ظ���״̬
            currentState = State.Follow;
        }
    }

    // ʹ�����������
    void FaceEnemy()
    {
        Vector3 directionToEnemy = nearestEnemy.position - transform.position;
        directionToEnemy.y = 0f; // ����y���ƫ�ȷ��ֻ��ˮƽ������ת
        Quaternion targetRotation = Quaternion.LookRotation(directionToEnemy);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f); // ƽ����ת
    }
}
