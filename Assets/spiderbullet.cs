using UnityEngine;

public class Bullets1 : MonoBehaviour
{
    public float damage = 25f; // �ӵ���ɵ��˺�

    private void OnCollisionEnter(Collision collision)
    {
        // ����Ƿ���ײ�����
        if (collision.gameObject.CompareTag("Player"))
        {
            // ��ȡ��ҵ� PlayerHealth �ű�������˺�
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                Debug.Log("Player hit by bullet! Damage dealt: " + damage);
            }

            // �����ӵ�
            Destroy(gameObject);
        }
        else
        {
            // ���������ң������ӵ�
            Destroy(gameObject);
        }
    }
}
