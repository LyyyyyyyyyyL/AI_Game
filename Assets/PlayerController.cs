using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float mouseSensitivity = 100f;
    public float jumpForce = 5f;
    public float gravty = -19.6f;

    private float xRotation = 0f;
    private bool isGrounded = true; // �Ƿ��ڵ�����
    private Rigidbody rb;
    private bool isTakingDamage = false; // ����Ƿ����ڿ�Ѫ

    private PlayerHealth playerHealth; // ���� PlayerHealth �ű�

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0, gravty, 0);

        // ��ȡ PlayerHealth ���
        playerHealth = GetComponent<PlayerHealth>();
        if (playerHealth == null)
        {
            Debug.LogError("PlayerHealth script not found on the player object!");
        }
    }

    void Update()
    {
        // ��ɫ�ƶ�
        float horizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float vertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        transform.position += move;

        // �������ӽ�
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // ��Ծ
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
            if (!isTakingDamage && playerHealth != null) // ȷ��Э�̲����ظ�����
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
        isTakingDamage = true; // ������ڿ�Ѫ
        while (isTakingDamage) // ������Ѫ��ֱ������뿪��Һ����
        {
            playerHealth.TakeDamage(5f); // ÿ�ο�5��Ѫ
            yield return new WaitForSeconds(1f); // �ȴ�1��
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Venom")
        {
            isTakingDamage = false; // ���ֹͣ��Ѫ
            Debug.Log("Player left venom zone.");
        }
    }
}
