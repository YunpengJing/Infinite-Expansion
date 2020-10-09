using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float moveSpeed = 100f;

    public float maxDamage = 150f; // 最大伤害
    public float currentDamage; // 当前伤害

    private ParticleSystem bulletSFX;

    private GameObject enemy;

    //private bool playOnlyOnce = false;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        bulletSFX = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * moveSpeed * Time.deltaTime, Space.World);

        // 检测碰撞发生
        
    }

    void OnTriggerEnter(Collider other)
    {
        // 碰撞到物体
        if (other.gameObject == enemy)
        {
            // SFX播放
            // bulletSFX.Play();
            GameObject hero = GameObject.FindWithTag("Hero");
            Debug.Log("当前伤害：" + currentDamage);
            other.GetComponent<Enemy>().TakeDamage(currentDamage, hero);
            Destroy(gameObject);
            Debug.Log("hit enemy");
        }
    }
}
