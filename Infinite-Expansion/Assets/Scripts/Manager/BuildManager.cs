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
    public TurretData selectedTurretData;

    public GameObject mapCubePrefab;
    private int buildTurretCount;

    // 单例
    private static BuildManager instance;

    public static BuildManager Instance
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
        // 初始化为普通炮台
        selectedTurretData = standardTurretData;
        buildTurretCount = 0;
    }

    // 建造 mapcube （墙）
    public void BuildMapCube(Vector3 v)
    {
        GameObject.Instantiate(mapCubePrefab, v, Quaternion.identity);
    }

    // 在 mapcube 上造塔
    public void BuildTurret(MapCube mapCube)
    {
        if (mapCube)
        {
            if (selectedTurretData != null && mapCube.turretGo == null)
            {
                bool flag = MoneyManager.Instance.UpdateMoney(-selectedTurretData.cost);
                if (!flag) return;

                mapCube.BuildTurret(selectedTurretData.turretPrefab);
                buildTurretCount += 1;
            }
            else
            {
                // TODO: 升级处理
            }
        }
    }

    public int getBuildTurretCount()
    {
        return buildTurretCount;
    }

    // 切换要建造的塔类型
    // 1. 普通炮塔 2. 导弹塔 3. 激光塔
    public void switchBuildTurret(int flag)
    {
        if (flag == 1)
        {
            selectedTurretData = standardTurretData;
        }
        else if (flag == 2)
        {
            selectedTurretData = missileTurretData;
        }
        else if (flag == 3)
        {
            selectedTurretData = laserTurretData;
        }
    }

    //private void Update()
    //{
    //    // 检测鼠标左键，
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        //// 没有选中的炮塔，直接返回
    //        //if (selectedTurretData == null) return;

    //        // 不在UI上，开发炮台的建造
    //        if (EventSystem.current.IsPointerOverGameObject() == false)
    //        {
    //            // 射线检测
    //            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //            RaycastHit hit;
    //            bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("MapCube"));

    //            if (isCollider)
    //            {
    //                MapCube mapCube = hit.collider.GetComponent<MapCube>();   // 得到点击的 mapCube
    //                if (selectedTurretData != null && mapCube.turretGo == null)
    //                {
    //                    // 还没建造过炮塔，且有选中的炮塔，才开始建造
    //                    if (money > selectedTurretData.cost)
    //                    {
    //                        //money -= selectedTurretData.cost;
    //                        ChangeMoney(-selectedTurretData.cost);
    //                        mapCube.BuildTurret(selectedTurretData.turretPrefab);
    //                    }
    //                    else
    //                    {
    //                        // TODO: 提示钱不够
    //                        moneyAnimator.SetTrigger("Flicker");
    //                    }
    //                }
    //                else
    //                {
    //                    // TODO: 升级处理
    //                }
    //            }
    //        }
    //    }
    //}

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
