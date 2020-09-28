using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class WeaponManager : MonoBehaviour
{

    public float damage = 1f;
    public float timeBetweenBullet = 0.15f;

    private float timer;

    public ParticleSystem gunParticlesss;

    private AudioSource gunAudio;

    private Ray shootRay;
    private int shootableMask;
    private RaycastHit shootHit;
    public float maxLength = 100f; // 射线最长长度

    public GameObject gunLine;

    private HeroInputManager heroInput;

    private bool playOnlyOnce = false;

    // Start is called before the first frame update
    void Start()
    {
        // gunParticles = GetComponent<ParticleSystem>();
        gunAudio = GetComponent<AudioSource>();
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
        if (shootButton > 0 && timer >= timeBetweenBullet)
        {
            Shoot();
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
        if (gunLine != null)
        {
            GameObject gunLineInstance = Instantiate(gunLine, transform.position, new Quaternion(0, 0, 0, 1), transform);
            Destroy(gunLineInstance, 2f);
        }
        // 开启粒子系统
        gunParticlesss.Stop();
        gunParticlesss.Play();
        // 测试
        Debug.Log("aaaa");
    }

}
