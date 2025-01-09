using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    [Header("Player Stats")]
    public int playerMaxHealth = 100;
    private int playerCurrentHealth;


    [Header("Leveling System")]
    public int playerLevel = 1;
    public int currentXP = 0;
    public int xpToLevelUp = 20;

    [Header("UI Elements")]
    public Image xpFillImage;
    public Image hpFillImage;
    public TextMeshProUGUI levelText;

    private UpgradeManager upgradeManager;

    void Start()
    {
        upgradeManager = FindObjectOfType<UpgradeManager>();
        playerCurrentHealth = playerMaxHealth;
        levelText.text = playerLevel.ToString();
        UpdateHPBar();
        UpdateXPBar();
    }

    void Update()
    {
        if(currentXP >= xpToLevelUp)
        {
            LevelUp();
        }
    }

    void UpdateXPBar()
    {
        float fillAmount = (float)currentXP / xpToLevelUp;
        xpFillImage.fillAmount = fillAmount;
        if(xpFillImage.fillAmount == 1)
        {
            xpFillImage.fillAmount = 0;
        }
    }

    void UpdateHPBar()
    {
        float fillAmount = (float)playerCurrentHealth / playerMaxHealth;
        hpFillImage.fillAmount = fillAmount;
    }



    public void TakeDamage(int damage)
    {
        playerCurrentHealth -= damage;
        UpdateHPBar();
        if(playerCurrentHealth <= 0)
        {
            Die();
        }

        Debug.Log("Player health: " + playerCurrentHealth);
    }

    public void GainXP(int xp)
    {
        currentXP += xp;
        UpdateXPBar();
        Debug.Log("Gained XP: " + xp + ", Total XP: " + currentXP);
    }

    // Level up the player
    void LevelUp()
    {
        playerLevel++;
        xpToLevelUp += 21;
        levelText.text = playerLevel.ToString();
        upgradeManager.ShowUpgradeOptions();
        // currentXP = 0;
        // xpToLevelUp += 60;
        // playerCurrentHealth += 20;
        Debug.Log("Level Up! Current Level: " + playerLevel);
    }

    void Die()
    {
        Debug.Log("Player died!");
    }
}
