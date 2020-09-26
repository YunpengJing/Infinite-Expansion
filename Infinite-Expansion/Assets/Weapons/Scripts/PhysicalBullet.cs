using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalBullet : MonoBehaviour
{

    public float moveSpeed = 100f;

    public float damage = 30f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-transform.forward * moveSpeed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        // 碰撞到物体
        if (other.tag == "Enemy")
        {
            //获取Hero
            GameObject hero = GameObject.FindWithTag("Hero");
            other.GetComponent<Enemy>().TakeDamage(damage,hero);
            Destroy(gameObject);
            Debug.Log("hit enemy");
        }
    }
}
