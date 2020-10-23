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

    public Sprite laserTurretImg;
    public Sprite missleTurretImg;
    public Sprite standardTurretImg;

    public int totalTurretNumber = 3;
    public int bagTurretMaximumNummer = 2;
    public List<int> selectedTurretIndex;

    // 当前选择的炮台（要建造的炮台）
    public TurretData selectedTurretData;
    public int currentTurretIndex;

    public GameObject mapCubePrefab;

    // 建造的数量
    public int mapCubeCnt;
    public int standardTurretCnt;
    public int missileTurretCnt;
    public int laserTurretCnt;

    private int mapCubeMoney;

    // Ture: show the range of build; Flase: donot show
    private bool showBuildRange;

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
        currentTurretIndex = 0;
        mapCubeCnt = 0;
        standardTurretCnt = 0;
        missileTurretCnt = 0;
        laserTurretCnt = 0;

        showBuildRange = true;

        selectedTurretIndex = new List<int>();
        selectedTurretIndex.Add(0);
        selectedTurretIndex.Add(1);

        mapCubeMoney = 20;
    }

    // 建造 mapcube （墙）
    public void BuildMapCube(Vector3 v)
    {
        bool flag = MoneyManager.Instance.UpdateMoney(-mapCubeMoney);
        if (!flag) return;

        GameObject newCube = GameObject.Instantiate(mapCubePrefab, v, Quaternion.identity);
        mapCubeCnt += 1;
    }

    // 在 mapcube 上造塔
    public void BuildTurret(MapCube mapCube)
    {
        Debug.Log(selectedTurretData.type);
        if (mapCube)
        {
            if (selectedTurretData != null && mapCube.turretGo == null)
            {
                bool flag = MoneyManager.Instance.UpdateMoney(-selectedTurretData.cost);
                if (!flag) return;

                mapCube.BuildTurret(selectedTurretData.turretPrefab);

                if (currentTurretIndex == 0)
                {
                    standardTurretCnt += 1;
                }
                else if (currentTurretIndex == 1)
                {
                    missileTurretCnt += 1;
                }
                else if (currentTurretIndex == 2)
                {
                    laserTurretCnt += 1;
                }
            }
            else
            {
                // TODO: 升级处理
            }
        }
    }

    // Swith the show of build range
    public void SwitchBuildRange()
    {
        showBuildRange = !showBuildRange;
        GameObject buildRange = GameObject.Find("Range");
        buildRange.GetComponent<Image>().enabled = showBuildRange;
    }

    // 切换要建造的塔类型
    // 0. 普通炮塔 1. 导弹塔 2. 激光塔
    public void switchBuildTurret(int flag)
    {
        GameObject button = GameObject.Find("Tower Switch");
        if (flag == 0)
        {
            selectedTurretData = standardTurretData;
            button.GetComponent<Button>().image.sprite = standardTurretImg;
        }
        else if (flag == 1)
        {
            selectedTurretData = missileTurretData;
            button.GetComponent<Button>().image.sprite = missleTurretImg;
        }
        else if (flag == 2)
        {
            selectedTurretData = laserTurretData;
            button.GetComponent<Button>().image.sprite = laserTurretImg;
        }
        else if (flag < 0)
        {
            selectedTurretData = null;
            button.GetComponent<Button>().image.sprite = null;
        }
        currentTurretIndex = flag;
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
