using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Weapons")]
    public GameObject pistol;
    public GameObject shotgun;
    public GameObject rifle;

    public Transform pistolFirePoint;
    public Transform shotgunFirePoint;
    public Transform rifleFirePoint;
    public Transform currentFirePoint;

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

        if(weapon == pistol)
        {
            currentFirePoint = pistolFirePoint;
        }
        else if(weapon == shotgun)
        {
            currentFirePoint = shotgunFirePoint;
        }
        else if(weapon == rifle)
        {
            currentFirePoint = rifleFirePoint;
        }
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

        if(Time.timeScale == 0)
            return;

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
