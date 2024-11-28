using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float mouseSensitivity = 100f;
    public float jumpForce = 5f;
    public float gravty = -19.6f;

    private float xRotation = 0f;
    private bool isGrounded = true; // 是否在地面上
    private Rigidbody rb;
    private bool isTakingDamage = false; // 标记是否正在扣血

    private PlayerHealth playerHealth; // 引用 PlayerHealth 脚本

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0, gravty, 0);

        // 获取 PlayerHealth 组件
        playerHealth = GetComponent<PlayerHealth>();
        if (playerHealth == null)
        {
            Debug.LogError("PlayerHealth script not found on the player object!");
        }
    }

    void Update()
    {
        // 角色移动
        float horizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float vertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        transform.position += move;

        // 鼠标控制视角
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // 跳跃
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Ground")
        {
            isGrounded = true;
        }
        else if (other.transform.tag == "Venom")
        {
            isGrounded = true;
            if (!isTakingDamage && playerHealth != null) // 确保协程不会重复运行
            {
                StartCoroutine(TakeDamageOverTime());
            }
        }
        else
        {
            isGrounded = false;
        }
    }

    IEnumerator TakeDamageOverTime()
    {
        isTakingDamage = true; // 标记正在扣血
        while (isTakingDamage) // 持续扣血，直到玩家离开毒液区域
        {
            playerHealth.TakeDamage(5f); // 每次扣5点血
            yield return new WaitForSeconds(1f); // 等待1秒
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Venom")
        {
            isTakingDamage = false; // 标记停止扣血
            Debug.Log("Player left venom zone.");
        }
    }
}
