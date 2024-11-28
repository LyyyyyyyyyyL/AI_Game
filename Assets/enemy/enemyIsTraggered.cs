using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyIsTraggered : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isInPlayerInAttackTrigger = true; // �������Ƿ��ڴ�����Χ��
    public bool isInBulletTrigger = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter called: " + other.gameObject.name);  // ��ӡ������������
        if (other.gameObject.CompareTag("Attack"))
        {
            isInPlayerInAttackTrigger = true;
            Debug.Log("Player is still in AttackTrigger!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExit called: " + other.gameObject.name);  // ��ӡ������������
        if (other.gameObject.CompareTag("Attack"))
        {
            isInPlayerInAttackTrigger = false;
            Debug.Log("Player left AttackTrigger!");
        }
    }
}
