using UnityEngine;

public class TeammateGun : MonoBehaviour
{
    public AudioSource audioSource;    // ����Դ
    public AudioClip shootSound;       // �������
    public GameObject bulletPrefab;    // �ӵ�Ԥ����
    public Transform firePoint;        // �����
    public float bulletSpeed = 10f;    // �ӵ��ٶ�

    public float fireRate = 0.5f;      // ÿ������ļ��ʱ��
    public float detectionRange = 15f; // �����˵ķ�Χ
    public LayerMask enemyLayer;       // �������ڵ�ͼ��
    private float nextFireTime = 0f;   // ��¼��һ�������ʱ��

    void Start()
    {
        audioSource = GetComponent<AudioSource>();  // ��ȡ AudioSource ���
    }

    void Update()
    {
        AutoFireAtEnemy();
    }

    // �Զ������˲���ǹ
    void AutoFireAtEnemy()
    {
        // ����Ѿ�����������ʱ��
        if (Time.time >= nextFireTime)
        {
            // ���ǰ���Ƿ��е���
            RaycastHit hit;
            if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, detectionRange, enemyLayer))
            {
                // ������������˵��ˣ�ִ�����
                Debug.Log("Enemy detected: " + hit.collider.name);

                FireBullet();           // ��ǹ
                PlayShootSound();       // �����������

                // ������һ�����ʱ��
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    void FireBullet()
    {
        // �����ӵ�ʵ��
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // ���ӵ�����ٶ�
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = firePoint.forward * bulletSpeed;
        }
    }

    void PlayShootSound()
    {
        // �����������
        audioSource.PlayOneShot(shootSound);
        Debug.Log("shootSound");
    }
}
