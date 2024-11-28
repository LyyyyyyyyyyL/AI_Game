using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 10;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log(" Collision working");

        // 检测碰撞的对象
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy Collision working");
            // 获取敌人身上的 EnemyHealth 脚本
            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
   

            // 如果敌人有 EnemyHealth 脚本，调用 TakeDamage 函数
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);  // 传入伤害值
            }
            else
            {
                Debug.Log("null! no enemyHealth");
            }


        }
        Destroy(gameObject);
    }
}
