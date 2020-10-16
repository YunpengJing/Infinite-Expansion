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
        selectedWeapon[0] = weapon1;
        selectedWeapon[1] = weapon2;

        HeroMove();
        //// camera follow
        //if (cameraMain)
        //{
        //    Vector3 destionation = new Vector3(transform.position.x, cameraMain.position.y, transform.position.z);
        //    cameraMain.position = Vector3.SmoothDamp(cameraMain.position, destionation, ref velocity, dampTime);
        //}
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

    #region Tower Build
    public void SelectAndBuild()
    {
        if (BuildManager.Instance.buildCube)
        {
            Vector3 hitPlanePoint = SelectPlane();
            if (hitPlanePoint.x == 0 && hitPlanePoint.y == 0 && hitPlanePoint.z == 0) return;

            LayerMask mask = LayerMask.GetMask("MapCube") | LayerMask.GetMask("Water") | LayerMask.GetMask("Enemy");
            RaycastHit hit;
            Debug.Log((int)mask);
            float[] x = { -2f, -1.5f, -1f, -0.5f, 0f, 0.5f, 1f, 1.5f, 2f};
            float[] z = { -2f, -1.5f, -1f, -0.5f, 0f, 0.5f, 1f, 1.5f, 2f};
            foreach(float i in x)
            {
                foreach(float j in z)
                {
                    Ray ray = new Ray(new Vector3(hitPlanePoint.x+i, hitPlanePoint.y+100f, hitPlanePoint.z+j), new Vector3(0f, -1f, 0f));
                    bool isCollider = Physics.Raycast(ray, out hit, 1000f, mask);
                    if (isCollider)
                    {
                        Debug.Log(hit.point);
                        Debug.Log(hit.collider.name);
                        return;
                    }
                }
            }
            BuildManager.Instance.BuildMapCube(hitPlanePoint);
        }
        else
        {
            MapCube mapCube = SelectCube();
            BuildManager.Instance.BuildTurret(mapCube);
        }
    }

    private Vector3 SelectPlane()
    {
        Vector2 mousePosition = heroInput.HeroAction.Select.ReadValue<Vector2>();

        // ray detection
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("Plane"));
        return hit.point;
    }

    private MapCube SelectCube()
    {
        Vector2 mousePosition = heroInput.HeroAction.Select.ReadValue<Vector2>();

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

    #endregion

    #region Tower Switch

    public void TowerSwitch()
    {
        if (BuildManager.Instance.selectedTurretIndex.Count < 2)
        {
            return;
        }
        else
        {
            int p = BuildManager.Instance.selectedTurretIndex.IndexOf(BuildManager.Instance.currentTurretIndex);
            p += 1;
            if (p == BuildManager.Instance.selectedTurretIndex.Count)
            {
                BuildManager.Instance.switchBuildTurret(BuildManager.Instance.selectedTurretIndex[0]);
            }
            else
            {
                BuildManager.Instance.switchBuildTurret(BuildManager.Instance.selectedTurretIndex[p]);
            }
        }
    }

    public void TowerCubeSwitch()
    {
        BuildManager.Instance.buildCube = !BuildManager.Instance.buildCube;
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