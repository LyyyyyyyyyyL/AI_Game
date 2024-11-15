using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Slider healthSlider;  // 绑定血条 Slider
    public Transform headPosition;  // 绑定敌人头部的位置
    public float maxHealth = 100f;  // 最大血量
    private float currentHealth;  // 当前血量

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    void Update()
    {
        // 让血条跟随头部位置
        healthSlider.transform.position = Camera.main.WorldToScreenPoint(headPosition.position + new Vector3(0, 2, 0));  // +2的Y偏移量，确保血条在头上方

        // 示例：按 M 键减少血量
        if (Input.GetKeyDown(KeyCode.M))
        {
            currentHealth -= 10f;
            if (currentHealth < 0) currentHealth = 0;
            healthSlider.value = currentHealth;
        }
    }
}
