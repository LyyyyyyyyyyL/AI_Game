using UnityEngine;

public class PatrolBehavior : BehaviorNode
{
    public float wanderSpeed = 5f;  // �ƶ��ٶȣ���λΪ��/��
    public float wanderRadius = 5f;  // �ǻ��뾶
    public float wanderJitter = 0.2f;  // �����������
    private Transform enemyTransform;  // �����洢���˵� Transform
    public GameObject enemy;  // ��Ҫ���Ƶĵ���
    public AudioClip zombieGrowl;  // �洢��ʬ�ĺ����
    private AudioSource audioSource;  // �������������� AudioSource

    private Vector3 wanderTarget;  // ��ǰ�ǻ�Ŀ��

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
            audioSource.loop = false;  // ���ò�ѭ������
            // ÿ�� 10 �벥��һ�κ����
            InvokeRepeating("PlayGrowl", 0f, 10f);
        }
        else
        {
            Debug.LogError("Zombie growl sound not assigned!");
        }

        // ��ʼ���ǻ�Ŀ��
        wanderTarget = enemyTransform.position;
    }

    public override bool Run()
    {
        // ��ӡ���˵�ǰλ�ã����ڵ���
        //Debug.Log("Enemy Current Position: " + enemyTransform.position);

        // �����µ��ǻ�Ŀ��
        Wander();

        // �õ������ŵ�ǰλ�û���ǰ��
        MoveEnemyForward();

        // ���� true����ʾ�ǻ���Ϊ��������
        return true;
    }

    // �����µ��ǻ�Ŀ���
    void Wander()
    {
        // ����һ������Ķ�������
        Vector3 randomDisplacement = new Vector3(
            Random.Range(-1f, 1f),
            0,  // ��2Dƽ���3D�����У�y�᲻��Ҫ�仯
            Random.Range(-1f, 1f)
        ) * wanderJitter;

        // �������������ǻ�Ŀ��
        wanderTarget += randomDisplacement;

        // ���ǻ�Ŀ�����Ƶ�Բ��
        wanderTarget = (wanderTarget - enemyTransform.position).normalized * wanderRadius + enemyTransform.position;
    }

    // �õ������ŵ�ǰλ�û���ǰ��������Ŀ��
    void MoveEnemyForward()
    {
        // ʹ������������ǰ�������ƶ����ƶ��ٶ�Ϊ wanderSpeed
        enemyTransform.position = Vector3.MoveTowards(enemyTransform.position, wanderTarget, wanderSpeed * Time.deltaTime);

        // ������˵�ǰ����
        Vector3 directionToTarget = wanderTarget - enemyTransform.position;

        // ���Ŀ��λ�ò��ڵ�ǰ��ǰ��������ת
        if (directionToTarget.sqrMagnitude > 0.1f)  // ��ֹ�������
        {
            // �����µ���ת�Ƕ�
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

            // �õ���ƽ����ת��Ŀ�귽��
            enemyTransform.rotation = Quaternion.RotateTowards(enemyTransform.rotation, targetRotation, 360f * Time.deltaTime);
        }
    }

    // ���Ž�ʬ�ĺ����
    void PlayGrowl()
    {
        audioSource.Play();
    }
}
