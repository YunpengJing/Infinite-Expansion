using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroMessage : MonoBehaviour
{
    public TurretData turretData1;
    public TurretData turretData2;
    public TurretData turretData3;

    private bool[] turretSelected;
    private TurretData[] turretAvailable;
    public int towerMaximum = 3;

    // Start is called before the first frame update
    void Start()
    {
        turretSelected = new bool[3];
        turretAvailable = new TurretData[3];
        turretAvailable[0] = turretData1;
        turretAvailable[1] = turretData2;
        turretAvailable[2] = turretData3;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < turretSelected.Length; i++)
        {
            turretSelected[i] = false;
        }
        for (int i = 0; i < TopDownController.selectedTurretData.Length; i++)
        {
            TurretData td = TopDownController.selectedTurretData[i];
            if (td == null)
                break;
            if (td.type == turretData1.type)
            {
                turretSelected[i] = true;
            }
        }

        for (int i = 0; i < turretSelected.Length; i++)
        {
            if (turretSelected[0])
            {
                GameObject.Find("Tower" + i.ToString()).GetComponent<Image>().color = Color.red;
            }
            else
            {
                GameObject.Find("Tower" + i.ToString()).GetComponent<Image>().color = Color.white;
            }
        }

            
    }

    public void TowerSwap(int k)
    {
        for (int i = 0; i < TopDownController.selectedTurretData.Length; i++)
        {
            if (TopDownController.selectedTurretData[i] == null)
            {
                TopDownController.selectedTurretData[i] = turretAvailable[k];
                return;
            }
            if (turretAvailable[k].type == TopDownController.selectedTurretData[i].type)
            {
                while (i < TopDownController.selectedTurretData.Length - 1)
                {
                    TopDownController.selectedTurretData[i] = TopDownController.selectedTurretData[i + 1];
                    i++;
                }
                TopDownController.selectedTurretData[i] = null;
                Debug.Log(TopDownController.selectedTurretData[0]);
                GameObject.Find("Tower" + k.ToString()).GetComponent<Image>().color = Color.white;
                return;
            }

        }
    }
}
