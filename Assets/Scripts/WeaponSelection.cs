using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponSelection : MonoBehaviour
{
    public PlayerStats playerStats;
    public WeaponManager weaponManager;
    public GameObject weaponSelectionPanel;
    public Button shotgunButton;
    public Button rifleButton;
    public int shotgunAttackDamageBuff;
    public float shotgunAttackSpeedDebuff;
    public float shotgunMovementSpeedDebuff;
    public float rifleAttackSpeedBuff;
    public float rifleMovementSpeedBuff;
    public int rifleAttackDamageDebuff;

    void Awake() 
    {
        if(playerStats == null)
        {
            playerStats = FindObjectOfType<PlayerStats>();
        }
        
        if(weaponManager == null)
        {
            weaponManager = FindObjectOfType<WeaponManager>();
        }
        
    }
    void Start() 
    {
        weaponSelectionPanel.SetActive(false);

        shotgunButton.onClick.AddListener(() => SelectWeapon("Shotgun"));
        rifleButton.onClick.AddListener(() => SelectWeapon("Rifle"));
    }

    public void ShowWeaponSelection()
    {
        Time.timeScale = 0;
        weaponSelectionPanel.SetActive(true);
    }

    private void SelectWeapon(string weaponType)
    {
        
        if(weaponType == "Shotgun")
        {
            weaponManager.SetWeapon(weaponManager.shotgun);
        }
        else if(weaponType == "Rifle")
        {
            weaponManager.SetWeapon(weaponManager.rifle);
        }

        weaponSelectionPanel.SetActive(false);
        Time.timeScale = 1;
    }

}
