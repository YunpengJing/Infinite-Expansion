using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public List<GameObject> enemys = new List<GameObject>();
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            enemys.Add(col.gameObject);
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "Enemy")
        {
            enemys.Remove(col.gameObject);
        }
    }

    public float attackRateTime = 1;
    private float timer = 0;

    public GameObject bulletPrefab;
    public Transform firePosition;

    private void Start()
    {
        timer = attackRateTime;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (enemys.Count > 0 && timer >= attackRateTime)
        {
            timer = 0;
            Attack();
        }
    }

    void Attack()
    {
        if(enemys[0] == null)
        {
            UpdateEnemys();
        }
        if(enemys.Count > 0)
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
            bullet.GetComponent<Bullet>().SetTarget(enemys[0].transform);
            bullet.GetComponent<Bullet>().SetSourceTurret(this);
        }
        else
        {
            timer = attackRateTime;
        }
    }

    void UpdateEnemys()
    {
        List<int> emptyIndex = new List<int>();
        for(int index = 0; index <enemys.Count; index++)
        {
            if (enemys[index] == null)
            {
                emptyIndex.Add(index);
            }
        }
        for (int i=0; i<emptyIndex.Count; i++)
        {
            enemys.RemoveAt(emptyIndex[i]-i);
        }
    }

    private int hp = 2;
    public void TakeDamage(int damage)
    {
        hp -= 1;
        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
