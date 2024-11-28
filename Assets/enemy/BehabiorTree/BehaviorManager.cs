using UnityEngine;

public class BehaviorManager : MonoBehaviour
{
    private BehaviorNode currentBehavior; // ��ǰ��Ϊ�ڵ�

    [SerializeField] private BehaviorNode patrolBehavior; // Ѳ��
    [SerializeField] private BehaviorNode runBehavior;    // ׷��
    [SerializeField] private BehaviorNode hurtBehavior;   // ����
    [SerializeField] private BehaviorNode attackBehavior; // ����
    [SerializeField] private BehaviorNode dieBehavior;    // ����

    [SerializeField] private enemyIsTraggered enemyIsTraggered; // ������������״̬
    [SerializeField] private EnemyHealth EnemyHealth;           // ���������˽���

    void Start()
    {
        // ���û�д� Inspector �и�ֵ�����Դӵ��˶����в��� enemyIsTraggered ���
        if (enemyIsTraggered == null)
        {
            enemyIsTraggered = GetComponent<enemyIsTraggered>();
            if (enemyIsTraggered == null)
            {
                Debug.LogError("enemyIsTraggered component not found on the enemy object!");
            }
        }

        // ��ʼ��ΪѲ����Ϊ
        if (patrolBehavior != null)
        {
            currentBehavior = patrolBehavior;
        }
    }

    void Update()
    {
        // ÿ֡���е�ǰ��Ϊ
        if (currentBehavior != null)
        {
            
            bool taskCompleted = currentBehavior.Run();
            if (!taskCompleted)
            {
                
                // �����ǰ��Ϊ���� false�����л�����һ����Ϊ
                SwitchToNextBehavior();
            }
        }

        // �������Ѫ��Ϊ0�����л���������Ϊ
        if (EnemyHealth != null && EnemyHealth.currentHealth <= 0)
        {
            
            if (dieBehavior != null)
            {
                currentBehavior = dieBehavior;
                Debug.Log("Enemy health is 0. Switching to DieBehavior.");
                return;
            }
        }

        // �������Ƿ�������״̬
        
        if (enemyIsTraggered != null && enemyIsTraggered.isInBulletTrigger)
        {
            // ������ӵ����У��л���������Ϊ
            
            if (hurtBehavior != null)
            {
                currentBehavior = hurtBehavior;
                Debug.Log("Enemy hit by bullet! Switching to HurtBehavior.");
            }
        }
        Debug.Log("enemyIsTraggered = " + (enemyIsTraggered != null && enemyIsTraggered.isInBulletTrigger));

    }

    private void OnTriggerEnter(Collider other)
    {
        // �����봥�����������Ƿ��б�ǩ"Player"
        if (other.gameObject.CompareTag("Player") && currentBehavior != runBehavior && currentBehavior != attackBehavior && currentBehavior != hurtBehavior)
        {
            if (runBehavior != null)
            {
                Debug.Log("Player detected! Switching to RunBehavior.");
                currentBehavior = runBehavior;
            }
        }

        // ����Ƿ���빥�����������л���������Ϊ
        if (other.gameObject.CompareTag("Attack") && currentBehavior == runBehavior)
        {
            Debug.Log("Player detected! Switching to Attack.");
            currentBehavior = attackBehavior;
        }
    }

    void SwitchToNextBehavior()
    {
        // ���ݵ�ǰ��Ϊ�л���������Ϊ
        if (currentBehavior == attackBehavior)
        {
            Debug.Log("Player left AttackTrigger. Switching to RunBehavior.");
            currentBehavior = runBehavior;  // �л���׷����Ϊ
        }
        else if (currentBehavior == hurtBehavior)
        {
            Debug.Log("Player left hurtBehavior. Switching to RunBehavior.");
            currentBehavior = runBehavior;  // ���˺��л���׷����Ϊ
        }
    }
}
