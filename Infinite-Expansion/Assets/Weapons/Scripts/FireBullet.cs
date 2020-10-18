using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{

    public float damage = 1f;

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
