using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    public Transform firePoint;
    [Header("Bullet Settings")]
    public float bulletSpeed = 10f;
    public float fireRate = 0.5f;

    private float nextFireTime = 0f;

    private BulletPool bulletPool;

    void Start()
    {
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
        rb.velocity = firePoint.up * bulletSpeed;
    }
}

