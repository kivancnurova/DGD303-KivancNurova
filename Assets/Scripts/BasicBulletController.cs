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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            BasicEnemy enemy = collision.gameObject.GetComponent<BasicEnemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(playerStats.playerDamage);
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
