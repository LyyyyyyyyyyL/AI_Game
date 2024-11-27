using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIAlly : MonoBehaviour
{
    public Transform player; // ���λ��
    public float followDistance = 5f; // ������Ҷ�Զʱֹͣ�ƶ�
    public float attackRange = 3f; // �������˵ķ�Χ
    public Transform enemy; // ����λ��
    private NavMeshAgent agent; // ��������

    void Start()
    {
        // ��ȡ NavMeshAgent ���
        //agent = GetComponent<NavMeshAgent>();


        agent = GetComponent<NavMeshAgent>();

        // ��̬�������
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
            else
            {
                Debug.LogError("No GameObject found with tag 'Player'. Please ensure the player has this tag.");
            }
        }
    }

    void Update()
    {
        if (player == null) return;

        // ��������ҵľ���
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // �������
        if (distanceToPlayer > followDistance)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            agent.ResetPath(); // ֹͣ�ƶ�
        }

        // �����˲�����
        if (enemy != null)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.position);
            if (distanceToEnemy <= attackRange)
            {
                AttackEnemy();
            }
        }
    }

    void AttackEnemy()
    {
        // �򵥹����߼���������չΪ�������ս�ȣ�
        Debug.Log("Attacking Enemy!");
    }
}
