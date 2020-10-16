using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelectManager : MonoBehaviour
{
    public GameObject weapon0;
    public GameObject weapon1;
    public GameObject weapon2;
    public List<GameObject> weapons;

    public int totalWeaponNumber = 3;
    public int bagWeaponMaximumNumber = 2;
    public List<int> selectedWeaponIndex;

    public int currentWeaponIndex;

    private static WeaponSelectManager instance;

    public static WeaponSelectManager Instance
    {
        get
        {
            return instance;
        }

        set
        {
            instance = value;
        }
    }

    public void Awake()
    {
        Instance = this;

        currentWeaponIndex = 0;

        selectedWeaponIndex = new List<int>();
        selectedWeaponIndex.Add(0);
        selectedWeaponIndex.Add(1);

        weapons = new List<GameObject>();
        weapons.Add(weapon0);
        weapons.Add(weapon1);
        weapons.Add(weapon2);
    }

    // switch the current weapon to the k weapon
    public void SwitchWeapon(int k)
    {
        if (!selectedWeaponIndex.Contains(k))
        {
            return;
        }
        else
        {
            int from = currentWeaponIndex;
            int to = k;
            
            //GameObject oldWeapon = transform.Find("Weapon").gameObject;
            GameObject oldWeapon = GameObject.Find("Weapon");
            GameObject newWeapon = GameObject.Instantiate(weapons[to]);
            //newWeapon.transform.parent = transform;
            newWeapon.transform.parent = oldWeapon.transform.parent;
            newWeapon.transform.localPosition = oldWeapon.transform.localPosition;
            newWeapon.transform.rotation = oldWeapon.transform.rotation;
            Destroy(oldWeapon);
            newWeapon.name = "Weapon";

            currentWeaponIndex = k;
        }
    }
}
