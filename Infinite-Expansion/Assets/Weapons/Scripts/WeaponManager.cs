using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.PlayerLoop;

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

    // Start is called before the first frame update
    void Start()
    {
        // gunParticles = GetComponent<ParticleSystem>();
        gunAudio = GetComponent<AudioSource>();
        currentNumberOfBullet = maxNumberOfBullet;
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
            else
            {
                // 正在充能
                //Debug.Log("charging");
            }
        }
        else
        {
            // 鼠标松开
            if (isCharging && currentNumberOfBullet > 0 && !isCoolingDown)
            {
                Charged();
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
            GameObject gunLineInstance = Instantiate(gunLine, transform.position, new Quaternion(0, 0, 0, 1), transform);
            Debug.Log("current demage is :" + newDamege);
            Destroy(gunLineInstance, 2f);
        }
        // 开启粒子系统
        gunParticlesss.Stop();
        gunParticlesss.Play();
        // 子弹减1
        currentNumberOfBullet--;
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
            Charged();
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
