using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TopDownController : MonoBehaviour
{

    [SerializeField]
    private float heroSpeed = 5.0f;

    private CharacterController controller;
    private HeroInputManager heroInput;

    //private Transform cameraMain;
    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        //cameraMain = Camera.main.transform;
        selectedTurretData = new TurretData[selectableTurretCount];
        selectedTurrentIndex = 0;

        selectedWeapon = new GameObject[selectableWeaponCount];
        selectedWeaponIndex = 0;
    }

    private void Awake()
    {
        heroInput = new HeroInputManager();
        controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        heroInput.Enable();
    }

    private void OnDisable()
    {
        heroInput.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        selectedTurretData[0] = turretData1;
        selectedTurretData[1] = turretData2;
        selectedTurretData[2] = turretData3;

        selectedWeapon[0] = weapon1;
        selectedWeapon[1] = weapon2;

        HeroMove();
        //// camera follow
        //if (cameraMain)
        //{
        //    Vector3 destionation = new Vector3(transform.position.x, cameraMain.position.y, transform.position.z);
        //    cameraMain.position = Vector3.SmoothDamp(cameraMain.position, destionation, ref velocity, dampTime);
        //}

        // tower build
        float BuildButton = heroInput.HeroAction.Build.ReadValue<float>();
        if (BuildButton > 0)
        {
            Ray ray = new Ray(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(0f, -1f, 0f));
            RaycastHit hit;
            bool isCollider = Physics.Raycast(ray, out hit, 1000f, LayerMask.GetMask("MapCube"));
            if (isCollider)
            {
                MapCube mapCube = hit.collider.GetComponent<MapCube>();   // 得到点击的 mapCube
                if (selectedTurretData != null && mapCube.turretGo == null)
                {
                    mapCube.BuildTurret(selectedTurretData[selectedTurrentIndex].turretPrefab);
                }
                else
                {
                    // TODO: 升级处理
                }
            }
        }

    }


    #region Hero Move

    private void HeroMove()
    {
        // hero move
        Vector2 moveInput = heroInput.HeroAction.Move.ReadValue<Vector2>();
        Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y);
        controller.Move(move * Time.deltaTime * heroSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
    }

    #endregion

    #region Tower

    public static TurretData[] selectedTurretData;
    public TurretData turretData1;
    public TurretData turretData2;
    public TurretData turretData3;
    public int selectableTurretCount = 3;
    private static int selectedTurrentIndex = 0;

    #region Tower Build
    public void SelectAndBuild()
    {
        MapCube mapCube = Select();
        //Build(mapCube);
        BuildManager.Instance.BuildTurret(mapCube);
    }

        //public void SelectAndBuild()
       // {
        //    MapCube mapCube = Select();
        //    Build(mapCube);
       // }

        private MapCube Select()
        {
            Vector2 mousePosition = heroInput.HeroAction.Select.ReadValue<Vector2>();
            Debug.Log(mousePosition);

            // ray detection
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit hit;
            bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("MapCube"));
            if (isCollider)
            {
                return hit.collider.GetComponent<MapCube>();
            }
            else
            {
                return null;
            }
        }

        private void Build(MapCube mapCube)
        {
            if (mapCube)
            {
                if (selectedTurretData != null && mapCube.turretGo == null)
                {
                    bool flag = MoneyManager.Instance.UpdateMoney(-selectedTurretData[selectedTurrentIndex].cost);
                    if (!flag) return;

                    Debug.Log(selectedTurrentIndex);
                    mapCube.BuildTurret(selectedTurretData[selectedTurrentIndex].turretPrefab);
                }
                else
                {
                    // TODO: 升级处理
                }
            }
        }

    #endregion

    #region Tower Switch

    public void TowerSwitch()
    {

        selectedTurrentIndex = selectedTurrentIndex + 1;
        if (selectedTurrentIndex == selectableTurretCount)
            selectedTurrentIndex = 0;
        if (selectedTurretData[selectedTurrentIndex] == null)
            selectedTurrentIndex = 0;
    }

    #endregion

    #endregion

    #region Weapon

    public static GameObject[] selectedWeapon;
    public GameObject weapon1;
    public GameObject weapon2;
    public int selectableWeaponCount = 2;
    private static int selectedWeaponIndex = 0;

    #region Weapon Switch

    public void WeaponSwitch()
    {
        int from = selectedWeaponIndex;
        int to = from + 1;
        if (to == selectableWeaponCount)
            to = 0;
        if (selectedWeapon[to] == null)
            to = 0;
        if (from == to)
            return;
        selectedWeaponIndex = to;
        
        GameObject oldWeapon = transform.Find("Weapon").gameObject;
        GameObject newWeapon = GameObject.Instantiate(selectedWeapon[to]);
        newWeapon.transform.parent = transform;
        newWeapon.transform.localPosition = oldWeapon.transform.localPosition;
        newWeapon.transform.rotation = oldWeapon.transform.rotation;
        Destroy(oldWeapon);
        newWeapon.name = "Weapon";

    }

    #endregion

    #endregion
}