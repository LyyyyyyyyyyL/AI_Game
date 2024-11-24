using UnityEngine;

public class PatrolBehavior : BehaviorNode
{
    public float patrolSpeed = 5f; // 移动速度，单位为米/秒
    public float rotateSpeed = 50f; // 旋转速度，单位为度/秒
    private Transform enemyTransform; // 用来存储敌人的 Transform
    public GameObject enemy; // 需要控制的敌人

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

        Debug.Log("Patrol Behavior Started.");
    }

    public override bool Run()
    {
        // 打印敌人当前位置，用于调试
        Debug.Log("Enemy Current Position: " + enemyTransform.position);

        // 让敌人旋转
        RotateEnemy();

        // 让敌人向着自己当前面对的方向缓慢前进
        MoveEnemyForward();

        // 返回 true，表示巡逻行为持续进行
        return true;
    }

    // 让敌人围绕 Y 轴旋转
    void RotateEnemy()
    {
        // 旋转敌人，绕 Y 轴旋转 rotateSpeed 度/秒
        enemyTransform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }

    // 让敌人向着自己当前的朝向方向缓慢前进
    void MoveEnemyForward()
    {
        // 使敌人沿着它的前方方向移动，移动速度为 patrolSpeed
        enemyTransform.Translate(enemyTransform.forward * patrolSpeed * Time.deltaTime);
    }
}
