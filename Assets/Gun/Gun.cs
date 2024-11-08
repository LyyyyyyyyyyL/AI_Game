using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;  // 子弹预制体
    public Transform firePoint;      // 发射点
    public float bulletSpeed = 10f;  // 子弹速度

    // 每次点击时发射子弹
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))  // 按下鼠标左键或其他设置的输入按钮
        {
            FireBullet();
            print("fire billet");
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
}
