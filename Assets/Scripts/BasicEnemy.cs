using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    [Header("Basic Enemy Stats")]
    public int basicEnemyHealth = 75;
    public int basicEnemyDamage = 20;
    public float basicEnemySpeed = 2f;
    private int basicEnemyCurrentHealth;

    private Transform player;
    private Rigidbody2D rb;
    
    private bool isCollidingWithPlayer = false;

    [Header("XP Info")]
    public int xpGiveOnDeath = 20;

    private SpriteRenderer spriteRenderer;


    void Start() 
    {
        basicEnemyCurrentHealth = basicEnemyHealth;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (player == null)
        {
            Debug.LogError("Player not found!");
        }

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        if(player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * basicEnemySpeed * Time.deltaTime;
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
                playerManager.TakeDamage(basicEnemyDamage);
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
        basicEnemyCurrentHealth -= damage;
        if(basicEnemyCurrentHealth <= 0)
        {
            Die();
        }

        Debug.Log("Enemy health: " + basicEnemyCurrentHealth);
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
