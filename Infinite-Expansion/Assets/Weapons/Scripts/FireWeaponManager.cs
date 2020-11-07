using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class FireWeaponManager : MonoBehaviour
{
    public int update = 0;

    private float timer;

    private ParticleSystem gunParticles;

    private GameObject fireInstance=null;

    //引用
    public GameObject firePrefab;

    public GameObject fireStormPrefab;

    private HeroInputManager heroInput;

    // 子弹数量
    public int currentAmmo = 30;
    public int totalAmmo = 100;
    public const int MagazineCapacity = 30;

    // UI系统
    private Text uiCurrentAmmo;
    private Text uiTotalAmmo;


    private void Awake()
    {
        heroInput = new HeroInputManager();
    }

    private void OnEnable()
    {
        heroInput.Enable();
    }

    private void OnDisable()
    {
        heroInput.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        gunParticles = gameObject.GetComponent<ParticleSystem>();
        uiCurrentAmmo = GameObject.Find("CurrentAmmo").GetComponent<Text>();
        uiTotalAmmo = GameObject.Find("TotalAmmo").GetComponent<Text>();
        uiCurrentAmmo.text = currentAmmo.ToString();
        uiTotalAmmo.text = totalAmmo.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        float shootButton = heroInput.HeroAction.Shoot.ReadValue<float>();

        if (update==1 && shootButton>0 && timer>1f)
        {

            Shoot();
            timer = 0f;
        }

        if (update==0 && shootButton > 0 && fireInstance==null)
        {
            Shoot();
        }
        else if(shootButton<=0)
        {
            if(fireInstance!=null)
            {
                Destroy(fireInstance, 0.5f);
            }
            
        }
    }

    void Shoot()
    {
        // 更新子弹数量
        currentAmmo = update == 0 ? currentAmmo - 1 : currentAmmo - 3;
        if (currentAmmo < 0)
        {
            bool hasAmmo = Reloaded();
            if (!hasAmmo)
            {
                currentAmmo = 0;
                return;
            }
        }

        //更新UI
        uiCurrentAmmo.text = currentAmmo.ToString();

        // 计时器清0
        timer = 0;
        // 开启灯光

        // 开启粒子系统
        gunParticles.Stop();
        gunParticles.Play();

        //生成子弹
        
        Vector3 facing = transform.rotation * Vector3.right;

        if (update == 0)
        {
            fireInstance = Instantiate(firePrefab, transform);
            fireInstance.transform.position = transform.position + facing * (-2);
        }
        else if(update==1)
        {
            GameObject fireStormIns = Instantiate(fireStormPrefab, transform.position, Quaternion.Euler(transform.eulerAngles));
        }
    }

    bool Reloaded()
    {
        if (totalAmmo == 0)
            return false;
        currentAmmo = totalAmmo - MagazineCapacity >= 0 ? MagazineCapacity : totalAmmo;
        totalAmmo = totalAmmo - currentAmmo >= 0 ? totalAmmo - currentAmmo : 0;
        // 更新UI
        uiTotalAmmo.text = totalAmmo.ToString();
        return true;
    }
}
