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
        bullet.transform.rotation = firePoint.rotation;

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        Vector2 direction = playerMovement.IsFacingRight() ? firePoint.right : -firePoint.right;
    
        rb.velocity  = direction * bulletSpeed;
    }
}

