using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public string targetTag = "Player";  // ����Ѱ��Ŀ��ı�ǩ
    public float moveSpeed = 3f;  // ����׷���ٶ�
    private GameObject target;  // Ŀ�������ң�

    void Start()
    {
        // �ڿ�ʼʱѰ�ҳ����б��Ϊ "Player" �Ķ���
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

        // ������˳�Ŀ���ƶ��ķ���
        Vector3 direction = (targetPosition - transform.position).normalized;

        // �õ��˳���Ŀ�꣨��ң�
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);  // ƽ����ת

        // �ƶ����˳�Ŀ���ƶ�
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
    }
}
