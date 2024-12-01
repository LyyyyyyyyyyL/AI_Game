using UnityEngine;

public class ChargeBehavior : BehaviorNode
{
    public Transform player;             // ���λ��
    public float chargeSpeed = 10f;      // ��ײ�ٶ�
    public float chargeForce = 10f;      // �������С
    private bool hasCharged = false;     // �Ƿ��Ѿ���ɳ�ײ

    private bool isPaused = false;       // �Ƿ�����ͣ״̬
    private float pauseDuration = 3f;    // ͣ��ʱ��
    private float pauseTimer = 0f;       // ͣ�ټ�ʱ��

    private float chargeDistance = 4f;   // ��ײ��������С����

    public override bool Run()
    {
        if (player == null)
        {
            Debug.LogError("Player transform is not set in ChargeBehavior!");
            return false;
        }

        // ���������ͣ״̬���ȴ���ʱ������
        if (isPaused)
        {
            pauseTimer += Time.deltaTime;
            if (pauseTimer >= pauseDuration)
            {
                isPaused = false;
                pauseTimer = 0f;
                Debug.Log("Pause over. Charger ready to attack again.");
                return false; // ��ȴ��ɣ�������ǰ��Ϊ
            }
            return true; // ��ȴ�У���ִ����������
        }

        // ��ײ�߼�
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0; // ȥ����ֱ����ķ���

        // �ƶ����˳������
        transform.position += direction * chargeSpeed * Time.deltaTime;

        // �������ҵľ���С�ڳ�ײ�������룬ִ�г�ײ��Ϊ
        if (Vector3.Distance(transform.position, player.position) < chargeDistance)
        {
            ApplyCollisionEffect(); // ִ�г�ײЧ��
            isPaused = true;         // ������ͣ״̬
            return false;            // ������ײ��Ϊ
        }

        return true; // ������ײ��ֱ���Ӵ����
    }

    private void ApplyCollisionEffect()
    {
        // ʩ�ӳ�ײЧ��
        Rigidbody playerRb = player.GetComponent<Rigidbody>();
        if (playerRb != null)
        {
            // ʩ�ӳ�����������λ��
            Vector3 pushForce = (player.position - transform.position).normalized * chargeForce;
            playerRb.AddForce(pushForce, ForceMode.Impulse);
        }

        // ����˺�
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(50); // ���������˺�
            Debug.Log("Player hit by Charger! Player takes 50 damage.");
        }

        hasCharged = true; // ���Ϊ����ɳ�ײ
    }

    public void ResetCharge()
    {
        hasCharged = false; // ����״̬��׼����һ�γ�ײ
    }
}
