using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

public class TowerBullet : MonoBehaviour
{
    public int damage = 50;
    public float speed = 40;

    public GameObject explosionEffectPrefab;
    private Transform target;
    private GameObject mapCubeGo;

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void SetMapCubeGo(GameObject mapCubeGo)
    {
        this.mapCubeGo = mapCubeGo;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Die();
            return;
        }

        transform.LookAt(target.position);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            // 如果怪物没血了，直接穿过
            if (other.gameObject.GetComponent<Enemy>().hp == 0) return;

            other.GetComponent<Enemy>().TakeDamage(damage, mapCubeGo);
            Die();

            // 统计来自 turret 子弹的伤害
            GameOverManager.Instance.AddDamageFromTurret(damage);
        }
    }

    void Die()
    {
        GameObject effect = GameObject.Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
        Destroy(effect, 1);
        Destroy(gameObject);
    }

}
