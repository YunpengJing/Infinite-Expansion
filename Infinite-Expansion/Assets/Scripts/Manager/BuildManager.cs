using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
    public TurretData laserTurretData;
    public TurretData missileTurretData;
    public TurretData standardTurretData;

    // 当前选择的炮台（要建造的炮台）
    public static TurretData selectedTurretData;

    public Text moneyText;

    public Animator moneyAnimator;

    private int money = 1000;

    void ChangeMoney(int change = 0)
    {
        money += change;
        moneyText.text = "$ " + money;
    }

    private void Update()
    {
        // 检测鼠标左键，
        if (Input.GetMouseButtonDown(0))
        {
            //// 没有选中的炮塔，直接返回
            //if (selectedTurretData == null) return;

            // 不在UI上，开发炮台的建造
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                // 射线检测
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("MapCube"));

                if (isCollider)
                {
                    MapCube mapCube = hit.collider.GetComponent<MapCube>();   // 得到点击的 mapCube
                    if (selectedTurretData != null && mapCube.turretGo == null)
                    {
                        // 还没建造过炮塔，且有选中的炮塔，才开始建造
                        if (money > selectedTurretData.cost)
                        {
                            //money -= selectedTurretData.cost;
                            ChangeMoney(-selectedTurretData.cost);
                            mapCube.BuildTurret(selectedTurretData.turretPrefab);
                        }
                        else
                        {
                            // TODO: 提示钱不够
                            moneyAnimator.SetTrigger("Flicker");
                        }
                    }
                    else
                    {
                        // TODO: 升级处理
                    }
                }
            }
        }


    }

    public void OnLaserSelected(bool isOn)
    {
        if (selectedTurretData != laserTurretData)
        {
            selectedTurretData = laserTurretData;
        } else
        {
            selectedTurretData = null;
        }
    }

    public void OnMissileSelected(bool isOn)
    {
        if (selectedTurretData != missileTurretData)
        {
            selectedTurretData = missileTurretData;
        }
        else
        {
            selectedTurretData = null;
        }
    }

    public void OnStandardSelected(bool isOn)
    {
        if (selectedTurretData != standardTurretData)
        {
            selectedTurretData = standardTurretData;
        }
        else
        {
            selectedTurretData = null;
        }
    }
}
