using UnityEngine;

// 定义抽象行为节点类
public abstract class BehaviorNode : MonoBehaviour
{
    public abstract bool Run(); // 行为逻辑
}
