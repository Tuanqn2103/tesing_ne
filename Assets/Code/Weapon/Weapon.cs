using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePos;
    public float TimeBtvFire = 1f;
    public GameObject muzzle;
    public GameObject fireEffect;
    public float bulletForce;

    public AudioClip fireSound;
    private AudioSource audioSource;

    private float timeBtvFire;

    void Start()
    {
        // Get the AudioSource component attached to the same GameObject
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        RotateGun();
        timeBtvFire -= Time.deltaTime;
        if (Input.GetMouseButton(0) && timeBtvFire < 0)
        {
            FireBullet();
        }
    }

    void RotateGun()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 LookDir = mousePos - transform.position;
        float angle = Mathf.Atan2(LookDir.y, LookDir.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = rotation;

        if (transform.eulerAngles.z > 90 && transform.eulerAngles.z < 270)
        {
            transform.localScale = new Vector3(1, -1, 0);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 0);
        }
    }

    void FireBullet()
    {
        timeBtvFire = TimeBtvFire;

        // Instantiate bullet and effects
        GameObject bulletTmp = Instantiate(bullet, firePos.position, Quaternion.identity);
        Instantiate(muzzle, firePos.position, transform.rotation, transform);
        Instantiate(fireEffect, firePos.position, transform.rotation, transform);

        // Add force to the bullet
        Rigidbody2D rb = bulletTmp.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * bulletForce, ForceMode2D.Impulse);

        // Play the fire sound
        PlayFireSound();
    }

    void PlayFireSound()
    {
        if (audioSource != null && fireSound != null)
        {
            audioSource.PlayOneShot(fireSound);
        }
    }
}
