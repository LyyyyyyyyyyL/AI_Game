using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public Slider healthSlider;  // ��Ѫ�� Slider
    public float maxHealth = 10f;  // ���Ѫ��
    public float currentHealth;  // ��ǰѪ��

    // ���� enemyIsTraggered �ű�
    public enemyIsTraggered enemyTriggerScript;

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;

        // ��ȡ enemyIsTraggered �ű�
        if (enemyTriggerScript == null)
        {
            enemyTriggerScript = GetComponent<enemyIsTraggered>();
            if (enemyTriggerScript == null)
            {
                Debug.LogError("Enemy does not have the enemyIsTraggered component!");
            }
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        // ����Ѫ��
        healthSlider.value = currentHealth;
        //Debug.Log("Hitted. Reducing HP. TakeDamage working.");

        // �� isInBulletTrigger ����Ϊ true
        if (enemyTriggerScript != null)
        {
            enemyTriggerScript.isInBulletTrigger = true; // ����Ϊ true ��ʾ���˱��ӵ�����
            Debug.Log("enemyIsTraggered.isInBulletTrigger set to true.");
        }
    }
}
