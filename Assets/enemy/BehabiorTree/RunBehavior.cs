using UnityEngine;

public class RunBehavior : BehaviorNode
{
    public GameObject enemy; // 需要控制的敌人
    public float runSpeed = 5f; // 追逐速度
    public float rotationSpeed = 5f; // 旋转速度
    private Transform enemyTransform; // 用来存储敌人的 Transform
    private Transform playerTransform; // 用来存储玩家的 Transform

    void Start()
    {
        // 获取敌人的 Transform 组件
        if (enemy != null)
        {
            enemyTransform = enemy.transform;
        }
        else
        {
            Debug.LogError("Enemy GameObject not assigned!");
        }

        // 查找玩家并获取其 Transform 组件
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (playerTransform == null)
        {
            Debug.LogError("Player not found! Make sure the player has the 'Player' tag.");
        }

        Debug.Log("Run Behavior started.");
    }

    public override bool Run()
    {
        // 确保敌人和玩家都不为空
        if (enemyTransform == null)
        {
            Debug.LogError("Enemy Transform is not assigned!");
            return false; // 如果没有敌人，返回 false
        }

        if (playerTransform == null)
        {
            Debug.LogWarning("Player not found, enemy cannot run towards player.");
            return false; // 如果没有玩家，返回 false
        }

        // 计算敌人和玩家之间的方向
        Vector3 directionToPlayer = playerTransform.position - enemyTransform.position;

        // 让敌人朝着玩家移动
        enemyTransform.Translate(directionToPlayer.normalized * runSpeed * Time.deltaTime, Space.World);

        // 让敌人面向玩家
        // 方法1: 使用 LookAt，使敌人直接朝向玩家
        // enemyTransform.LookAt(playerTransform.position);

        // 方法2: 使用平滑旋转（更自然）
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
        enemyTransform.rotation = Quaternion.Slerp(enemyTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // 打印当前敌人和玩家的位置，用于调试
        Debug.Log("Enemy Current Position: " + enemyTransform.position);
        Debug.Log("Player Position: " + playerTransform.position);

        // 返回任务完成状态，这里假设追逐行为持续进行，返回 true
        return true;
    }
}
