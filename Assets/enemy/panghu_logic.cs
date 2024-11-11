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

    public float moveSpeed = 2f;  // �����ƶ��ٶ�
    public float moveDistance = 3f; // �ƶ���������
    private Vector3 initialPosition; // ��ʼλ��
    public AudioSource audioSource; // �������������� AudioSource
    public AudioClip patrolSound; // ����ʱ���ŵ�����

    private bool isMovingForward = true;  // ��������ǰ���ƶ��ķ���

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;  // ��¼��ʼλ��
        audioSource = GetComponent<AudioSource>();  // ��ȡ AudioSource ���
        audioSource.loop = true;  // ������Чѭ������
        audioSource.clip = patrolSound;  // ���ô�����Ч
        audioSource.Play();  // ������Ч
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                // ִ�д�����Ϊ

                break;
            case EnemyState.Patrol:
                // ִ��Ѳ����Ϊ
                PatrolBehavior();
                break;
            case EnemyState.Chase:
                // ִ��׷����Ϊ

                break;
            case EnemyState.Attack:
                // ִ�й�����Ϊ
                break;
            case EnemyState.Dead:
                // ִ��������Ϊ
                break;
            case EnemyState.Hurt:
                // ִ��������Ϊ
                break;
            case EnemyState.Alert:
                // ִ�о�����Ϊ
                break;
        }

    }

    void PatrolBehavior()
    {
        // �õ���ǰ���ƶ�
        float movement = moveSpeed * Time.deltaTime;

        if (isMovingForward)
        {
            transform.Translate(Vector3.forward * movement);  // ��ǰ�ƶ�
            if (Vector3.Distance(transform.position, initialPosition) >= moveDistance)
            {
                isMovingForward = false;  // �ﵽ�������ı䷽��
            }
        }
        else
        {
            transform.Translate(Vector3.back * movement);  // ����ƶ�
            if (Vector3.Distance(transform.position, initialPosition) <= 0.1f)
            {
                isMovingForward = true;  // �ص���ʼλ�ú�ı䷽��
            }
        }
    }
}
