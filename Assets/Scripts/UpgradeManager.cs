using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    [System.Serializable]
    public class UpgradeOption
    {
        public string name;
        public string description;
        public Sprite icon;
        public System.Action effect;
    }

    public List<UpgradeOption> allUpgrades;
    public Button[] upgradeButtons;
    public GameObject upgradePanel;

    void Start()
    {
        upgradePanel.SetActive(false);
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
