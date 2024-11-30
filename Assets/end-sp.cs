using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text winText; // ������ʾ"You Win!"���ı�
    public Button returnButton; // ���ز˵��İ�ť
    public Transform targetPosition; // �����Ҫ�����Ŀ��λ��
    public float winDistance = 1f; // ��ҵ���Ŀ��ľ�����ֵ

    private bool gameWon = false; // �Ƿ��ѻ�ʤ

    void Start()
    {
        // ��ʼʱ����"You Win!"�ı��Ͱ�ť
        winText.gameObject.SetActive(false);
        returnButton.gameObject.SetActive(false);

        // Ϊ��ť��ӵ���¼�
        returnButton.onClick.AddListener(ReturnToMenu);
    }

    void Update()
    {
        // �������Ƿ񵽴�Ŀ��λ��
        if (!gameWon && Vector3.Distance(transform.position, targetPosition.position) < winDistance)
        {
            GameWon();
        }
    }

    void GameWon()
    {
        // ����ҵ���Ŀ��λ��ʱ����ʾ"You Win!"�ı��Ͱ�ť
        gameWon = true;
        winText.gameObject.SetActive(true);
        returnButton.gameObject.SetActive(true);
    }

    void ReturnToMenu()
    {
        // ���ص����˵��������������˵�����������Ϊ"MainMenu"��
        SceneManager.LoadScene("MainMenu");
    }
}
