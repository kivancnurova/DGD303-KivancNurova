using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Weapons")]
    public GameObject pistol;
    public GameObject shotgun;
    public GameObject rifle;

    public GameObject currentWeapon;

    void Start() 
    {
        SetWeapon(pistol);
    }

    public void SetWeapon(GameObject weapon)
    {
        if(currentWeapon != null)
        {
            currentWeapon.SetActive(false);
        }

        currentWeapon = weapon;
        currentWeapon.SetActive(true);
    }

    void SwitchToShotgun()
    {
        SetWeapon(shotgun);
    }

    void SwitchToRifle()
    {
        SetWeapon(rifle);
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetWeapon(pistol);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetWeapon(shotgun);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetWeapon(rifle);
        }
    }
}
