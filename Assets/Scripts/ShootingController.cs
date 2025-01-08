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
    public WeaponManager weaponManager;
    void Start()
    {
        if (weaponManager == null)
            weaponManager = FindObjectOfType<WeaponManager>();


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
        bullet.transform.position = weaponManager.currentFirePoint.position;

        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursorPosition.z = 0;

        Vector2 direction = (cursorPosition - weaponManager.currentFirePoint.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();    
        rb.velocity  = direction * bulletSpeed;
    }
}

