using UnityEngine;

public class Gun : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip shootSound;
    public GameObject bulletPrefab;  // �ӵ�Ԥ����
    public Transform firePoint;      // �����
    public float bulletSpeed = 10f;  // �ӵ��ٶ�


    void Start()
    {
        // ��ȡ AudioSource ���
        audioSource = GetComponent<AudioSource>();
    }

    // ÿ�ε��ʱ�����ӵ�
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))  // �������������������õ����밴ť
        {
            FireBullet();
            PlayShootSound();
            //print("fire billet");
        }
    }

    public void FireBullet()
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
