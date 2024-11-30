using UnityEngine;
using UnityEngine.UI;  // 引入 UI 命名空间
using UnityEngine.SceneManagement;  // 引入场景管理（用于重新加载或退出）

public class GameEndTriggers : MonoBehaviour
{
    public Transform target;  // 目标位置
    public float distanceThreshold = 5f;  // 判断距离的阈值
    private Transform player;  // 玩家 Transform

    // UI 元素
    public Canvas endGameCanvas;  // 用来显示结束画面的 Canvas
    public Text winText;  // 显示 "YOU WIN" 的 Text
    public Button exitButton;  // 用于退出游戏的 Button

    void Start()
    {
        // 获取玩家对象
        player = GameObject.FindWithTag("Player").transform;  // 假设玩家物体有 Player 标签

        // 隐藏 UI 元素，默认不显示
        endGameCanvas.enabled = false;

        // 为按钮设置点击事件
        exitButton.onClick.AddListener(ExitGame);
    }

    void Update()
    {
        // 计算玩家与目标之间的距离
        float distance = Vector3.Distance(player.position, target.position);

        // 如果玩家与目标的距离小于阈值，触发游戏结束
        if (distance < distanceThreshold)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        // 显示 "YOU WIN" 和按钮
        winText.text = "YOU WIN";  // 设置文本为 "YOU WIN"
        endGameCanvas.enabled = true;  // 显示 Canvas

        // 暂停游戏
        Time.timeScale = 0;  // 停止游戏时间（让游戏暂停）
    }

    // 按钮点击时退出游戏
    void ExitGame()
    {
        // 退出游戏
        Debug.Log("Game Over. Exiting...");
        Application.Quit();

        // 在编辑器中测试时，退出不生效，但会显示日志信息
        // Unity 编辑器内测试时，可以使用以下语句退出游戏
        // UnityEditor.EditorApplication.isPlaying = false;
    }
}
