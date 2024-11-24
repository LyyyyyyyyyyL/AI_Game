using UnityEngine;

public class BehaviorManager : MonoBehaviour
{
    private BehaviorNode currentBehavior; // 当前行为节点

    [SerializeField] private BehaviorNode patrolBehavior; // 巡逻行为
    [SerializeField] private BehaviorNode runBehavior;    // 奔跑行为

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
        // 每帧运行当前行为
        currentBehavior?.Run();  // 调用当前行为的Run()方法
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
