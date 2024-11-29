using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Transform hinge; // 门的旋转点
    public float openAngle = 100f; // 打开门的角度
    public float openSpeed = 2f; // 打开门的时间（秒）
    public bool isOpen = false; // 当前门是否是打开状态

    private Quaternion closedRotation; // 门的初始关闭角度
    private Quaternion openRotation; // 门的打开目标角度
    private float rotateProgress = 0f; // 旋转进度

    void Start()
    {
        // 确保 hinge 已设置，否则使用当前门的 Transform
        if (hinge == null)
        {
            hinge = transform;
        }

        // 保存初始关闭状态的旋转
        closedRotation = hinge.localRotation;

        // 计算打开状态的目标旋转
        openRotation = Quaternion.Euler(hinge.localEulerAngles + new Vector3(0, openAngle, 0));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // 按下 E 键切换门的开关状态
        {
            isOpen = !isOpen;
        }

        // 根据门的状态插值旋转
        if (isOpen)
        {
            rotateProgress += Time.deltaTime / openSpeed;
        }
        else
        {
            rotateProgress -= Time.deltaTime / openSpeed;
        }

        // 限制进度在 0 和 1 之间
        rotateProgress = Mathf.Clamp01(rotateProgress);

        // 更新门的旋转
        hinge.localRotation = Quaternion.Lerp(closedRotation, openRotation, rotateProgress);
    }
}