using UnityEngine;

public class TeammateGun : MonoBehaviour
{
    public AudioSource audioSource;    // 声音源
    public AudioClip shootSound;       // 射击声音
    public GameObject bulletPrefab;    // 子弹预制体
    public Transform firePoint;        // 发射点
    public float bulletSpeed = 10f;    // 子弹速度

    public float fireRate = 0.5f;      // 每次射击的间隔时间
    public float detectionRange = 15f; // 检测敌人的范围
    public LayerMask enemyLayer;       // 敌人所在的图层
    private float nextFireTime = 0f;   // 记录下一次射击的时间

    void Start()
    {
        audioSource = GetComponent<AudioSource>();  // 获取 AudioSource 组件
    }

    void Update()
    {
        AutoFireAtEnemy();
    }

    // 自动检测敌人并开枪
    void AutoFireAtEnemy()
    {
        // 如果已经到达射击间隔时间
        if (Time.time >= nextFireTime)
        {
            // 检测前方是否有敌人
            RaycastHit hit;
            if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, detectionRange, enemyLayer))
            {
                // 如果射线碰到了敌人，执行射击
                Debug.Log("Enemy detected: " + hit.collider.name);

                FireBullet();           // 开枪
                PlayShootSound();       // 播放射击声音

                // 更新下一次射击时间
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    void FireBullet()
    {
        // 创建子弹实例
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // 给子弹添加速度
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = firePoint.forward * bulletSpeed;
        }
    }

    void PlayShootSound()
    {
        // 播放射击声音
        audioSource.PlayOneShot(shootSound);
        Debug.Log("shootSound");
    }
}
