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
        MapCube mapCube = Select();
        BuildManager.Instance.BuildTurret(mapCube);
    }

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

    #endregion

    #region Tower Switch

    public void TowerSwitch()
    {
        BuildManager.Instance.switchBuildTurret(2);
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