using UnityEngine;

public class BehaviorManager : MonoBehaviour
{
    private BehaviorNode currentBehavior; // 当前行为节点

    [SerializeField] private BehaviorNode patrolBehavior; // 巡逻
    [SerializeField] private BehaviorNode runBehavior;    // 追逐
    [SerializeField] private BehaviorNode hurtBehavior;   // 受伤
    [SerializeField] private BehaviorNode attackBehavior; // 攻击
    [SerializeField] private BehaviorNode dieBehavior;    // 死亡

    [SerializeField] private enemyIsTraggered enemyIsTraggered; // 用来检测子组件状态
    [SerializeField] private EnemyHealth EnemyHealth;           // 用来检测敌人健康

    void Start()
    {
        // 如果没有从 Inspector 中赋值，则尝试从敌人对象中查找 enemyIsTraggered 组件
        if (enemyIsTraggered == null)
        {
            enemyIsTraggered = GetComponent<enemyIsTraggered>();
            if (enemyIsTraggered == null)
            {
                Debug.LogError("enemyIsTraggered component not found on the enemy object!");
            }
        }

        // 初始化为巡逻行为
        if (patrolBehavior != null)
        {
            currentBehavior = patrolBehavior;
        }
    }

    void Update()
    {
        // 每帧运行当前行为
        if (currentBehavior != null)
        {
            
            bool taskCompleted = currentBehavior.Run();
            if (!taskCompleted)
            {
                
                // 如果当前行为返回 false，则切换到下一个行为
                SwitchToNextBehavior();
            }
        }

        // 如果敌人血量为0，则切换到死亡行为
        if (EnemyHealth != null && EnemyHealth.currentHealth <= 0)
        {
            
            if (dieBehavior != null)
            {
                currentBehavior = dieBehavior;
                Debug.Log("Enemy health is 0. Switching to DieBehavior.");
                return;
            }
        }

        // 检查敌人是否处于受伤状态
        
        if (enemyIsTraggered != null && enemyIsTraggered.isInBulletTrigger)
        {
            // 如果被子弹击中，切换到受伤行为
            
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
        // 检查进入触发器的物体是否有标签"Player"
        if (other.gameObject.CompareTag("Player") && currentBehavior != runBehavior && currentBehavior != attackBehavior && currentBehavior != hurtBehavior)
        {
            if (runBehavior != null)
            {
                Debug.Log("Player detected! Switching to RunBehavior.");
                currentBehavior = runBehavior;
            }
        }

        // 检查是否进入攻击触发器并切换到攻击行为
        if (other.gameObject.CompareTag("Attack") && currentBehavior == runBehavior)
        {
            Debug.Log("Player detected! Switching to Attack.");
            currentBehavior = attackBehavior;
        }
    }

    void SwitchToNextBehavior()
    {
        // 根据当前行为切换到其他行为
        if (currentBehavior == attackBehavior)
        {
            Debug.Log("Player left AttackTrigger. Switching to RunBehavior.");
            currentBehavior = runBehavior;  // 切换到追逐行为
        }
        else if (currentBehavior == hurtBehavior)
        {
            Debug.Log("Player left hurtBehavior. Switching to RunBehavior.");
            currentBehavior = runBehavior;  // 受伤后切换到追逐行为
        }
    }
}
