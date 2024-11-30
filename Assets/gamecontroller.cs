using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndManager : MonoBehaviour
{
    void Start()
    {
        // 启用鼠标光标
        Cursor.visible = true;

        // 解锁鼠标光标（让鼠标可以自由移动）
        Cursor.lockState = CursorLockMode.None;
    }

    public void MainMenu()
    {
        // 加载 GameStart 场景
        SceneManager.LoadScene("GameStart");
    }

    public void StartGame()
    {
        // 加载 MainScene 场景
        SceneManager.LoadScene("MainScence");
    }

    public void QuitGame()
    {
        // 退出游戏
        Application.Quit();
        Debug.Log("Game has been quit!");  // 用于测试，退出功能仅在打包后的游戏中有效
    }
}
