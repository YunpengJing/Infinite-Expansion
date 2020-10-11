using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

public class Turret : MonoBehaviour
{
    // ref
    public GameObject bulletPrefab;
    public Transform firePosition;
    public Transform head;
    public LineRenderer laserRenderer;
    private GameObject mapCubeGo;

    // attr
    public List<GameObject> enemys = new List<GameObject>();
    public float attackRateTime = 1f; // 多少秒攻击一次
    private float timer = 0;    // 计时器
    public bool useLaser = false;
    public float damageRate = 70;   // 1s 造成 70 伤害

    private void Start()
    {
        timer = attackRateTime;
    }

    public void SetMapCubeGo(GameObject mapCubeGo)
    {
        this.mapCubeGo = mapCubeGo;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemys.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemys.Remove(other.gameObject);
        }
    }

    private void Update()
    {
        // 炮口朝向敌人
        if (enemys.Count > 0 && enemys[0] != null)
        {
            Vector3 targetPosition = enemys[0].transform.position;
            targetPosition.y = head.position.y;
            head.LookAt(targetPosition);
        }

        // 判断第一个敌人是否被销毁或死亡，如果是更新 enemys
        if (enemys[0] == null || enemys[0].GetComponent<Enemy>().hp == 0)
        {
            UpdateEnemys();
        }

        // 如果敌人人数等于 0，那 timer 不会增加，也不会攻击
        if (enemys.Count == 0)
        {
            if (useLaser) laserRenderer.enabled = false;
            return;
        }

        if (!useLaser)
        {
            // 使用普通子弹
            timer += Time.deltaTime;

            if (timer >= attackRateTime)
            {
                Attack();
                timer -= attackRateTime;
            }
        }
        else
        {
            // 使用激光
            if (laserRenderer.enabled == false)
            {
                laserRenderer.enabled = true;
            }

            //if (enemys[0] == null || enemys[0].GetComponent<Enemy>().hp == 0)
            //{
            //    UpdateEnemys();
            //}

            if (enemys.Count > 0)
            {
                // 有敌人，进行攻击
                laserRenderer.SetPositions(new Vector3[] { firePosition.position, enemys[0].transform.position });
                enemys[0].GetComponent<Enemy>().TakeDamage(damageRate * Time.deltaTime, mapCubeGo);

                // 统计来自 turret 激光的伤害
                GameOverManager.Instance.AddDamageFromTurret(damageRate * Time.deltaTime);
            }
        }
    }

    void Attack()
    {
        //// 如果 enemys[0] 为空，或 enemys[0] 的 hp 为 0，更新 enemy
        //if (enemys[0] == null || enemys[0].GetComponent<Enemy>().hp == 0)
        //{
        //    UpdateEnemys();
        //}

        if (enemys.Count > 0)
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
            bullet.GetComponent<TowerBullet>().SetTarget(enemys[0].transform);
            bullet.GetComponent<TowerBullet>().SetMapCubeGo(mapCubeGo);
        }
        else
        {
            timer = attackRateTime;
        }
    }

    void UpdateEnemys()
    {
        List<GameObject> newEnemys = new List<GameObject>();

        foreach (GameObject enemy in enemys)
        {
            // 把 enemy 不为 null 且 hp 不为 0 的放进 list
            if (enemy != null && enemy.GetComponent<Enemy>().hp != 0)
            {
                newEnemys.Add(enemy);
            }
        }

        enemys = newEnemys;
    }
}
