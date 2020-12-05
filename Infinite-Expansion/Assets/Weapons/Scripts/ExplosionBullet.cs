﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBullet : MonoBehaviour
{

    public float damage = 5f;

    // Start is called before the first frame update
    void Start()
    {
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
        Destroy(gameObject);
    }
}