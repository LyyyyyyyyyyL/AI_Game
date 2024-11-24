using UnityEngine;

public class PatrolBehavior : BehaviorNode
{
    public float patrolSpeed = 5f; // �ƶ��ٶȣ���λΪ��/��
    public float rotateSpeed = 50f; // ��ת�ٶȣ���λΪ��/��
    private Transform enemyTransform; // �����洢���˵� Transform
    public GameObject enemy; // ��Ҫ���Ƶĵ���

    void Start()
    {
        // ��ȡ���˵� Transform ���
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
        // ��ӡ���˵�ǰλ�ã����ڵ���
        Debug.Log("Enemy Current Position: " + enemyTransform.position);

        // �õ�����ת
        RotateEnemy();

        // �õ��������Լ���ǰ��Եķ�����ǰ��
        MoveEnemyForward();

        // ���� true����ʾѲ����Ϊ��������
        return true;
    }

    // �õ���Χ�� Y ����ת
    void RotateEnemy()
    {
        // ��ת���ˣ��� Y ����ת rotateSpeed ��/��
        enemyTransform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }

    // �õ��������Լ���ǰ�ĳ�������ǰ��
    void MoveEnemyForward()
    {
        // ʹ������������ǰ�������ƶ����ƶ��ٶ�Ϊ patrolSpeed
        enemyTransform.Translate(enemyTransform.forward * patrolSpeed * Time.deltaTime);
    }
}
