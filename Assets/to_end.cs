using UnityEngine;
using UnityEngine.UI;  // ���� UI �����ռ�
using UnityEngine.SceneManagement;  // ���볡�������������¼��ػ��˳���

public class GameEndTriggers : MonoBehaviour
{
    public Transform target;  // Ŀ��λ��
    public float distanceThreshold = 5f;  // �жϾ������ֵ
    private Transform player;  // ��� Transform

    // UI Ԫ��
    public Canvas endGameCanvas;  // ������ʾ��������� Canvas
    public Text winText;  // ��ʾ "YOU WIN" �� Text
    public Button exitButton;  // �����˳���Ϸ�� Button

    void Start()
    {
        // ��ȡ��Ҷ���
        player = GameObject.FindWithTag("Player").transform;  // ������������� Player ��ǩ

        // ���� UI Ԫ�أ�Ĭ�ϲ���ʾ
        endGameCanvas.enabled = false;

        // Ϊ��ť���õ���¼�
        exitButton.onClick.AddListener(ExitGame);
    }

    void Update()
    {
        // ���������Ŀ��֮��ľ���
        float distance = Vector3.Distance(player.position, target.position);

        // ��������Ŀ��ľ���С����ֵ��������Ϸ����
        if (distance < distanceThreshold)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        // ��ʾ "YOU WIN" �Ͱ�ť
        winText.text = "YOU WIN";  // �����ı�Ϊ "YOU WIN"
        endGameCanvas.enabled = true;  // ��ʾ Canvas

        // ��ͣ��Ϸ
        Time.timeScale = 0;  // ֹͣ��Ϸʱ�䣨����Ϸ��ͣ��
    }

    // ��ť���ʱ�˳���Ϸ
    void ExitGame()
    {
        // �˳���Ϸ
        Debug.Log("Game Over. Exiting...");
        Application.Quit();

        // �ڱ༭���в���ʱ���˳�����Ч��������ʾ��־��Ϣ
        // Unity �༭���ڲ���ʱ������ʹ����������˳���Ϸ
        // UnityEditor.EditorApplication.isPlaying = false;
    }
}
