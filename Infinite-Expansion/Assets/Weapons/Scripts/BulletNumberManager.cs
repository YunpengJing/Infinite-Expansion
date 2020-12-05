using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletNumberManager : MonoBehaviour
{

    public int bulletGunTotalAmmo = 30;
    public int bulletGunCurrentAmmo = 30;
    public int bulletCapacity = 30;

    public int RPGTotalAmmo = 30;
    public int RPGCurrentAmmo = 6;
    public int RPGCapacity = 6;

    public int FireTotalAmmo = 30;
    public int FireCurrentAmmo = 6;
    public int FireCapacity = 6;

    // UI系统
    private Text uiCurrentAmmo;
    private Text uiTotalAmmo;

    // Start is called before the first frame update
    void Start()
    {
        uiCurrentAmmo = GameObject.Find("CurrentAmmo").GetComponent<Text>();
        uiTotalAmmo = GameObject.Find("TotalAmmo").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void BuyBullet()
    {
        bool flag = MoneyManager.Instance.UpdateMoney(-100);
        if (!flag) return;
        else
        {   // succeed buy 
            bulletGunTotalAmmo = 60;
            RPGTotalAmmo = 30;
            FireTotalAmmo = 30;
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        uiTotalAmmo.text = bulletGunTotalAmmo.ToString();
    }
}
