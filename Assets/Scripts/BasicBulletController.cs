using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBulletController : MonoBehaviour
{
    public float lifetime;
    private float timeElapsed = 0f;
    public int basicBulletDamage = 25;

    void OnEnable()
    {
        timeElapsed = 0f;
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
                enemy.TakeDamage(basicBulletDamage);
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
