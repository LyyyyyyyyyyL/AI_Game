using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AStarPathfinding : MonoBehaviour
{
    public Transform target; // Player's position
    public float moveSpeed = 5f;
    public float refreshRate = 0.5f; // How often to refresh the path

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;  // To manually control rotation
    }

    void Update()
    {
        // 每隔一定时间更新一次路径
        if (Vector3.Distance(transform.position, target.position) > 2f)
        {
            UpdatePath();
        }
    }

    void UpdatePath()
    {
        NavMeshPath path = new NavMeshPath();
        if (NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, path))
        {
            // 计算出的路径
            agent.SetPath(path); // 使用NavMeshAgent执行路径
        }
    }
}
