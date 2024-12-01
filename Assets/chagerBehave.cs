using UnityEngine;

public class BehaviorManager1 : MonoBehaviour
{
    private BehaviorNode currentBehavior; // ��ǰ��Ϊ�ڵ�
    private bool isCooldown = false;      // ��ײ��Ϊ����ȴ״̬

    [SerializeField] private BehaviorNode patrolBehavior; // Ѳ��
    [SerializeField] private BehaviorNode chargeBehavior; // ��ײ

    [SerializeField] private Transform player;            // ��Ҷ���
    [SerializeField] private float detectionRange = 10f;  // ��ⷶΧ

    void Start()
    {
        currentBehavior = patrolBehavior != null ? patrolBehavior : chargeBehavior;
    }

    void Update()
    {
        if (!isCooldown && IsPlayerInDetectionRange())
        {
            currentBehavior = chargeBehavior;
        }

        if (currentBehavior != null && !isCooldown)
        {
            bool taskCompleted = currentBehavior.Run();
            if (!taskCompleted)
            {
                SwitchToCooldown();
            }
        }

        if (isCooldown)
        {
            isCooldown = false;
            currentBehavior = patrolBehavior;
        }
    }

    private bool IsPlayerInDetectionRange()
    {
        if (player == null) return false;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        return distanceToPlayer <= detectionRange;
    }

    private void SwitchToCooldown()
    {
        if (currentBehavior == chargeBehavior)
        {
            ChargeBehavior charge = currentBehavior as ChargeBehavior;
            if (charge != null) charge.ResetCharge();

            Debug.Log("Charge complete. Entering cooldown.");
            isCooldown = true; // ��ȴ״̬
        }
    }
}
