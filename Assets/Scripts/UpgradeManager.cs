using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    public PlayerStats playerStats;
    public PlayerManager playerManager;

    [Header("Upgrade Amounts")]
    public int playerAttackDamageUpgradeAmount;
    public int playerMaxHealthUpgradeAmount;
    public int playerRegenerationUpgradeAmount;
    public float playerMovementSpeedUpgradeAmount;
    public float playerFireRateUpgradeAmount;
    public float playerBulletSpeedUpgradeAmount;
    public int playerXPToLevelUpUpgradeAmount;
 

    [System.Serializable]
    public class UpgradeOption
    {
        public string name;
        public string description;
        public Sprite icon;
        public System.Action effect;
    }

    [Header("Upgrade Options")]
    public List<UpgradeOption> allUpgrades;
    public Button[] upgradeButtons;
    public GameObject upgradePanel;

    void Awake() 
    {
        if(playerStats == null)
        {
            playerStats = FindObjectOfType<PlayerStats>();
        }
        if(playerManager == null)
        {
            playerManager = FindObjectOfType<PlayerManager>();
        }
    }

    void Start()
    {
        upgradePanel.SetActive(false);

        allUpgrades = new List<UpgradeOption>
        {
            new UpgradeOption
            {
                name = "Attack Damage",
                description = "Increases Player Attack Damage:" + playerAttackDamageUpgradeAmount,
                effect = () => playerStats.playerAttackDamage += playerAttackDamageUpgradeAmount
            },
            new UpgradeOption
            {
                name = "Max Health",
                description = "Increases Player Max Health:" + playerMaxHealthUpgradeAmount,
                effect = () => 
                {
                playerStats.playerMaxHealth += playerMaxHealthUpgradeAmount;
                playerManager.UpdateHPBar();
                }
            },
            new UpgradeOption
            {
                name = "Regeneration",
                description = "Regenerate" + playerRegenerationUpgradeAmount + "Health Instantly",
                effect = () =>
                {
                    if(playerStats.playerCurrentHealth + playerRegenerationUpgradeAmount > playerStats.playerMaxHealth)
                        playerStats.playerCurrentHealth = playerStats.playerMaxHealth;
                    else
                    playerStats.playerCurrentHealth += playerRegenerationUpgradeAmount;
                }
            },
            new UpgradeOption
            {
                name = "Movement Speed",
                description = "Increases Player Movement Speed:" + playerMovementSpeedUpgradeAmount,
                effect = () => playerStats.playerMovementSpeed += playerMovementSpeedUpgradeAmount
            },
            new UpgradeOption
            {
                name = "Attack Speed",
                description = "Increases Player Attack Speed",
                effect = () => playerStats.playerFireRate -= playerFireRateUpgradeAmount
            },
            new UpgradeOption
            {
                name = "Bullet Speed",
                description = "Increases player bullet speed",
                effect = () => playerStats.playerBulletSpeed += playerBulletSpeedUpgradeAmount
            },
            new UpgradeOption
            {
                name = "Exp Ratio",
                description = "Decreases the amount of XP needed to level up",
                effect = () => 
                {
                playerStats.playerXPToLevelUp -= playerXPToLevelUpUpgradeAmount;
                playerManager.UpdateXPBar();
                }
            }
        };
    }

    public void ShowUpgradeOptions()
    {
        Time.timeScale = 0;
        upgradePanel.SetActive(true);

        List<UpgradeOption> randomUpgrades = GetRandomUpgrades(3);

        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            int index = i;
            Button button = upgradeButtons[i];
            UpgradeOption upgrade = randomUpgrades[i];

            button.GetComponentInChildren<TextMeshProUGUI>().text = upgrade.name;
            button.GetComponent<Image>().sprite = upgrade.icon;

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() =>
            {
                ApplyUpgrade(upgrade);
                upgradePanel.SetActive(false);
                Time.timeScale = 1;
            });
        }
    }

    public void ApplyUpgrade(UpgradeOption upgrade)
    {
        Debug.Log("Applied upgrade: " + upgrade.name);
        upgrade.effect?.Invoke();
    }

    private List<UpgradeOption> GetRandomUpgrades(int count)
    {
        List<UpgradeOption> randomUpgrades = new List<UpgradeOption>();
        List<UpgradeOption> availableUpgrades = new List<UpgradeOption>(allUpgrades);

        for (int i = 0; i < count; i++)
        {
            if(availableUpgrades.Count == 0) break;

            int randomIndex = Random.Range(0, availableUpgrades.Count);
            randomUpgrades.Add(availableUpgrades[randomIndex]);
            availableUpgrades.RemoveAt(randomIndex);

        }

        return randomUpgrades;
    }
}
