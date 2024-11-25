using UnityEngine;

public class BehaviorManager : MonoBehaviour
{
    private BehaviorNode currentBehavior; // ��ǰ��Ϊ�ڵ�

    [SerializeField] private BehaviorNode patrolBehavior; // Ѳ����Ϊ
    [SerializeField] private BehaviorNode runBehavior;    // ������Ϊ
    [SerializeField] private BehaviorNode dieBehavior;    // ������Ϊ


    [SerializeField] private EnemyHealth EnemyHealth;

    void Start()
    {
        // ��ʼ��ΪѲ����Ϊ
        if (patrolBehavior != null)
        {
            currentBehavior = patrolBehavior;
        }

    }

    void Update()
    {
        Debug.Log("Enemy health is "+EnemyHealth.currentHealth);
        // ÿ֡���е�ǰ��Ϊ
        currentBehavior?.Run();  // ���õ�ǰ��Ϊ��Run()����

        if (EnemyHealth != null && EnemyHealth.currentHealth <= 0)
        {
            // �л���������Ϊ
            if (dieBehavior != null)
            {
                currentBehavior = dieBehavior;
                Debug.Log("Enemy health is 0. Switching to DieBehavior.");
                return; // ��ֹ�������е�ǰ��Ϊ
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        // �����ײ�������Ƿ��б�ǩ"Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            // ��ײ�����л���������Ϊ
            Debug.Log("Player detected! Switching to RunBehavior.");
            if (runBehavior != null)
            {
                currentBehavior = runBehavior;
            }
        }
    }
}
