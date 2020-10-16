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
            GameObject.Find("Tower" + i.ToString()).GetComponent<Image>().color = Color.red;
        }
    }

    public void TowerSwap(int k)
    {
        if (BuildManager.Instance.selectedTurretIndex.Contains(k))
        {
            // cancal a tower from the bag
            if (BuildManager.Instance.currentTurretIndex == k)
            {
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
}
