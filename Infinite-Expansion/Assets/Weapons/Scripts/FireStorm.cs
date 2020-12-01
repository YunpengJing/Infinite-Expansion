using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireStorm : MonoBehaviour
{
    public float damage = 1f;

    public float timer = 0f;

    public float moveSpeed = 5;

    private AudioSource gunAudio;

    // Start is called before the first frame update
    void Start()
    {
        gunAudio = GetComponent<AudioSource>();
        gunAudio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer<3f)
        {
            //移动状态
            transform.Translate(-transform.right * moveSpeed * Time.deltaTime, Space.World);
        }
        else if(timer>6f)
        {
            //6秒后，销毁自身
            Destroy(gameObject);
        }
        timer = timer + Time.deltaTime;
    }

    void OnTriggerStay(Collider other)
    {
        // 碰撞到物体
        if (other.tag == "Enemy")
        {
            //获取Hero
            GameObject hero = GameObject.FindWithTag("Hero");
            other.GetComponent<Enemy>().TakeDamage(damage, hero);
            Debug.Log("hit enemy");
        }
    }
}
