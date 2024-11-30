using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // ��Һ�Ŀ��������
    public Transform player;          // �������
    public Transform target;          // Ŀ���
    public float winDistance = 3.0f;  // �����Ŀ����ʤ������

    // UI Ԫ��
    public Text winText;              // ��ʾʤ����Ϣ���ı�
    public Button quitButton;         // �˳���ť

    private bool gameWon = false;

    // Start is called before the first frame update
    void Start()
    {
        // ȷ��ʤ����Ϣ�Ͱ�ť��ʼʱ���ɼ�
        winText.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);

        // ���ð�ť�ĵ���¼�
        quitButton.onClick.AddListener(QuitGame);
    }

    // Update is called once per frame
    void Update()
    {
        // �����Ϸ�Ѿ���ʤ��ֹͣ����ƶ�
        if (!gameWon)
        {
            float moveSpeed = 5f * Time.deltaTime;
            float moveHorizontal = Input.GetAxis("Horizontal") * moveSpeed;
            float moveVertical = Input.GetAxis("Vertical") * moveSpeed;

            player.Translate(moveHorizontal, 0, moveVertical);
        }

        // �������Ƿ񿿽�Ŀ���
        CheckWinCondition();
    }


    // ��������Ŀ���ľ��룬�ж��Ƿ��ʤ
    void CheckWinCondition()
    {
        if (Vector3.Distance(player.position, target.position) <= winDistance && !gameWon)
        {
            gameWon = true;
            Debug.Log("Player won the game!");  // ��ӵ������
                                                // ��ʾʤ����Ϣ�Ͱ�ť
            winText.gameObject.SetActive(true);
            quitButton.gameObject.SetActive(true);
        }
    }


    // �˳���Ϸ
    void QuitGame()
    {
        // �˳���Ϸ
        Debug.Log("Game Over! Exiting...");
        Application.Quit();

        // ������ڱ༭���в��ԣ�ֹͣ��Ϸ
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
