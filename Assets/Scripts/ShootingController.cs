using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    public PlayerMovement playerMovement;

    public Transform firePoint;
    [Header("Bullet Settings")]
    public float bulletSpeed = 10f;
    public float fireRate = 0.5f;

    private float nextFireTime = 0f;

    private BulletPool bulletPool;

    void Start()
    {
        if (playerMovement == null)
        {
            playerMovement = GetComponent<PlayerMovement>();
        }
        
        bulletPool = FindObjectOfType<BulletPool>();
    }

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            FireBullet();
            nextFireTime = Time.time + fireRate;
        }
    }

    void FireBullet()
    {
        GameObject bullet = bulletPool.GetBullet();
        bullet.transform.position = firePoint.position;

        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursorPosition.z = 0;

        Vector2 direction = (cursorPosition - firePoint.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);


        // Vector2 direction = playerMovement.IsFacingRight() ? firePoint.right : -firePoint.right;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();    
        rb.velocity  = direction * bulletSpeed;
    }
}

