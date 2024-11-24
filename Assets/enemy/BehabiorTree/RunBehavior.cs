using UnityEngine;

public class RunBehavior : BehaviorNode
{
    public GameObject enemy; // ��Ҫ���Ƶĵ���
    public float runSpeed = 5f; // ׷���ٶ�
    public float rotationSpeed = 5f; // ��ת�ٶ�
    private Transform enemyTransform; // �����洢���˵� Transform
    private Transform playerTransform; // �����洢��ҵ� Transform

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

        // ������Ҳ���ȡ�� Transform ���
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (playerTransform == null)
        {
            Debug.LogError("Player not found! Make sure the player has the 'Player' tag.");
        }

        Debug.Log("Run Behavior started.");
    }

    public override bool Run()
    {
        // ȷ�����˺���Ҷ���Ϊ��
        if (enemyTransform == null)
        {
            Debug.LogError("Enemy Transform is not assigned!");
            return false; // ���û�е��ˣ����� false
        }

        if (playerTransform == null)
        {
            Debug.LogWarning("Player not found, enemy cannot run towards player.");
            return false; // ���û����ң����� false
        }

        // ������˺����֮��ķ���
        Vector3 directionToPlayer = playerTransform.position - enemyTransform.position;

        // �õ��˳�������ƶ�
        enemyTransform.Translate(directionToPlayer.normalized * runSpeed * Time.deltaTime, Space.World);

        // �õ����������
        // ����1: ʹ�� LookAt��ʹ����ֱ�ӳ������
        // enemyTransform.LookAt(playerTransform.position);

        // ����2: ʹ��ƽ����ת������Ȼ��
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
        enemyTransform.rotation = Quaternion.Slerp(enemyTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // ��ӡ��ǰ���˺���ҵ�λ�ã����ڵ���
        Debug.Log("Enemy Current Position: " + enemyTransform.position);
        Debug.Log("Player Position: " + playerTransform.position);

        // �����������״̬���������׷����Ϊ�������У����� true
        return true;
    }
}
