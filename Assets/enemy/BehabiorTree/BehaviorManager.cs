using UnityEngine;

public class BehaviorManager : MonoBehaviour
{
    private BehaviorNode currentBehavior; // ��ǰ��Ϊ�ڵ�

    [SerializeField] private BehaviorNode patrolBehavior; // Ѳ����Ϊ
    [SerializeField] private BehaviorNode runBehavior;    // ������Ϊ

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
        // ÿ֡���е�ǰ��Ϊ
        currentBehavior?.Run();  // ���õ�ǰ��Ϊ��Run()����
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
