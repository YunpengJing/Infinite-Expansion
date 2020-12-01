using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    private float timer;

    public ParticleSystem gunParticlesss;

    private AudioSource gunAudio;

    public GameObject[] gunLines;

    private HeroInputManager heroInput;

    private bool isCoolingDown = false; // 是否正在冷却
    public float coolingTimer; // 冷却时间
    public int maxNumberOfBullet; // 最大弹夹容量
    private int currentNumberOfBullet; // 当前子弹数量
    public AudioSource coolingAudio; // 冷却声音

    public float maxChargingTimer; // 最大充能时间
    private float currentDemageRatio; // 当前充能弹伤害比例
    private bool isCharging = false; // 是否正在冲能
    public AudioSource chargeAudio; // 充能声音
    private float charingTimer; // 当前充能时间
    public float maxScale; // 子弹最大放大比例

    private Slider bulletSlider; // 当前子弹数量UI

    public int update = 0; // 枪械升级

    // Start is called before the first frame update
    void Start()
    {
        // gunParticles = GetComponent<ParticleSystem>();
        gunAudio = GetComponent<AudioSource>();
        currentNumberOfBullet = maxNumberOfBullet;
        bulletSlider = GameObject.Find("BulletSlider").GetComponent<Slider>();
        if (bulletSlider != null)
            bulletSlider.value = 1f;
    }

    void Awake()
    {
        heroInput = new HeroInputManager();
    }

    void OnEnable()
    {
        heroInput.Enable();
    }

    void OnDisable()
    {
        heroInput.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        float shootButton = heroInput.HeroAction.Shoot.ReadValue<float>();
        if (shootButton > 0)
        {
            if (currentNumberOfBullet > 0  && !isCoolingDown && !isCharging)
            {
                Debug.Log("start charging");
                StartCharing();
            }
            else if (currentNumberOfBullet > 0 && !isCoolingDown && isCharging)
            {
                Debug.Log("charging");
                Charging();
            }
            else if (currentNumberOfBullet == 0 && !isCoolingDown)
            {
                // 触发reloading
                Reloading();
                Debug.Log("reloading");
            }
            else if (isCoolingDown && timer >= coolingTimer)
            {
                Reloaded();
            }
            else if(isCoolingDown && timer < coolingTimer)
            {
                // 正在冷却
                // 更新UI
                if (bulletSlider != null)
                    bulletSlider.value = timer / coolingTimer;
            }
        }
        else
        {
            // 鼠标松开
            if (isCharging && currentNumberOfBullet > 0 && !isCoolingDown)
            {
                Charged();
            } else if (isCoolingDown && timer < coolingTimer)
            {
                // 更新UI
                if (bulletSlider != null)
                    bulletSlider.value = timer / coolingTimer;
            } else if (isCoolingDown && timer >= coolingTimer)
            {
                // 冷却完毕
                Reloaded();
            }
        }
    }

    void Shoot(float currentDemageRatio)
    {
        // 计时器清0
        timer = 0;
        // 开启灯光

        // 播放声音
        gunAudio.Play();
        
        // 画线
        if (gunLines != null)
        {
            GameObject gunLine = null; 
            if (currentDemageRatio > 0 && currentDemageRatio <= 0.33)
            {
                gunLine = gunLines[0];
            } else if (currentDemageRatio <= 0.66)
            {
                gunLine = gunLines[1];
            }
            else
            {
                gunLine = gunLines[2];
            }

            Bullet bullet = gunLine.GetComponent<Bullet>();
            float maxDamege = bullet.maxDamage;
            float newDamege = maxDamege * currentDemageRatio;
            Vector3 newScale = new Vector3(this.currentDemageRatio * maxScale, this.currentDemageRatio * maxScale, 10);
            gunLine.transform.localScale = newScale;
            gunLine.GetComponent<Bullet>().currentDamage = update == 0 ? newDamege : 2 * newDamege;
            Vector3 fixAngle = new Vector3(90f, 0f, 0f);
            GameObject gunLineInstance = Instantiate(gunLine, transform.position, Quaternion.Euler(transform.eulerAngles + fixAngle));
            Debug.Log("current demage is :" + newDamege);
            Destroy(gunLineInstance, 2f);
        }
        // 开启粒子系统
        gunParticlesss.Stop();
        gunParticlesss.Play();
        // 子弹减1
        currentNumberOfBullet--;
        // 更新子弹UI
        if (bulletSlider != null)
            bulletSlider.value = (float)currentNumberOfBullet / maxNumberOfBullet;
        // 测试
        // Debug.Log("aaaa");
    }

    void Reloading()
    {
        // 重置充能计时器
        timer = 0;
        // 播放充能声音
        coolingAudio.Play();
        // 正在冷却中
        isCoolingDown = true;
    }

    void Reloaded()
    {
        // 充能完毕
        currentNumberOfBullet = maxNumberOfBullet;
        if (bulletSlider != null)
            bulletSlider.value = 1.0f;
        isCoolingDown = false;
        Debug.Log("reload finished");
        coolingAudio.Stop();
    }

    void StartCharing()
    {
        chargeAudio.Play();
        charingTimer = 0;
        isCharging = true;
    }

    void Charging()
    {
        if (charingTimer > maxChargingTimer)
        {
            //Charged();
        }
        else
        {
            charingTimer += Time.deltaTime;
        }
    }

    void Charged()
    {
        chargeAudio.Stop();
        currentDemageRatio = charingTimer / maxChargingTimer > 1 ? 1 : charingTimer / maxChargingTimer;
        isCharging = false;
        Shoot(currentDemageRatio);
        Debug.Log("launch lazy; ratio: " + currentDemageRatio);
        charingTimer = 0;
    }
}
