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

    public static TurretData selectedTurretData;
    public TurretData laserTurretData;
    public TurretData missileTurretData;
    public TurretData standardTurretData;


    // Start is called before the first frame update
    void Start()
    {
        //cameraMain = Camera.main.transform;
        selectedTurretData = standardTurretData;
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
                    Debug.Log(mapCube.name);
                    mapCube.BuildTurret(selectedTurretData.turretPrefab);
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


    #region Tower Build

    public void SelectAndBuild()
    {
        MapCube mapCube = Select();
        Build(mapCube);
    }

    private MapCube Select()
    {
        Debug.Log("selected triggered");
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
                bool flag = MoneyManager.Instance.UpdateMoney(-selectedTurretData.cost);
                if (!flag) return;

                Debug.Log(mapCube.name);
                mapCube.BuildTurret(selectedTurretData.turretPrefab);
            }
            else
            {
                // TODO: 升级处理
            }
        }
    }

    #endregion
}