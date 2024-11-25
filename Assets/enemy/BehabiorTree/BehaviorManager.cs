using UnityEngine;

public class BehaviorManager : MonoBehaviour
{
    private BehaviorNode currentBehavior; // 当前行为节点

    [SerializeField] private BehaviorNode patrolBehavior; // 巡逻行为
    [SerializeField] private BehaviorNode runBehavior;    // 奔跑行为
    [SerializeField] private BehaviorNode dieBehavior;    // 奔跑行为


    [SerializeField] private EnemyHealth EnemyHealth;

    void Start()
    {
        // 初始化为巡逻行为
        if (patrolBehavior != null)
        {
            currentBehavior = patrolBehavior;
        }

    }

    void Update()
    {
        Debug.Log("Enemy health is "+EnemyHealth.currentHealth);
        // 每帧运行当前行为
        currentBehavior?.Run();  // 调用当前行为的Run()方法

        if (EnemyHealth != null && EnemyHealth.currentHealth <= 0)
        {
            // 切换到死亡行为
            if (dieBehavior != null)
            {
                currentBehavior = dieBehavior;
                Debug.Log("Enemy health is 0. Switching to DieBehavior.");
                return; // 防止继续运行当前行为
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        // 检查碰撞的物体是否有标签"Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            // 碰撞检测后切换到奔跑行为
            Debug.Log("Player detected! Switching to RunBehavior.");
            if (runBehavior != null)
            {
                currentBehavior = runBehavior;
            }
        }
    }
}
