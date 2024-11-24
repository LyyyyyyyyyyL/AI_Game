using UnityEngine;

public class RunBehavior : BehaviorNode
{
    public override bool Run()
    {
        Debug.Log("Running...");
        return true; // 返回任务完成状态
    }
}
