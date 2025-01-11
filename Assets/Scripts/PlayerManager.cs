using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public PlayerStats playerStats;
    public UpgradeManager upgradeManager;
    
    [Header("UI Elements")]
    public Image xpFillImage;
    public Image hpFillImage;
    public TextMeshProUGUI levelText;


    void Awake() 
    {
        if(playerStats == null)
        {
            playerStats = FindObjectOfType<PlayerStats>();
        }
    }


    void Start()
    {
        upgradeManager = FindObjectOfType<UpgradeManager>();
        playerStats.playerCurrentHealth = playerStats.playerMaxHealth;
        levelText.text = playerStats.playerLevel.ToString();
        UpdateHPBar();
        UpdateXPBar();
    }

    void Update()
    {
        if(playerStats.playerCurrentXP >= playerStats.playerXPToLevelUp)
        {
            LevelUp();
        }
    }

    public void UpdateXPBar()
    {
        float fillAmount = (float)playerStats.playerCurrentXP / playerStats.playerXPToLevelUp;
        xpFillImage.fillAmount = fillAmount;
        if(xpFillImage.fillAmount == 1)
        {
            xpFillImage.fillAmount = 0;
        }
    }

    public void UpdateHPBar()
    {
        float fillAmount = (float)playerStats.playerCurrentHealth / playerStats.playerMaxHealth;
        hpFillImage.fillAmount = fillAmount;
    }



    public void TakeDamage(int damage)
    {
        playerStats.playerCurrentHealth -= damage;
        UpdateHPBar();
        if(playerStats.playerCurrentHealth <= 0)
        {
            Die();
        }

        Debug.Log("Player health: " + playerStats.playerCurrentHealth);
    }

    public void GainXP(int xp)
    {
        playerStats.playerCurrentXP += xp;
        UpdateXPBar();
        Debug.Log("Gained XP: " + xp + ", Total XP: " + playerStats.playerCurrentXP);
    }

    // Level up the player
    void LevelUp()
    {
        playerStats.playerLevel++;
        playerStats.playerXPToLevelUp += 21;
        levelText.text = playerStats.playerLevel.ToString();
        upgradeManager.ShowUpgradeOptions();
        Debug.Log("Level Up! Current Level: " + playerStats.playerLevel);
    }

    void Die()
    {
        Debug.Log("Player died!");
    }
}
