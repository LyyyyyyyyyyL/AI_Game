using UnityEngine;

public class TeammateGun3 : MonoBehaviour
{
    public AudioSource audioSource;       // 声音源
    public AudioClip shootSound;          // 射击声音
    public GameObject bulletPrefab;       // 子弹预制体
    public Transform firePoint;           // 发射点
    public float bulletSpeed = 10f;       // 子弹速度

    public void FireBullet()
    {
        if (firePoint == null || bulletPrefab == null)
        {
            Debug.LogWarning("FirePoint or BulletPrefab is not assigned!");
            return;
        }

        // 创建子弹实例
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // 设置子弹速度
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = firePoint.forward * bulletSpeed;
        }

        Debug.Log("Bullet fired!");
        PlayShootSound();  // 播放射击声音
    }

    void PlayShootSound()
    {
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }
}
