using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBulletController : MonoBehaviour
{
    public float lifetime;
    private float timeElapsed = 0f;
    public PlayerStats playerStats;

    void OnEnable()
    {
        timeElapsed = 0f;
        if (playerStats == null)
        {
            playerStats = FindObjectOfType<PlayerStats>();
        }
    }


    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= lifetime)
        {
            ReturnToPool();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(playerStats.playerAttackDamage);
            }
            ReturnToPool();
        }
    }

    void ReturnToPool()
    {
        BulletPool pool = FindObjectOfType<BulletPool>();
        pool.ReturnBullet(gameObject);
    }
}
