using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    public PlayerStats playerStats;
    public WeaponManager weaponManager;
    public Transform firePoint;
    private float nextFireTime = 0f;

    private BulletPool bulletPool;


    void Awake() 
    {
        if (playerStats == null)
            playerStats = FindObjectOfType<PlayerStats>();
    }

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
            nextFireTime = Time.time + playerStats.playerFireRate;
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
        rb.velocity  = direction * playerStats.playerBulletSpeed;
    }
}

