﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectManager : MonoBehaviour
{
    public GameObject weapon0;
    public GameObject weapon1;
    public GameObject weapon2;
    public GameObject weapon3;
    public List<GameObject> weapons;

    public Sprite weapon0Img;
    public Sprite weapon1Img;
    public Sprite weapon2Img;
    public Sprite weapon3Img;
    public List<Sprite> weaponImgs;

    bool[] weaponsLocked = { false, false, false, true};
    bool[] weaponsUpdated = { false, false, false, false };
    int[] moneyTOUnlock = { 0, 0, 0, 100 };
    int[] moneyToUpdate = { 100, 100, 100, 100000 };

    public List<int> selectedWeaponIndex;

    public int totalWeaponNumber = 4;
    public int bagWeaponMaximumNummer = 3;
    public int currentWeaponIndex;

    private static WeaponSelectManager instance;

    private GameObject countable;
    private GameObject bulletSlider;

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

    public void Start()
    {
        countable = GameObject.Find("Countable");
        bulletSlider = GameObject.Find("BulletSlider");
        bulletSlider.SetActive(false);
    }

    public void Awake()
    {
        Instance = this;

        currentWeaponIndex = 0;

        selectedWeaponIndex = new List<int>();
        selectedWeaponIndex.Add(0);
        selectedWeaponIndex.Add(1);
        selectedWeaponIndex.Add(2);
        //selectedWeaponIndex.Add(3);

        weapons = new List<GameObject>();
        weapons.Add(weapon0);
        weapons.Add(weapon1);
        weapons.Add(weapon2);
        weapons.Add(weapon3);
        weaponImgs = new List<Sprite>();
        weaponImgs.Add(weapon0Img);
        weaponImgs.Add(weapon1Img);
        weaponImgs.Add(weapon2Img);
        weaponImgs.Add(weapon3Img);

        bagWeaponMaximumNummer = 3;
    }

    // switch the current weapon to the k weapon
    public void SwitchWeapon(int k)
    {
        if (!Unlocked(k)) // not unlocked
        {
            return;
        }
        if (!selectedWeaponIndex.Contains(k))
        {
            return;
        }
        else
        {
            int from = currentWeaponIndex;
            int to = k;

            if (to == 1)
            {
                // 切换到狙击枪
                countable.SetActive(false);
                bulletSlider.SetActive(true);
            }
            else
            {
                countable.SetActive(true);
                bulletSlider.SetActive(false);
            }

            //GameObject oldWeapon = transform.Find("Weapon").gameObject;
            GameObject oldWeapon = GameObject.Find("Weapon");
            GameObject newWeapon = GameObject.Instantiate(weapons[to]);
            //newWeapon.transform.parent = transform;
            newWeapon.transform.parent = oldWeapon.transform.parent;
            newWeapon.transform.localPosition = oldWeapon.transform.localPosition;
            newWeapon.transform.rotation = oldWeapon.transform.rotation;
            Destroy(oldWeapon);
            newWeapon.name = "Weapon";

            // update the weapon
            if (weaponsUpdated[k])
            {
                if (k == 0)
                {
                    GameObject head = GameObject.Find("Bullet_Gun_Head");
                    head.GetComponent<BulletWeaponManager>().updator();
                }
                else if (k == 1)
                {
                    GameObject head = GameObject.Find("Gun_Head");
                    head.GetComponent<WeaponManager>().updator();
                }
                else if (k == 2)
                {
                    GameObject head = GameObject.Find("Fire_Gun_Head");
                    head.GetComponent<FireWeaponManager>().updator();
                }
            }
            

            currentWeaponIndex = k;

            GameObject button = GameObject.Find("Weapon Switch");
            button.GetComponent<Button>().image.sprite = weaponImgs[k];
        }
    }

    public bool Unlocked(int k)
    {
        return !weaponsLocked[k];
    }

    // if success unlock return true
    // else return false
    public bool Unlock(int k)
    {
        if (Unlocked(k))    // already unlocked
        {
            return false;
        }
        else
        {
            bool flag = MoneyManager.Instance.UpdateMoney(-moneyTOUnlock[k]);
            if (!flag) return false;
            else
            {
                weaponsLocked[k] = false;
                return true;
            }
        }
    }

    public bool UpdateWeapon(int k)
    {
        if (weaponsUpdated[k] == true) return false;
        bool flag = MoneyManager.Instance.UpdateMoney(-moneyToUpdate[k]);
        if (!flag) return false;
        else
        {
            weaponsUpdated[k] = true;

            if (k == 0)
            {
                GameObject head = GameObject.Find("Bullet_Gun_Head");
                head.GetComponent<BulletWeaponManager>().updator();
            }
            else if (k == 1)
            {
                GameObject head = GameObject.Find("Gun_Head");
                head.GetComponent<WeaponManager>().updator();
            }
            else if (k == 2)
            {
                GameObject head = GameObject.Find("Fire_Gun_Head");
                head.GetComponent<FireWeaponManager>().updator();
            }

            return true;
        }
    }
}
