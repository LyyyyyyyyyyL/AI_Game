using UnityEngine;

public class PatrolBehavior : BehaviorNode
{
    public float patrolSpeed = 5f; // �ƶ��ٶȣ���λΪ��/��
    public float rotateSpeed = 50f; // ��ת�ٶȣ���λΪ��/��
    private Transform enemyTransform; // �����洢���˵� Transform
    public GameObject enemy; // ��Ҫ���Ƶĵ���
    public AudioClip zombieGrowl; // �洢��ʬ�ĺ����
    private AudioSource audioSource; // �������������� AudioSource

    void Start()
    {
        if (enemy != null)
        {
            enemyTransform = enemy.transform;
        }
        else
        {
            Debug.LogError("Enemy GameObject not assigned!");
        }

        // ��ȡ���˶���� AudioSource ���
        audioSource = enemy.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // ���û���ҵ� AudioSource �������̬���һ��
            audioSource = enemy.AddComponent<AudioSource>();
        }

        // ������Ч�ļ�
        if (zombieGrowl != null)
        {
            audioSource.clip = zombieGrowl;
            audioSource.loop = false; // ���ò�ѭ������
            // ÿ�� 10 �벥��һ�κ����
            InvokeRepeating("PlayGrowl", 0f, 10f);
            audioSource.Play(); // ��������
        }
        else
        {
            Debug.LogError("Zombie growl sound not assigned!");
        }

        Debug.Log("Patrol Behavior Started.");
    }

    public override bool Run()
    {
        // ��ӡ���˵�ǰλ�ã����ڵ���
        //Debug.Log("Enemy Current Position: " + enemyTransform.position);

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
