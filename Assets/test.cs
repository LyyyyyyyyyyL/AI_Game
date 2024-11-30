//using UnityEngine;
//using UnityEngine.SceneManagement;  // 用于加载场景
//using UnityEngine.UI;  // 用于UI组件

//public class GameManager : MonoBehaviour
//{
//    public Text winText;  // 显示“YOU Win!!!”的文本
//    public Button restartButton;  // 重玩按钮
//    public Button quitButton;  // 退出按钮

//    void OnTriggerEnter(Collider other)
//    {
//        // 检查碰撞的物体是否是玩家
//        if (other.CompareTag("Player"))
//        {
//            // 玩家到达目标地点时，触发游戏结束
//            EndGame();
//        }
//    }

//    void EndGame()
//    {
//        // 显示“YOU Win!!!”文本
//        winText.gameObject.SetActive(true);  // 激活文本
//        winText.text = "YOU Win!!!";  // 设置文本内容

//        // 激活按钮
//        restartButton.gameObject.SetActive(true);  // 激活重玩按钮
//        quitButton.gameObject.SetActive(true);  // 激活退出按钮

//        Time.timeScale = 0;  // 停止游戏时间
//    }

//    // 绑定到重玩按钮
//    public void RestartGame()
//    {
//        Time.timeScale = 1;  // 恢复游戏时间
//        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // 重载当前场景
//    }

//    // 绑定到退出按钮
//    public void QuitGame()
//    {
//        Time.timeScale = 1;  // 恢复游戏时间
//        SceneManager.LoadScene("MainMenu");  // 加载主菜单场景（确保有主菜单场景）
//    }

//    void Start()
//    {
//        // 确保按钮点击事件绑定到正确的方法
//        restartButton.onClick.AddListener(RestartGame);
//        quitButton.onClick.AddListener(QuitGame);

//        // 确保游戏开始时文本和按钮是隐藏的
//        winText.gameObject.SetActive(false);
//        restartButton.gameObject.SetActive(false);
//        quitButton.gameObject.SetActive(false);
//    }
//}
