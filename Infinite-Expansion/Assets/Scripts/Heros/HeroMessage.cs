using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroMessage : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        TowerSelectedShow();
        WeaponSelectedShow();
    }





    #region Tower
    private void TowerSelectedShow()
    {
        // show the selected tower
        for (int i = 0; i < BuildManager.Instance.totalTurretNumber; i++)
        {
            GameObject.Find("Tower" + i.ToString()).GetComponent<Image>().color = Color.white;
        }
        foreach (int i in BuildManager.Instance.selectedTurretIndex)
        {
            GameObject.Find("Tower" + i.ToString()).GetComponent<Image>().color = Color.Lerp( Color.green, Color.yellow, 0.3f);
        }
    }

    public void TowerSwap(int k)
    {
        if (BuildManager.Instance.selectedTurretIndex.Contains(k))
        {
            // cancal a tower from the bag
            if (BuildManager.Instance.currentTurretIndex == k)
            {
                if (BuildManager.Instance.selectedTurretIndex.Count == 1) return;
                    // the tower to cancel is the current tower
                    int p = BuildManager.Instance.selectedTurretIndex.IndexOf(k);
                BuildManager.Instance.selectedTurretIndex.RemoveAt(p);
                // cancel the last one
                if (BuildManager.Instance.selectedTurretIndex.Count == p)
                {
                    if (p == 0)
                    {
                        BuildManager.Instance.switchBuildTurret(-1);
                    }
                    else
                    {
                        BuildManager.Instance.switchBuildTurret(BuildManager.Instance.selectedTurretIndex[0]);
                    }
                }
                else
                {
                    BuildManager.Instance.switchBuildTurret(BuildManager.Instance.selectedTurretIndex[p]);
                }
            }
            else
            {
                BuildManager.Instance.selectedTurretIndex.Remove(k);
            }
        }
        else
        {
            if (BuildManager.Instance.selectedTurretIndex.Count >= BuildManager.Instance.bagTurretMaximumNummer)
            {
                // bag is full, can not select 
                return;
            }
            else
            {
                // still space in bag, do select
                if (BuildManager.Instance.selectedTurretIndex.Count == 0)
                {
                    BuildManager.Instance.switchBuildTurret(k);
                }
                BuildManager.Instance.selectedTurretIndex.Add(k);
            }
        }
    }
    #endregion

    #region Weapon
    private void WeaponSelectedShow()
    {
        for (int i = 0; i < WeaponSelectManager.Instance.totalWeaponNumber; i++)
        {
            GameObject.Find("Weapon" + i.ToString()).GetComponent<Image>().color = Color.white;
        }
        foreach (int i in WeaponSelectManager.Instance.selectedWeaponIndex)
        {
            GameObject.Find("Weapon" + i.ToString()).GetComponent<Image>().color = Color.green;
        }
    }

    public void WeaponSwap(int k)
    {
        // cancal a weapon from the bag
        if (WeaponSelectManager.Instance.selectedWeaponIndex.Contains(k))
        {
            // the tower to cancel is the current tower
            if (WeaponSelectManager.Instance.currentWeaponIndex == k)
            {
                // select only one weapon
                if (WeaponSelectManager.Instance.selectedWeaponIndex.Count == 1)
                {
                    // need to select at least one weapon
                    return;
                }
                int p = WeaponSelectManager.Instance.selectedWeaponIndex.IndexOf(k);
                WeaponSelectManager.Instance.selectedWeaponIndex.RemoveAt(p);
                // cancel the last one
                if (WeaponSelectManager.Instance.selectedWeaponIndex.Count == p)
                {
                    WeaponSelectManager.Instance.SwitchWeapon(WeaponSelectManager.Instance.selectedWeaponIndex[0]);
                    WeaponSelectManager.Instance.currentWeaponIndex = WeaponSelectManager.Instance.selectedWeaponIndex[0];
                }
                else
                {
                    WeaponSelectManager.Instance.SwitchWeapon(WeaponSelectManager.Instance.selectedWeaponIndex[p]);
                    WeaponSelectManager.Instance.currentWeaponIndex = WeaponSelectManager.Instance.selectedWeaponIndex[p];
                }
            }
            else
            {
                WeaponSelectManager.Instance.selectedWeaponIndex.Remove(k);
            }
        }
        else
        {
            // bag is full, can not select 
            if (WeaponSelectManager.Instance.selectedWeaponIndex.Count >= WeaponSelectManager.Instance.bagWeaponMaximumNummer)
            {
                Debug.LogError(WeaponSelectManager.Instance.bagWeaponMaximumNummer);
                Debug.LogError(WeaponSelectManager.Instance.selectedWeaponIndex);
                return;
            }
            // still space in bag, do select
            else
            { 
                WeaponSelectManager.Instance.selectedWeaponIndex.Add(k);
            }
        }
    }
    #endregion
}
