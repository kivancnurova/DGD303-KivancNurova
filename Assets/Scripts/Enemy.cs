using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Basic Enemy Stats")]
    public int enemyHealth;
    public int enemyDamage;
    public float enemySpeed;
    private int enemyCurrentHealth;

    private Transform player;
    private Rigidbody2D rb;
    
    private bool isCollidingWithPlayer = false;

    [Header("XP Info")]
    public int xpGiveOnDeath;

    private SpriteRenderer spriteRenderer;


    void Start() 
    {
        enemyCurrentHealth = enemyHealth;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (player == null)
        {
            Debug.LogError("Player not found!");
        }

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if(player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * enemySpeed * Time.deltaTime;
        }

        FlipEnemy();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && !isCollidingWithPlayer)
        {
            isCollidingWithPlayer = true;
            PlayerManager playerManager = collision.gameObject.GetComponent<PlayerManager>();
            if(playerManager != null)
            {
                playerManager.TakeDamage(enemyDamage);
            }
        }
    }

    void OnCollisionExit2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            isCollidingWithPlayer = false;
        }
    }

    public void TakeDamage(int damage)
    {
        enemyCurrentHealth -= damage;
        if(enemyCurrentHealth <= 0)
        {
            Die();
        }

        Debug.Log("Enemy health: " + enemyCurrentHealth);
    }

    void Die()
    {
        PlayerManager playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        if(playerManager != null)
        {
            playerManager.GainXP(xpGiveOnDeath);
        }
        Destroy(gameObject);
    }

    void FlipEnemy()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        if(direction.x >0)
        {
            spriteRenderer.flipX = false;
        }
        else if(direction.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }
}
