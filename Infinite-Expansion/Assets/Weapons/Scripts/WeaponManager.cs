using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    public float damage = 1f;
    public float timeBetweenBullet = 0.15f;

    private float timer;

    private ParticleSystem gunParticles;

    private AudioSource gunAudio;

    // Start is called before the first frame update
    void Start()
    {
        gunParticles = GetComponent<ParticleSystem>();
        gunAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetButton("Fire1") && timer >= timeBetweenBullet)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        timer = 0;
        // 开启灯光

        // 播放声音
        gunAudio.Play();
        // 画线

        // 开启粒子系统
        gunParticles.Stop();
        gunParticles.Play();

        // 测试
        Debug.Log("aaaa");
    }
}
