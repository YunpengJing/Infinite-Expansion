using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalBullet : MonoBehaviour
{

    public float moveSpeed = 100f;


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
        if (other.tag == "Enemy")
        {
            // 碰撞到物体
            Destroy(gameObject);
            Debug.Log("hit enemy");
        }
    }
}
