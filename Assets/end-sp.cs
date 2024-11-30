using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text winText; // 用来显示"You Win!"的文本
    public Button returnButton; // 返回菜单的按钮
    public Transform targetPosition; // 玩家需要到达的目标位置
    public float winDistance = 1f; // 玩家到达目标的距离阈值

    private bool gameWon = false; // 是否已获胜

    void Start()
    {
        // 初始时隐藏"You Win!"文本和按钮
        winText.gameObject.SetActive(false);
        returnButton.gameObject.SetActive(false);

        // 为按钮添加点击事件
        returnButton.onClick.AddListener(ReturnToMenu);
    }

    void Update()
    {
        // 检查玩家是否到达目标位置
        if (!gameWon && Vector3.Distance(transform.position, targetPosition.position) < winDistance)
        {
            GameWon();
        }
    }

    void GameWon()
    {
        // 当玩家到达目标位置时，显示"You Win!"文本和按钮
        gameWon = true;
        winText.gameObject.SetActive(true);
        returnButton.gameObject.SetActive(true);
    }

    void ReturnToMenu()
    {
        // 返回到主菜单场景（假设主菜单场景的名称为"MainMenu"）
        SceneManager.LoadScene("MainMenu");
    }
}
