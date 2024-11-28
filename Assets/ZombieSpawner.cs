using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab; // 僵尸的 Prefab
    public int zombieCount = 10; // 要生成的僵尸数量
    public Vector3 spawnArea = new Vector3(10, 0, 10); // 生成区域的大小
    public Transform spawnCenter; // 生成区域的中心

    void Start()
    {
        SpawnZombies();
    }

    void SpawnZombies()
    {
        for (int i = 0; i < zombieCount; i++)
        {
            // 随机生成一个位置
            Vector3 randomPosition = spawnCenter.position + new Vector3(
                Random.Range(-spawnArea.x / 2, spawnArea.x / 2),
                0,
                Random.Range(-spawnArea.z / 2, spawnArea.z / 2)
            );

            // 实例化僵尸
            Instantiate(zombiePrefab, randomPosition, Quaternion.identity);
        }
    }
}
