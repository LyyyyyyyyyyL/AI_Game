using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;  // �ӵ�Ԥ����
    public Transform firePoint;      // �����
    public float bulletSpeed = 10f;  // �ӵ��ٶ�

    // ÿ�ε��ʱ�����ӵ�
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))  // �������������������õ����밴ť
        {
            FireBullet();
            print("fire billet");
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
}
