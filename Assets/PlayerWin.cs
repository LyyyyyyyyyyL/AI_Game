using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSuccess : MonoBehaviour
{
    public Transform targetPosition;  // 玩家需要到达的目标位置
    public float successDistance = 5f;  // 到达目标位置的距离阈值

    private bool gameWon = false;  // 游戏是否成功标志

    void Update()
    {
        // 输出玩家当前位置与目标位置之间的距离，帮助调试
        float distance = Vector3.Distance(transform.position, targetPosition.position);
        Debug.Log("Current distance to target: " + distance);

        // 检查玩家是否到达目标位置，并且只有在游戏未成功时触发
        if (distance < successDistance)
        {
            TriggerSuccess();
        }
    }

    // 游戏成功事件
    void TriggerSuccess()
    {
        Debug.Log("CTriggerSuccess running" );
        // 确保游戏成功标志已设置，防止重复执行
        if (!gameWon)
        {
            gameWon = true;
            Debug.Log("Game Success");

            // 游戏结束后可以执行其他处理逻辑，如播放动画、停止计时等
            Time.timeScale = 1;  // 确保游戏时间继续

            // 切换到成功场景
            SceneManager.LoadScene("WinGame");
        }
    }
}
