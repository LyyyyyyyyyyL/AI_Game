using UnityEngine;

public class DieBehavior : BehaviorNode
{
    public GameObject enemy; // 需要倒下并删除的敌人对象
    public float destroyDelay = 2f; // 延迟销毁时间
    public Vector3 rotationAngle = new Vector3(90f, 0f, 0f); // 倒下时的旋转角度
    public float rotationSpeed = 2f; // 旋转的速度，决定每秒转动的角度

    public override bool Run()
    {
        if (enemy != null)
        {
            Debug.Log("DieBehavior is running");

            // 启动协程让敌人逐渐倒下
            Quaternion targetRotation = Quaternion.Euler(rotationAngle);
            enemy.GetComponent<MonoBehaviour>().StartCoroutine(RotateToTarget(enemy.transform, targetRotation));

            // 延迟销毁敌人对象
            Destroy(enemy, destroyDelay);

            return true; // 行为完成
        }
        else
        {
            Debug.LogError("Enemy GameObject is not assigned!");
            return false; // 行为失败
        }
    }

    private System.Collections.IEnumerator RotateToTarget(Transform enemyTransform, Quaternion targetRotation)
    {
        while (Quaternion.Angle(enemyTransform.rotation, targetRotation) > 0.1f)
        {
            enemyTransform.rotation = Quaternion.RotateTowards(
                enemyTransform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
            yield return null;
        }

        // 确保最终完全对准目标角度
        enemyTransform.rotation = targetRotation;
    }
}
