using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Player Stats")]
    public int playerMaxHealth = 100;
    private int playerCurrentHealth;


    [Header("Leveling System")]
    public int playerLevel = 1;
    public int currentXP = 0;
    public int xpToLevelUp = 100;


    void Start()
    {
        playerCurrentHealth = playerMaxHealth;
    }

    void Update()
    {
        if(currentXP >= xpToLevelUp)
        {
            LevelUp();
        }
    }

    public void TakeDamage(int damage)
    {
        playerCurrentHealth -= damage;
        if(playerCurrentHealth <= 0)
        {
            Die();
        }

        Debug.Log("Player health: " + playerCurrentHealth);
    }

    public void GainXP(int xp)
    {
        currentXP += xp;
        Debug.Log("Gained XP: " + xp + ", Total XP: " + currentXP);
    }

    void LevelUp()
    {
        playerLevel++;
        currentXP = 0;
        xpToLevelUp += 60;
        playerCurrentHealth += 20;
        Debug.Log("Level Up! Current Level: " + playerLevel);
    }

    void Die()
    {
        Debug.Log("Player died!");
    }
}
