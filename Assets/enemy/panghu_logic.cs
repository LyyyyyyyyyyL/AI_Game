using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Idle,
    Patrol,
    Chase,
    Attack,
    Dead,
    Hurt,
    Evade,
    Cast,
    Alert
}


public class panghu_logic : MonoBehaviour
{
    public EnemyState currentState = EnemyState.Patrol;

    public float moveSpeed = 2f;  // 控制移动速度
    public float moveDistance = 3f; // 移动的最大距离
    private Vector3 initialPosition; // 初始位置
    public AudioSource audioSource; // 用来播放声音的 AudioSource
    public AudioClip patrolSound; // 待机时播放的声音

    private bool isMovingForward = true;  // 用来控制前后移动的方向

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;  // 记录初始位置
        audioSource = GetComponent<AudioSource>();  // 获取 AudioSource 组件
        audioSource.loop = true;  // 设置音效循环播放
        audioSource.clip = patrolSound;  // 设置待机音效
        audioSource.Play();  // 播放音效
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                // 执行待机行为

                break;
            case EnemyState.Patrol:
                // 执行巡逻行为
                PatrolBehavior();
                break;
            case EnemyState.Chase:
                // 执行追踪行为

                break;
            case EnemyState.Attack:
                // 执行攻击行为
                break;
            case EnemyState.Dead:
                // 执行死亡行为
                break;
            case EnemyState.Hurt:
                // 执行受伤行为
                break;
            case EnemyState.Alert:
                // 执行警觉行为
                break;
        }

    }

    void PatrolBehavior()
    {
        // 让敌人前后移动
        float movement = moveSpeed * Time.deltaTime;

        if (isMovingForward)
        {
            transform.Translate(Vector3.forward * movement);  // 向前移动
            if (Vector3.Distance(transform.position, initialPosition) >= moveDistance)
            {
                isMovingForward = false;  // 达到最大距离后改变方向
            }
        }
        else
        {
            transform.Translate(Vector3.back * movement);  // 向后移动
            if (Vector3.Distance(transform.position, initialPosition) <= 0.1f)
            {
                isMovingForward = true;  // 回到初始位置后改变方向
            }
        }
    }
}
