using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Slider healthSlider;  // ��Ѫ�� Slider
    public Transform headPosition;  // �󶨵���ͷ����λ��
    public float maxHealth = 100f;  // ���Ѫ��
    private float currentHealth;  // ��ǰѪ��

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    void Update()
    {
        // ��Ѫ������ͷ��λ��
        healthSlider.transform.position = Camera.main.WorldToScreenPoint(headPosition.position + new Vector3(0, 2, 0));  // +2��Yƫ������ȷ��Ѫ����ͷ�Ϸ�

        // ʾ������ M ������Ѫ��
        if (Input.GetKeyDown(KeyCode.M))
        {
            currentHealth -= 10f;
            if (currentHealth < 0) currentHealth = 0;
            healthSlider.value = currentHealth;
        }
    }
}
