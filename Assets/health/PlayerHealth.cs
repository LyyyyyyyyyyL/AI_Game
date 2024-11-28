using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;  // 最大血量
    public float currentHealth;     // 当前血量
    public Slider playerHealthSlider; // 玩家血条Slider
    public Image fillImage; // 用于修改血条颜色的Image

    private Coroutine damageCoroutine; // 用来存储协程引用，方便停止协程

    void Start()
    {
        // 初始化血量
        currentHealth = maxHealth;
        // 设置血条的最大值和初始值
        playerHealthSlider.maxValue = maxHealth;
        playerHealthSlider.value = currentHealth;

        // 获取血条的填充部分Image
        fillImage = playerHealthSlider.fillRect.GetComponent<Image>();
    }

    void Update()
    {
        // 根据当前血量更新血条
        playerHealthSlider.value = currentHealth;

        // 检查血量并根据血量设置颜色
        if (currentHealth <= maxHealth / 2f)
        {
            fillImage.color = Color.red;
        }
        else
        {
            fillImage.color = Color.white;  // 默认颜色
        }

        // 检查血量是否为 0，结束游戏
        if (currentHealth <= 0f)
        {
            EndGame();
        }
    }

    // 扣血的方法
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);  // 保证血量不低于0
    }

    // 结束游戏的方法
    void EndGame()
    {
        Debug.Log("Game Over");
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

}
