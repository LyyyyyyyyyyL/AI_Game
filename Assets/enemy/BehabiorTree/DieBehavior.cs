using UnityEngine;

public class DieBehavior : BehaviorNode
{
    public GameObject enemy; // ��Ҫ���²�ɾ���ĵ��˶���
    public float destroyDelay = 2f; // �ӳ�����ʱ��
    public Vector3 rotationAngle = new Vector3(90f, 0f, 0f); // ����ʱ����ת�Ƕ�
    public float rotationSpeed = 2f; // ��ת���ٶȣ�����ÿ��ת���ĽǶ�

    public override bool Run()
    {
        if (enemy != null)
        {
            Debug.Log("DieBehavior is running");

            // ����Э���õ����𽥵���
            Quaternion targetRotation = Quaternion.Euler(rotationAngle);
            enemy.GetComponent<MonoBehaviour>().StartCoroutine(RotateToTarget(enemy.transform, targetRotation));

            // �ӳ����ٵ��˶���
            Destroy(enemy, destroyDelay);

            return true; // ��Ϊ���
        }
        else
        {
            Debug.LogError("Enemy GameObject is not assigned!");
            return false; // ��Ϊʧ��
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

        // ȷ��������ȫ��׼Ŀ��Ƕ�
        enemyTransform.rotation = targetRotation;
    }
}
