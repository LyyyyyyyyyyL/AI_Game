using UnityEngine;
using UnityEngine.AI;

public class AttackBehaviorSpitterObject : BehaviorNode
{
    // ��Һ����ʱ��׷�����ͼ��
    private float lastVenomSpawnTime = 0f; // �ϴ����ɶ�Һ��ʱ��
    public float venomSpawnInterval = 2f; // ��Һ���ɵ�ʱ�������룩
    public GameObject enemy;      // �������˶���
    public GameObject enemyArm;   // ���˵ĸ첲
    public float rotationSpeed;   // �ֱ���ת�ٶȣ���λ����/��
    public int damage = 10;       // ÿ�οۼ���Ѫ��

    private Transform targetTransform;  // �洢Ŀ�� Transform
    private NavMeshAgent navMeshAgent;  // ���˵� NavMeshAgent ���
    private PlayerHealth playerHealth; // �洢��ҵ�Ѫ���ű�
    //private bool isPlayerInTrigger = true; // �������Ƿ��ڴ�����Χ��
    private float lastCheckTime = 0f; // �ϴμ���ʱ��
    public float checkInterval = 1f; // ÿ����һ��

    // �����ֶ�
    private enemyIsTraggered enemyTriggerScript;  // ������ȡ enemyIsTraggered �ű�

    // ��Һ�������
    public GameObject venomPrefab;    // ��Һ����Ԥ����
    public float venomLifetime = 5f;  // ��Һ����Ĵ��ʱ��

    void Start()
    {
        Debug.Log("get into the attack");

        // ��ȡ NavMeshAgent ���
        navMeshAgent = enemy.GetComponent<NavMeshAgent>();
        if (navMeshAgent == null)
        {
            Debug.LogError("Enemy does not have a NavMeshAgent component!");
        }

        // ��ȡ enemyIsTraggered �ű�
        enemyTriggerScript = enemy.GetComponent<enemyIsTraggered>();
        if (enemyTriggerScript == null)
        {
            Debug.LogError("Enemy does not have the enemyIsTraggered component!");
        }

        // ����Ŀ�����
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
        // ȷ�����ˡ������ֱۺ� NavMeshAgent ����
        if (enemy == null || enemyArm == null || navMeshAgent == null || targetTransform == null || playerHealth == null || enemyTriggerScript == null || venomPrefab == null)
        {
            Debug.LogError("Missing required components!");
            return false;
        }

        // ��ȡ enemyIsTraggered �ű��� isInPlayerInAttackTrigger ֵ
        bool isPlayerInAttackTrigger = enemyTriggerScript.isInPlayerInAttackTrigger;

        // ��ת�ֱ�
        RotateArmOnXAxis();

        // ÿ����һ��
        if (Time.time - lastCheckTime >= checkInterval)
        {
            lastCheckTime = Time.time;

            // �������ڹ�����Χ��
            if (isPlayerInAttackTrigger)
            {
                // �ۼ����Ѫ��
                playerHealth.TakeDamage(damage);
                Debug.Log("Player hit! Remaining health: " + playerHealth.currentHealth);

                // ��鶾Һ���ɵ�ʱ����
                if (Time.time - lastVenomSpawnTime >= venomSpawnInterval)
                {
                    SpawnVenomAtPlayerPosition();
                    lastVenomSpawnTime = Time.time; // �����ϴ����ɶ�Һ��ʱ��
                }
            }
            else
            {
                // ��Ҳ��ڷ�Χ�ڣ��л���׷����Ϊ
                Debug.Log("return false");
                return false;
            }
        }

        // ���� NavMeshAgent ��Ŀ��λ��
        navMeshAgent.SetDestination(targetTransform.position);

        return true; // ��ʾ��Ϊ��������
    }

    // �� X ����ת�ķ���
    void RotateArmOnXAxis()
    {
        if (enemyArm != null)
        {
            enemyArm.transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime, Space.Self);
        }
    }

    // ����ҽ������ɶ�Һ����
    void SpawnVenomAtPlayerPosition()
    {
        if (venomPrefab != null && targetTransform != null)
        {
            Vector3 spawnPosition = targetTransform.position; // ��ȡ���λ��
            spawnPosition.y = 0.5f; // ȷ����Һ���������ڵ���
            GameObject venom = Instantiate(venomPrefab, spawnPosition, Quaternion.identity); // ʵ������Һ
            Destroy(venom, venomLifetime); // ��Һ������ָ��ʱ�������
            Debug.Log("Venom spawned at: " + spawnPosition);
        }
    }
}

