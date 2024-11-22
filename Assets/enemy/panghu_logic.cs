using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public string targetTag = "Player";  // 用来寻找目标的标签
    public float moveSpeed = 3f;  // 敌人追击速度
    private GameObject target;  // 目标对象（玩家）

    void Start()
    {
        // 在开始时寻找场景中标记为 "Player" 的对象
        target = GameObject.FindWithTag(targetTag);

        if (target == null)
        {
            Debug.LogWarning("No target with tag 'Player' found in the scene!");
        }
    }

    void Update()
    {
        if (target == null)
        {
            Debug.LogWarning("Target is not assigned or not found!");
            return;
        }

        Vector3 targetPosition = target.transform.position;

        // 计算敌人朝目标移动的方向
        Vector3 direction = (targetPosition - transform.position).normalized;

        // 让敌人朝向目标（玩家）
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);  // 平滑旋转

        // 移动敌人朝目标移动
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
    }
}
