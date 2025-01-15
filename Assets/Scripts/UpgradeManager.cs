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
    public GameObject upgradePanel;

    [Header("Upgrade Slots")]
    public GameObject[] upgradeSlots; // Upgrade1, Upgrade2, Upgrade3 gibi GameObjectler
    public TMP_Text[] upgradeNames; // UpgradeName1, UpgradeName2, UpgradeName3
    public Image[] upgradeImages; // UpgradeImage1, UpgradeImage2, UpgradeImage3
    public TMP_Text[] upgradeDescriptions; // UpgradeDescription1, UpgradeDescription2, UpgradeDescription3
    public Button[] upgradeButtons; // UpgradeButton1, UpgradeButton2, UpgradeButton3

    void Awake()
    {
        if (playerStats == null)
        {
            playerStats = FindObjectOfType<PlayerStats>();
        }
        if (playerManager == null)
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
                description = "Increases Player Attack Damage: " + playerAttackDamageUpgradeAmount,
                icon = Resources.Load<Sprite>("Icons/AttackDamageIcon"),
                effect = () => playerStats.playerAttackDamage += playerAttackDamageUpgradeAmount
            },
            new UpgradeOption
            {
                name = "Max Health",
                description = "Increases Player Max Health: " + playerMaxHealthUpgradeAmount + " and heals 15 health instantly",
                icon = Resources.Load<Sprite>("Icons/MaxHealthIcon"),
                effect = () => 
                {
                playerStats.playerMaxHealth += playerMaxHealthUpgradeAmount;
                playerStats.playerCurrentHealth += 15;
                playerManager.UpdateHPBar();
                }
            },
            new UpgradeOption
            {
                name = "Regeneration",
                description = "Regenerate " + playerRegenerationUpgradeAmount + " Health Instantly",
                icon = Resources.Load<Sprite>("Icons/RegenerationIcon"),
                effect = () =>
                {
                    if(playerStats.playerCurrentHealth + playerRegenerationUpgradeAmount > playerStats.playerMaxHealth)
                        playerStats.playerCurrentHealth = playerStats.playerMaxHealth;
                    else
                    playerStats.playerCurrentHealth += playerRegenerationUpgradeAmount;
                    playerManager.UpdateHPBar();
                }
            },
            new UpgradeOption
            {
                name = "Movement Speed",
                description = "Increases Player Movement Speed: " + playerMovementSpeedUpgradeAmount,
                icon = Resources.Load<Sprite>("Icons/MovementSpeedIcon"),
                effect = () => playerStats.playerMovementSpeed += playerMovementSpeedUpgradeAmount
            },
            new UpgradeOption
            {
                name = "Attack Speed",
                description = "Increases Player Attack Speed",
                icon = Resources.Load<Sprite>("Icons/AttackSpeedIcon"),
                effect = () => playerStats.playerFireRate -= playerFireRateUpgradeAmount
            },
            new UpgradeOption
            {
                name = "Bullet Speed",
                description = "Increases player bullet speed: " + playerBulletSpeedUpgradeAmount,
                icon = Resources.Load<Sprite>("Icons/BulletSpeedIcon"),
                effect = () => playerStats.playerBulletSpeed += playerBulletSpeedUpgradeAmount
            },
            new UpgradeOption
            {
                name = "Exp Ratio",
                description = "Decreases the amount of XP needed to level up",
                icon = Resources.Load<Sprite>("Icons/ExpRatioIcon"),
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

        List<UpgradeOption> randomUpgrades = GetRandomUpgrades(upgradeSlots.Length);

        for (int i = 0; i < upgradeSlots.Length; i++)
        {
            UpgradeOption upgrade = randomUpgrades[i];

            // Update UI elements for each slot
            upgradeNames[i].text = upgrade.name;
            upgradeImages[i].sprite = upgrade.icon;
            upgradeDescriptions[i].text = upgrade.description;

            // Set button behavior
            Button button = upgradeButtons[i];
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
            if (availableUpgrades.Count == 0) break;

            int randomIndex = Random.Range(0, availableUpgrades.Count);
            randomUpgrades.Add(availableUpgrades[randomIndex]);
            availableUpgrades.RemoveAt(randomIndex);
        }

        return randomUpgrades;
    }
}
