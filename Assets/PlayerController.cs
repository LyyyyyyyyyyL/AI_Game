using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float mouseSensitivity = 100f;
    public float jumpForce = 5f;
    public float gravty = -19.6f;

    private float xRotation = 0f;
    private bool isGrounded = true; // Whether the player is on the ground
    private Rigidbody rb;
    private bool isTakingDamage = false; // Flag to indicate if the player is taking damage

    private PlayerHealth playerHealth; // Reference to the PlayerHealth script

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0, gravty, 0);

        // Get the PlayerHealth component
        playerHealth = GetComponent<PlayerHealth>();
        if (playerHealth == null)
        {
            Debug.LogError("PlayerHealth script not found on the player object!");
        }
    }

    void Update()
    {
        // Character movement
        float horizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float vertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        transform.position += move;

        // Mouse control for camera view
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // Jump
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
            if (!isTakingDamage && playerHealth != null) // Ensure coroutine doesn't run multiple times
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
        isTakingDamage = true; // Mark that the player is taking damage
        while (isTakingDamage) // Continuously take damage while in the venom zone
        {
            playerHealth.TakeDamage(0.5f); // Deduct 0.5 health points per tick
            yield return new WaitForSeconds(1f); // Wait for 1 second between damage ticks
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Venom")
        {
            isTakingDamage = false; // Mark to stop taking damage
            Debug.Log("Player left venom zone.");
        }
    }
}
