using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtBehavior : BehaviorNode
{
    public Material originalMaterial;  // 原始材质
    public Material hurtMaterial;      // 受伤时的材质
    private Renderer enemyRenderer;    // 用于更改材质的 Renderer
    public GameObject enemyBody;      // 敌人身体（变色部分）
    public GameObject enemy;      // 敌人本人


    private enemyIsTraggered enemyTriggerScript;
    

    // Start is called before the first frame update
    void Start()
    {
        

        // 获取敌人的 enemyIsTraggered 脚本
        enemyTriggerScript = enemy.GetComponent<enemyIsTraggered>();
        //敌人的Renderer用来更换颜色的
        enemyRenderer = enemyBody.GetComponent<Renderer>();
        if (enemyRenderer == null)
        {
            Debug.LogError("Enemy does not have a Renderer component!");
        }
        if (originalMaterial == null)
        {
            Debug.LogError("Original material is not assigned!");
        }

        // 检查是否分配了受伤材质
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
        //Debug.Log("HurtBehavior is working");
        // 你可以在这里再次施加力，或者只做其他逻辑

        if (enemyRenderer != null && hurtMaterial != null)
        {
            enemyRenderer.material = hurtMaterial;
            Debug.Log("Enemy material changed to hurt material.");

            // 启动协程，在一定时间后恢复原始材质
            StartCoroutine(RestoreMaterialAfterDelay(0.1f)); // 0.1 秒后恢复材质
        }

        enemyTriggerScript.isInBulletTrigger = false;
        
        return false;

    }


    private IEnumerator RestoreMaterialAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // 等待指定时间
        if (enemyRenderer != null && originalMaterial != null)
        {
            enemyRenderer.material = originalMaterial;
            Debug.Log("Enemy material restored to original.");
        }
    }


}
