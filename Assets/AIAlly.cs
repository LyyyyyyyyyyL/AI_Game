using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIAlly : MonoBehaviour
{
    public Transform player; // 玩家位置
    public float followDistance = 5f; // 距离玩家多远时停止移动
    public float attackRange = 3f; // 攻击敌人的范围
    public Transform enemy; // 敌人位置
    private NavMeshAgent agent; // 导航代理

    void Start()
    {
        // 获取 NavMeshAgent 组件
        //agent = GetComponent<NavMeshAgent>();


        agent = GetComponent<NavMeshAgent>();

        // 动态查找玩家
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

        // 计算与玩家的距离
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // 跟随玩家
        if (distanceToPlayer > followDistance)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            agent.ResetPath(); // 停止移动
        }

        // 检测敌人并攻击
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
        // 简单攻击逻辑（可以扩展为射击、近战等）
        Debug.Log("Attacking Enemy!");
    }
}
