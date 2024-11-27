using UnityEngine;
using UnityEngine.UI;

public class AllyHealth : MonoBehaviour
{
    public Slider healthSlider1;  // ��Ѫ�� Slider
    public float maxHealth = 100f;  // ���Ѫ��
    public float currentHealth;  // ��ǰѪ��
    public Image fillImage;       // Ѫ������䲿��
    public GameObject deathEffect; // ��ѡ������Ч����Ԥ����
    public bool isDead = false;   // �Ƿ��Ѿ�����

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider1.maxValue = maxHealth;
        healthSlider1.value = currentHealth;

        // ��ȡѪ������䲿��Image
        fillImage = healthSlider1.fillRect.GetComponent<Image>();
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return; // ����Ѿ����������ٴ����˺�

        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        healthSlider1.value = currentHealth;
        Debug.Log("Hitted. Reducing HP. TakeDamage working");

        if (currentHealth == 0)
        {
            Die(); // ������������
        }
    }

    void Die()
    {
        if (isDead) return; // ��ֹ��δ��������߼�

        isDead = true;
        Debug.Log("Ally has died!");

        // ��ѡ����������Ч��
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        // ��ѡ�����ý�ɫ��ִ�����������߼�
        gameObject.SetActive(false); // ��ʱ���ö���ģ������Ч��

        // ����и����߼�������֪ͨ���ѻ���ң�������������չ
    }
}
