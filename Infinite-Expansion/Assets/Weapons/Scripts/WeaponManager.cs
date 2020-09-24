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

    private ParticleSystem gunParticles;

    private AudioSource gunAudio;

    private Ray shootRay;
    private int shootableMask;
    private RaycastHit shootHit;
    public float maxLength = 100f; // 射线最长长度

    public GameObject gunLine;

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
        if (Input.GetButton("Fire2") && timer >= timeBetweenBullet)
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
            GameObject gunLineInstance = Instantiate(gunLine, transform.position, new Quaternion(0, 180, 0, 1), transform);
            Destroy(gunLineInstance, 2f);
        }

        // 开启粒子系统
        gunParticles.Stop();
        gunParticles.Play();

        // 检测射击是否击中怪物
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if (Physics.Raycast(shootRay, out shootHit, maxLength, shootableMask))
        {
            // 击中物体(击中物体由shootHit检测)
            // 终点位置 = 射线击中的物体的位置
            // gunLine.SetPosition(1, shootHit.point);
            // 怪物扣血
            // EnemyHealth enemyHealth = shootHit.collider.gameObject.GetComponent<EnemyHealth>();
            //if (enemyHealth != null)
            //{
            //    enemyHealth.TakeDamage(damagePerShot, shootHit.point);
            //}
        }
        else
        {
            // 未击中任何物体
            // 终点位置 = 枪口位置 + 枪口方向向量 * 长度
            // gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }

        // 测试
        Debug.Log("aaaa");
    }

}
