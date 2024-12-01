using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 25f;   // �ӵ��˺�

    private void OnCollisionEnter(Collision collision)
    {
        // �����ײ�������
        if (collision.gameObject.CompareTag("Player"))
        {
            // ��ȡ��ҵ� Health ���
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            if (playerHealth != null)
            {
                // ���������˺�
                playerHealth.TakeDamage(damage);
                Debug.Log("Player hit! Damage applied: " + damage);
            }
        }

        // �����ӵ�
        Destroy(gameObject);
    }
}
