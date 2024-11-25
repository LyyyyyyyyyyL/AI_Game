using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;  // ���Ѫ��
    public float currentHealth;     // ��ǰѪ��
    public Slider playerHealthSlider; // ���Ѫ��Slider
    public Image fillImage; // �����޸�Ѫ����ɫ��Image

    private Coroutine damageCoroutine; // �����洢Э�����ã�����ֹͣЭ��

    void Start()
    {
        // ��ʼ��Ѫ��
        currentHealth = maxHealth;
        // ����Ѫ�������ֵ�ͳ�ʼֵ
        playerHealthSlider.maxValue = maxHealth;
        playerHealthSlider.value = currentHealth;

        // ��ȡѪ������䲿��Image
        fillImage = playerHealthSlider.fillRect.GetComponent<Image>();
    }

    void Update()
    {
        // ���ݵ�ǰѪ������Ѫ��
        playerHealthSlider.value = currentHealth;

        // ���Ѫ��������Ѫ��������ɫ
        if (currentHealth <= maxHealth / 2f)
        {
            fillImage.color = Color.red;
        }
        else
        {
            fillImage.color = Color.white;  // Ĭ����ɫ
        }

        // ���Ѫ���Ƿ�Ϊ 0��������Ϸ
        if (currentHealth <= 0f)
        {
            EndGame();
        }
    }

    // ��Ѫ�ķ���
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);  // ��֤Ѫ��������0
    }

    // ������Ϸ�ķ���
    void EndGame()
    {
        Debug.Log("Game Over");
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) // ȷ����ײ�������ǵ���
        {
            // ����Ѿ��ڿ�Ѫ�У���ֹͣ���е�Э��
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
            }
            // ����Э��ÿ 0.5 ���һ��Ѫ
            damageCoroutine = StartCoroutine(DamageOverTime());
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) // ������뿪����ʱֹͣ��Ѫ
        {
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }

    // ÿ 0.5 ���һ��Ѫ
    IEnumerator DamageOverTime()
    {
        while (true)  // ������Ѫ��ֱ����ײ����
        {
            TakeDamage(10f); // ÿ�ο� 10 Ѫ
            yield return new WaitForSeconds(0.5f);  // �ȴ� 0.5 ��
        }
    }
}
