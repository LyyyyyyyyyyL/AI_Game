using UnityEngine;

public class TeammateGun3 : MonoBehaviour
{
    public AudioSource audioSource;       // ����Դ
    public AudioClip shootSound;          // �������
    public GameObject bulletPrefab;       // �ӵ�Ԥ����
    public Transform firePoint;           // �����
    public float bulletSpeed = 10f;       // �ӵ��ٶ�

    public void FireBullet()
    {
        if (firePoint == null || bulletPrefab == null)
        {
            Debug.LogWarning("FirePoint or BulletPrefab is not assigned!");
            return;
        }

        // �����ӵ�ʵ��
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // �����ӵ��ٶ�
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = firePoint.forward * bulletSpeed;
        }

        Debug.Log("Bullet fired!");
        PlayShootSound();  // �����������
    }

    void PlayShootSound()
    {
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }
}
