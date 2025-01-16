using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Data;
using Unity.VisualScripting;

public class PlayerStatsUIManager : MonoBehaviour
{
    public PlayerStats playerStats;
    public GameObject statsPanel;

    [Header("Player Stats")]
    public TMP_Text playerMaxHealthText;
    public TMP_Text playerCurrentHealthText;
    public TMP_Text playerAttackDamageText;
    public TMP_Text playerLevelText;
    public TMP_Text playerCurrentXPText;
    public TMP_Text playerXPToLevelUpText;
    public TMP_Text playerMovementSpeedText;
    public TMP_Text playerBulletSpeedText;
    public TMP_Text playerFireRateText;

    private bool isStatsPanelActive;

    void Awake()
    {
        if(playerStats == null)
        {
            playerStats = FindObjectOfType<PlayerStats>();
        }

        statsPanel.SetActive(false);
    }

    void Update()
    {
        if(Time.timeScale !=0 && Input.GetKeyDown(KeyCode.B))
            ToggleStatsPanel();

        else if(Time.timeScale == 0 && isStatsPanelActive && Input.GetKeyDown(KeyCode.B))
            ToggleStatsPanel();
    }

    public void ToggleStatsPanel()
    {
        isStatsPanelActive = !isStatsPanelActive;
        statsPanel.SetActive(isStatsPanelActive);

        if(isStatsPanelActive)
        {
            UpdateStatusUI();
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void UpdateStatusUI()
    {
        playerMaxHealthText.text = "Max Health: " + playerStats.playerMaxHealth;
        playerCurrentHealthText.text = "Current Health: " + playerStats.playerCurrentHealth;
        playerAttackDamageText.text = "Attack Damage: " + playerStats.playerAttackDamage;
        playerLevelText.text = "Level: " + playerStats.playerLevel;
        playerCurrentXPText.text = "Current XP: " + playerStats.playerCurrentXP;
        playerXPToLevelUpText.text = "XP to Level Up: " + playerStats.playerXPToLevelUp;
        playerMovementSpeedText.text = "Movement Speed: " + playerStats.playerMovementSpeed;
        playerBulletSpeedText.text = "Bullet Speed: " + playerStats.playerBulletSpeed;
        playerFireRateText.text = "Fire Rate: " + playerStats.playerFireRate;
    }
}
