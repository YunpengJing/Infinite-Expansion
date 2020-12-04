using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class FireWeaponManager : MonoBehaviour
{
    private int update = 0;

    private float timer;

    //喷火的最长时间
    private float timer2;

    private ParticleSystem gunParticles;

    private GameObject fireInstance = null;

    //引用
    public GameObject firePrefab;

    public GameObject fireStormPrefab;

    private HeroInputManager heroInput;

    // 子弹数量
    private BulletNumberManager bulletNumberManager;

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
        bulletNumberManager = GameObject.Find("WeaponManager").GetComponent<BulletNumberManager>();
        uiCurrentAmmo = GameObject.Find("CurrentAmmo").GetComponent<Text>();
        uiTotalAmmo = GameObject.Find("TotalAmmo").GetComponent<Text>();
        uiCurrentAmmo.text = bulletNumberManager.FireCurrentAmmo.ToString();
        uiTotalAmmo.text = bulletNumberManager.FireTotalAmmo.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        timer2 += Time.deltaTime;

        float shootButton = heroInput.HeroAction.Shoot.ReadValue<float>();

        if (update == 1 && shootButton > 0 && timer > 1f)
        {

            Shoot();
            timer = 0f;
        }

        if (update == 0 && shootButton > 0 && fireInstance == null)
        {
            Shoot();
        }
        else if (shootButton <= 0 || timer2>2f)
        {
            if(fireInstance!=null)
            {
                Destroy(fireInstance, 0.5f);
                timer2 = 0;
            }
            
        }
    }

    void Shoot()
    {
        // 更新子弹数量
        bulletNumberManager.FireCurrentAmmo = update == 0 ? bulletNumberManager.FireCurrentAmmo - 1 : bulletNumberManager.FireCurrentAmmo - 3;
        if (bulletNumberManager.FireCurrentAmmo < 0)
        {
            bool hasAmmo = Reloaded();
            if (!hasAmmo)
            {
                bulletNumberManager.FireCurrentAmmo = 0;
                return;
            }
        }

        //更新UI
        uiCurrentAmmo.text = bulletNumberManager.FireCurrentAmmo.ToString();

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
        if (bulletNumberManager.FireTotalAmmo == 0)
            return false;
        bulletNumberManager.FireCurrentAmmo = bulletNumberManager.FireTotalAmmo - bulletNumberManager.FireCapacity >= 0 ? bulletNumberManager.FireCapacity : bulletNumberManager.FireTotalAmmo;
        bulletNumberManager.FireTotalAmmo = bulletNumberManager.FireTotalAmmo - bulletNumberManager.FireCurrentAmmo >= 0 ? bulletNumberManager.FireTotalAmmo - bulletNumberManager.FireCurrentAmmo : 0;
        // 更新UI
        uiTotalAmmo.text = bulletNumberManager.FireTotalAmmo.ToString();
        return true;
    }

    public void updator()
    {
        update = 1;
    }
}
