using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterHurt : BehaviorNode
{
    public Material originalMaterial;  // ԭʼ����
    public Material hurtMaterial;      // ����ʱ�Ĳ���
    private Renderer enemyRenderer;    // ���ڸ��Ĳ��ʵ� Renderer
    public GameObject enemyBody;      // �������壨��ɫ���֣�
    public GameObject enemy;   
    private EnemyHealth enemyHealth;    // ���˱���


    private enemyIsTraggered enemyTriggerScript;
    

    // Start is called before the first frame update
    void Start()
    {
        
        
        enemyTriggerScript = enemy.GetComponent<enemyIsTraggered>();
        enemyHealth = enemy.gameObject.GetComponent<EnemyHealth>();
        
        // 初始化 EnemyHealth 的 maxHealth 和 currentHealth
        if (enemyHealth != null)
        {
            enemyHealth.maxHealth = 10;  // 设置最大血量为 10
            enemyHealth.currentHealth = 10;  // 设置当前血量为满血
            Debug.Log($"Enemy maxHealth initialized to: {enemyHealth.maxHealth}");
        }
        else
        {
            Debug.LogError("Enemy does not have the EnemyHealth component!");
        }
        // ��ȡ���˵� enemyIsTraggered �ű�
    
        //���˵�Renderer����������ɫ��
        enemyRenderer = enemyBody.GetComponent<Renderer>();
        if (enemyRenderer == null)
        {
            Debug.LogError("Enemy does not have a Renderer component!");
        }
        if (originalMaterial == null)
        {
            Debug.LogError("Original material is not assigned!");
        }

        // ����Ƿ���������˲���
        if (hurtMaterial == null)
        {
            Debug.LogError("Hurt material is not assigned!");
        }
        if (enemyTriggerScript == null)
        {
            Debug.LogError("Enemy does not have the enemyIsTraggered component!");
        }



    }

    // Update is called once per frame
    public override bool Run()
    {
        Debug.Log("isInBulletTrigger set to"+ enemyTriggerScript.isInBulletTrigger);
        Debug.Log("maxhealth"+ enemy.gameObject.GetComponent<EnemyHealth>().maxHealth);
        //Debug.Log("HurtBehavior is working");
        // ������������ٴ�ʩ����������ֻ�������߼�

        if (enemyRenderer != null && hurtMaterial != null)
        {
            enemyRenderer.material = hurtMaterial;
            Debug.Log("Enemy material changed to hurt material.");

            // ����Э�̣���һ��ʱ���ָ�ԭʼ����
            StartCoroutine(RestoreMaterialAfterDelay(0.1f)); // 0.1 ���ָ�����
        }

        enemyTriggerScript.isInBulletTrigger = false;
        
        return false;

    }


    private IEnumerator RestoreMaterialAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // �ȴ�ָ��ʱ��
        if (enemyRenderer != null && originalMaterial != null)
        {
            enemyRenderer.material = originalMaterial;
            Debug.Log("Enemy material restored to original.");
        }
    }


}
