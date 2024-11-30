//using UnityEngine;
//using UnityEngine.SceneManagement;  // ���ڼ��س���
//using UnityEngine.UI;  // ����UI���

//public class GameManager : MonoBehaviour
//{
//    public Text winText;  // ��ʾ��YOU Win!!!�����ı�
//    public Button restartButton;  // ���水ť
//    public Button quitButton;  // �˳���ť

//    void OnTriggerEnter(Collider other)
//    {
//        // �����ײ�������Ƿ������
//        if (other.CompareTag("Player"))
//        {
//            // ��ҵ���Ŀ��ص�ʱ��������Ϸ����
//            EndGame();
//        }
//    }

//    void EndGame()
//    {
//        // ��ʾ��YOU Win!!!���ı�
//        winText.gameObject.SetActive(true);  // �����ı�
//        winText.text = "YOU Win!!!";  // �����ı�����

//        // ���ť
//        restartButton.gameObject.SetActive(true);  // �������水ť
//        quitButton.gameObject.SetActive(true);  // �����˳���ť

//        Time.timeScale = 0;  // ֹͣ��Ϸʱ��
//    }

//    // �󶨵����水ť
//    public void RestartGame()
//    {
//        Time.timeScale = 1;  // �ָ���Ϸʱ��
//        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // ���ص�ǰ����
//    }

//    // �󶨵��˳���ť
//    public void QuitGame()
//    {
//        Time.timeScale = 1;  // �ָ���Ϸʱ��
//        SceneManager.LoadScene("MainMenu");  // �������˵�������ȷ�������˵�������
//    }

//    void Start()
//    {
//        // ȷ����ť����¼��󶨵���ȷ�ķ���
//        restartButton.onClick.AddListener(RestartGame);
//        quitButton.onClick.AddListener(QuitGame);

//        // ȷ����Ϸ��ʼʱ�ı��Ͱ�ť�����ص�
//        winText.gameObject.SetActive(false);
//        restartButton.gameObject.SetActive(false);
//        quitButton.gameObject.SetActive(false);
//    }
//}
