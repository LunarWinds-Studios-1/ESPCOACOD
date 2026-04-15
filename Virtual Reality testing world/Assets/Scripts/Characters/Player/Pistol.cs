using UnityEngine;
using UnityEngine.XR.OpenXR.NativeTypes;

public class Pistol : MonoBehaviour
{

    public GameObject bulletPrefab;

    public Transform firePoint;
    public float bulletSpeed = 20f;
    public float bulletLifeTime = 5f;

    public AudioClip clip;
    public AudioSource source;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void FireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.linearVelocity = firePoint.forward * bulletSpeed;
        }

        source.PlayOneShot(clip);

        Destroy(bullet, bulletLifeTime);
    }
}
