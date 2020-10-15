using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class FireWeaponManager : MonoBehaviour
{
    public int update = 0;

    private float timer;

    private ParticleSystem gunParticles;

    private AudioSource gunAudio;

    private GameObject fireInstance=null;

    //引用
    public GameObject firePrefab;

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
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        //if (Input.GetButton("Fire2") && timer >= timeBetweenBullet)
        float shootButton = heroInput.HeroAction.Shoot.ReadValue<float>();
        if (shootButton > 0 && fireInstance==null)
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
        Vector3 facing = transform.rotation * Vector3.right;
        
        fireInstance = Instantiate(firePrefab,transform);
        fireInstance.transform.position = transform.position + facing*(-2);
        
        
    }
}
