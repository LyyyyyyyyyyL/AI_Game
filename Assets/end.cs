using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // 玩家和目标点的设置
    public Transform player;          // 玩家物体
    public Transform target;          // 目标点
    public float winDistance = 3.0f;  // 玩家与目标点的胜利距离

    // UI 元素
    public Text winText;              // 显示胜利消息的文本
    public Button quitButton;         // 退出按钮

    private bool gameWon = false;

    // Start is called before the first frame update
    void Start()
    {
        // 确保胜利消息和按钮初始时不可见
        winText.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);

        // 设置按钮的点击事件
        quitButton.onClick.AddListener(QuitGame);
    }

    // Update is called once per frame
    void Update()
    {
        // 如果游戏已经获胜，停止玩家移动
        if (!gameWon)
        {
            float moveSpeed = 5f * Time.deltaTime;
            float moveHorizontal = Input.GetAxis("Horizontal") * moveSpeed;
            float moveVertical = Input.GetAxis("Vertical") * moveSpeed;

            player.Translate(moveHorizontal, 0, moveVertical);
        }

        // 检查玩家是否靠近目标点
        CheckWinCondition();
    }


    // 检查玩家与目标点的距离，判断是否获胜
    void CheckWinCondition()
    {
        if (Vector3.Distance(player.position, target.position) <= winDistance && !gameWon)
        {
            gameWon = true;
            Debug.Log("Player won the game!");  // 添加调试输出
                                                // 显示胜利消息和按钮
            winText.gameObject.SetActive(true);
            quitButton.gameObject.SetActive(true);
        }
    }


    // 退出游戏
    void QuitGame()
    {
        // 退出游戏
        Debug.Log("Game Over! Exiting...");
        Application.Quit();

        // 如果是在编辑器中测试，停止游戏
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
