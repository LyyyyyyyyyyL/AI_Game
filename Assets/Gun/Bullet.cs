using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 10;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log(" Collision working");

        // �����ײ�Ķ���
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy Collision working");
            // ��ȡ�������ϵ� EnemyHealth �ű�
            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
   

            // ��������� EnemyHealth �ű������� TakeDamage ����
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);  // �����˺�ֵ
            }
            else
            {
                Debug.Log("null! no enemyHealth");
            }


        }
        Destroy(gameObject);
    }
}
