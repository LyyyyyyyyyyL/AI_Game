using UnityEngine;

public class PatrolBehavior : BehaviorNode
{
    public float wanderSpeed = 5f;  // 移动速度，单位为米/秒
    public float wanderRadius = 5f;  // 徘徊半径
    public float wanderJitter = 0.2f;  // 随机抖动幅度
    private Transform enemyTransform;  // 用来存储敌人的 Transform
    public GameObject enemy;  // 需要控制的敌人
    public AudioClip zombieGrowl;  // 存储僵尸的吼叫声
    private AudioSource audioSource;  // 用来播放声音的 AudioSource

    private Vector3 wanderTarget;  // 当前徘徊目标

    void Start()
    {
        if (enemy != null)
        {
            enemyTransform = enemy.transform;
        }
        else
        {
            Debug.LogError("Enemy GameObject not assigned!");
        }

        // 获取敌人对象的 AudioSource 组件
        audioSource = enemy.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // 如果没有找到 AudioSource 组件，则动态添加一个
            audioSource = enemy.AddComponent<AudioSource>();
        }

        // 设置音效文件
        if (zombieGrowl != null)
        {
            audioSource.clip = zombieGrowl;
            audioSource.loop = false;  // 设置不循环播放
            // 每隔 10 秒播放一次吼叫声
            InvokeRepeating("PlayGrowl", 0f, 10f);
        }
        else
        {
            Debug.LogError("Zombie growl sound not assigned!");
        }

        // 初始化徘徊目标
        wanderTarget = enemyTransform.position;
    }

    public override bool Run()
    {
        // 打印敌人当前位置，用于调试
        //Debug.Log("Enemy Current Position: " + enemyTransform.position);

        // 生成新的徘徊目标
        Wander();

        // 让敌人沿着当前位置缓慢前进
        MoveEnemyForward();

        // 返回 true，表示徘徊行为持续进行
        return true;
    }

    // 生成新的徘徊目标点
    void Wander()
    {
        // 生成一个随机的抖动向量
        Vector3 randomDisplacement = new Vector3(
            Random.Range(-1f, 1f),
            0,  // 在2D平面或3D环境中，y轴不需要变化
            Random.Range(-1f, 1f)
        ) * wanderJitter;

        // 添加随机抖动到徘徊目标
        wanderTarget += randomDisplacement;

        // 将徘徊目标限制到圆内
        wanderTarget = (wanderTarget - enemyTransform.position).normalized * wanderRadius + enemyTransform.position;
    }

    // 让敌人沿着当前位置缓慢前进并面向目标
    void MoveEnemyForward()
    {
        // 使敌人沿着它的前方方向移动，移动速度为 wanderSpeed
        enemyTransform.position = Vector3.MoveTowards(enemyTransform.position, wanderTarget, wanderSpeed * Time.deltaTime);

        // 计算敌人当前方向
        Vector3 directionToTarget = wanderTarget - enemyTransform.position;

        // 如果目标位置不在当前面前，进行旋转
        if (directionToTarget.sqrMagnitude > 0.1f)  // 防止除零错误
        {
            // 计算新的旋转角度
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

            // 让敌人平滑旋转到目标方向
            enemyTransform.rotation = Quaternion.RotateTowards(enemyTransform.rotation, targetRotation, 360f * Time.deltaTime);
        }
    }

    // 播放僵尸的吼叫声
    void PlayGrowl()
    {
        audioSource.Play();
    }
}
