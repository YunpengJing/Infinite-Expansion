using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class BulletWeaponManager : MonoBehaviour
{
    public float damage = 1f;
    public float timeBetweenBullet = 0.15f;

    //升级 0 单发；1 三散弹
    private int update = 0;

    private float timer;

    // 子弹数量
    private BulletNumberManager bulletNumberManager;

    // UI系统
    private Text uiCurrentAmmo;
    private Text uiTotalAmmo;

    private ParticleSystem gunParticles;

    private AudioSource gunAudio;

    //引用
    public GameObject bulletPrefab;

    private HeroInputManager heroInput;

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
        gunAudio = GetComponent<AudioSource>();
        bulletNumberManager = GameObject.Find("WeaponManager").GetComponent<BulletNumberManager>();
        uiCurrentAmmo = GameObject.Find("CurrentAmmo").GetComponent<Text>();
        uiTotalAmmo = GameObject.Find("TotalAmmo").GetComponent<Text>();
        uiCurrentAmmo.text = bulletNumberManager.bulletGunCurrentAmmo.ToString();
        uiTotalAmmo.text = bulletNumberManager.bulletGunTotalAmmo.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        //if (Input.GetButton("Fire2") && timer >= timeBetweenBullet)
        float shootButton = heroInput.HeroAction.Shoot.ReadValue<float>();
        if (shootButton > 0 && timer >= timeBetweenBullet)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // 更新子弹数量
        bulletNumberManager.bulletGunCurrentAmmo = update == 0 ? bulletNumberManager.bulletGunCurrentAmmo - 1 : bulletNumberManager.bulletGunCurrentAmmo - 1;
        if (bulletNumberManager.bulletGunCurrentAmmo < 0)
        {
            bool hasAmmo = Reloaded();
            if (!hasAmmo)
            {
                bulletNumberManager.bulletGunCurrentAmmo = 0;
                return;
            }
        }

        //更新UI
        uiCurrentAmmo.text = bulletNumberManager.bulletGunCurrentAmmo.ToString();

        // 计时器清0
        timer = 0;
        // 开启灯光

        // 播放声音
        gunAudio.Play();
        // 画线

        // 开启粒子系统
        gunParticles.Stop();
        gunParticles.Play();

        //生成子弹
        if(update==0)
        {
            GameObject bulletInstance=Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles));
            Destroy(bulletInstance, 2f);
        }
        else if(update==1)
        {
            Vector3 angle1 = new Vector3(0, -30, 0);
            Vector3 angle2 = new Vector3(0, 30, 0);
            GameObject bulletInstance1 = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles));
            GameObject bulletInstance2 = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles+angle1));
            GameObject bulletInstance3 = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles+angle2));

            Destroy(bulletInstance1, 2f);
            Destroy(bulletInstance2, 2f);
            Destroy(bulletInstance3, 2f);
        }

        


        // 测试
        //Debug.Log(currentAmmo);
    }

    bool Reloaded()
    {
        if (bulletNumberManager.bulletGunTotalAmmo == 0)
            return false;
        bulletNumberManager.bulletGunCurrentAmmo = bulletNumberManager.bulletGunTotalAmmo - bulletNumberManager.bulletCapacity >= 0 ? bulletNumberManager.bulletCapacity : bulletNumberManager.bulletGunTotalAmmo;
        bulletNumberManager.bulletGunTotalAmmo = bulletNumberManager.bulletGunTotalAmmo - bulletNumberManager.bulletGunCurrentAmmo >= 0 ? bulletNumberManager.bulletGunTotalAmmo - bulletNumberManager.bulletGunCurrentAmmo : 0;
        // 更新UI
        uiTotalAmmo.text = bulletNumberManager.bulletGunTotalAmmo.ToString();
        return true;
    }

    public void updator()
    {
        update = 1;
    }
}
