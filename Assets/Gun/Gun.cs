using UnityEngine;

public class Gun : MonoBehaviour
{
    public AudioSource audioSource;  // AudioSource component for playing sounds
    public AudioClip shootSound;     // Audio clip for the shooting sound
    public GameObject bulletPrefab;  // Bullet prefab
    public Transform firePoint;      // The point where the bullet is fired from
    public float bulletSpeed = 10f;  // Bullet speed


    void Start()
    {
        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();
    }

    // Fire bullet each time the button is pressed
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))  // When left mouse button or other assigned input is pressed
        {
            FireBullet();
            PlayShootSound();
            //print("fire bullet");
        }
    }

    public void FireBullet()
    {
        // Instantiate the bullet
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Apply speed to the bullet
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = firePoint.forward * bulletSpeed;
        }
    }

    void PlayShootSound()
    {
        // Play the shooting sound
        audioSource.PlayOneShot(shootSound);
        Debug.Log("shootSound");
    }
}
