using UnityEngine;
using UnityEngine.AI;

public class HealingTeammate : MonoBehaviour
{
    public Transform player; // ���λ��
    public float healingAmount = 10f; // ÿ�λ�Ѫ����
    public float healInterval = 1f; // �ظ����ʱ��
    public float healingRange = 5f; // �ظ���������

    private NavMeshAgent agent;
    private float timeSinceLastHeal = 0f; // �ϴλظ���ʱ��
    private float remainingLife = 100f; // ����������ֵ
    private bool isHealingCompleted = false; // �ж��Ƿ�����˻�Ѫ����

    // ����״̬ö��
    private enum State
    {
        Follow,    // �������
        Heal,      // ����һ�Ѫ
        Dying      // ��������
    }

    private State currentState; // ��ǰ״̬

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // ��ʼ״̬Ϊ����
        currentState = State.Follow;

        // �������Ƿ����
        if (player == null)
        {
            Debug.LogError("Player not assigned! Ensure there is a player object in the scene.");
        }
    }

    void Update()
    {
        // ��⵱ǰ״̬��ִ�ж�Ӧ�߼�
        switch (currentState)
        {
            case State.Follow:
                HandleFollowState();
                break;

            case State.Heal:
                HandleHealState();
                break;

            case State.Dying:
                HandleDyingState();
                break;
        }

        // ����ʱ��
        timeSinceLastHeal += Time.deltaTime;
    }

    // ����״̬����
    void HandleFollowState()
    {
        if (agent != null && agent.isOnNavMesh && player != null)
        {
            agent.SetDestination(player.position); // �������

            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // ������Ѫ���������ڻ�Ѫ��Χ�ڣ��л�����Ѫ״̬
                if (playerHealth.currentHealth < playerHealth.maxHealth &&
                    Vector3.Distance(transform.position, player.position) <= healingRange)
                {
                    currentState = State.Heal;
                }
            }
            else
            {
                Debug.LogWarning("PlayerHealth component not found!");
            }
        }
        else
        {
            Debug.LogWarning("NavMeshAgent or Player is not assigned, or NavMesh is invalid.");
        }
    }

    // ��Ѫ״̬����
    void HandleHealState()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // �������뿪��Ѫ��Χ���л��ظ���״̬
            if (distanceToPlayer > healingRange)
            {
                currentState = State.Follow;
                return;
            }

            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // ��������Ѫ���л��ظ���״̬
                if (playerHealth.currentHealth >= playerHealth.maxHealth)
                {
                    currentState = State.Follow;
                    return;
                }

                // ������Ի�Ѫ��ִ�л�Ѫ�߼�
                if (timeSinceLastHeal >= healInterval)
                {
                    HealPlayer(playerHealth);
                    timeSinceLastHeal = 0f; // ���û�Ѫ���
                }

                // �������������ֵ�ľ�����������״̬
                if (isHealingCompleted)
                {
                    currentState = State.Dying;
                }
            }
            else
            {
                Debug.LogWarning("PlayerHealth component not found!");
                currentState = State.Follow;
            }
        }
        else
        {
            Debug.LogWarning("Player is not assigned.");
            currentState = State.Follow;
        }
    }

    // �����������߼�
    void HandleDyingState()
    {
        Debug.Log("Healing robot has completed its task and is self-destructing.");
        Destroy(gameObject); // ���ٻ�����
    }

    // ִ�л�Ѫ�߼�
    void HealPlayer(PlayerHealth playerHealth)
    {
        float missingHealth = playerHealth.maxHealth - playerHealth.currentHealth;
        if (missingHealth > 0)
        {
            // ���㱾��ʵ�ʻظ���Ѫ��
            float healAmount = Mathf.Min(healingAmount, missingHealth);
            playerHealth.AddHealth((int)healAmount); // ���� PlayerHealth �Ļ�Ѫ����

            remainingLife -= healAmount; // ���Ļ����˵�����ֵ
            Debug.Log($"Player healed for {healAmount}. Robot remaining life: {remainingLife}");

            // �������������ֵ���꣬��ǻ�Ѫ���
            if (remainingLife <= 0)
            {
                isHealingCompleted = true;
            }
        }
        else
        {
            Debug.Log("Player is already at full health. Healing task complete.");
            isHealingCompleted = true; // �����Ѫ�����������
        }
    }
}
