﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public string name = "anonymous";
    public float speed = 10;
    public float hp = 150;
    public int attackDistance = 2;
    public int attackPower = 10;
    public float attackRate = 1;
    private float totalHp;
    private Slider hpSlider;
    private GameObject target;
    private string status = "forward";
    private Transform destination;
    private Animator anim;
    private float timer = 0;
    public int money = 10;
    NavMeshAgent m_Agent;
    private string unusualStatus = "";

    // 减速持续事件
    private float slowDownDuration;

    // Start is called before the first frame update
    void Start()
    {
        hpSlider = GetComponentInChildren<Slider>();
        totalHp = hp;
        anim = GetComponent<Animator>();
        timer = attackRate;
        destination = HomeCube.homeTransform;
        m_Agent = GetComponent<NavMeshAgent>();
        m_Agent.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (status == "forward")
        {
            Forward();
        }
        else if (status == "fight")
        {
            if (target == null)
            {
                this.status = "forward";
                timer = attackRate;
                return;
            }
            if (Vector3.Distance(target.transform.position, transform.position) > attackDistance)
            {
                m_Agent.ResetPath();
                Vector3 newDestination = (transform.position - target.transform.position).normalized * 3 + target.transform.position;
                m_Agent.destination = newDestination;
            }
            else
            {
                m_Agent.ResetPath();
                timer += Time.deltaTime;
                if (timer >= attackRate)
                {
                    timer = 0;
                    if (hp > 0)
                    {
                        Fight();
                    }
                }
            }
        }

        if (unusualStatus == "slowDown")
        {
            slowDownDuration -= Time.deltaTime;
            if (slowDownDuration < 0)
            {
                unusualStatus = "";
                EndSlowDown();
            }
        }
    }

    private void Forward()
    {
        m_Agent.destination = destination.position;
        anim.Play("WalkFWD");
    }

    private void OnTriggerEnter(Collider col)
    {
        //attack home
        if (col.tag == "Home" && target == null)
        {
            this.status = "fight";
            target = col.gameObject;
        }
    }

    void OnDestroy()
    {
        EnemySpawner.CountEnemyAlive--;    
    }
    private void TrackDamage(string tag, float damage)
    {
        if (tag == "Turret")
        {
            EnemyManager.Instance.damageFromTurret += damage;
        }
        else if (tag == "Hero")
        {
            EnemyManager.Instance.damageFromHero += damage;
        }
        else
        {
            EnemyManager.Instance.damageFromOther += damage;
        }
    }
    public void TakeDamage(float damage, GameObject source)
    {
        if (hp <= 0)
        {
            return;
        }
        if (source)
        {
            TrackDamage(source.tag, damage);
        }
        //update hp and slider
        hp -= damage;
        anim.Play("GetHit");
        hpSlider.value = hp / totalHp;
        if (hp <= 0)
        {
            Die(); 
        }
        //set attack target
        if (source != null && target == null)
        {
            if (source.tag == "Turret")
            {
                this.status = "fight";
                target = source;
            }
        }
    } 

    private void TrackeDeath()
    {
        if (EnemyManager.Instance.enemyDeathStat.ContainsKey(this.name))
        {
            EnemyManager.Instance.enemyDeathStat[this.name] = EnemyManager.Instance.enemyDeathStat[this.name] + 1;
        } else
        {
            EnemyManager.Instance.enemyDeathStat.Add(this.name, 1);
        }

    }
    void Die()
    {
        //call die animation and destroy the object
        m_Agent.ResetPath();
        anim.Play("Die");
        MoneyManager.Instance.UpdateMoney(this.money);
        status = "die";
        float dieTime = 1.0f;
        TrackeDeath();
        Destroy(this.gameObject, dieTime);
    }
    void Fight()
    {
        //stop and call attack animation
        if (target.tag == "Turret")
        {
            int num=Random.Range(0,2);
            if(num==0)
                anim.Play("Attack01");
            else
                anim.Play("Attack02");
            target.GetComponent<MapCube>().TakeDamage(attackPower);
            TrackTakingDamage("Turret", attackPower);
        }
        else if (target.tag == "Home")
        {
            int num=Random.Range(0,2);
            if(num==0)
                anim.Play("Attack01");
            else
                anim.Play("Attack02");

            anim.Play("IdleBattle");
            target.GetComponent<HomeCube>().TakeDamage(attackPower);
            TrackTakingDamage("Home", attackPower);
        }
    }

    private void TrackTakingDamage(string target, int damage)
    {
        if (target == "Turret")
        {
            EnemyManager.Instance.damageToTurret += damage;
        } else if (target == "Hero")
        {
            EnemyManager.Instance.damageToHero += damage;
        } else if (target == "Home")
        {
            EnemyManager.Instance.damageToHome += damage;
        }
    }

    public void SlowDown(int duration)
    {
        if (unusualStatus != "slowDown")
        {
            unusualStatus = "slowDown";
            speed /= 2;
            m_Agent.speed = speed;
            SkinnedMeshRenderer[] smr = this.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
            foreach (SkinnedMeshRenderer sss in smr)
            {
                //sss.material.color = Color.blue;
                sss.material.color = new Color(76 / 255.0f, 142 / 255.0f, 195 / 255.0f, 1.0f);
            }
            //Invoke("EndSlowDown", (float)duration);
            slowDownDuration = 2.0f;
        }
        else
        {
            slowDownDuration = 2.0f;
        }
    }

    public void EndSlowDown()
    {
        unusualStatus = "";
        speed *= 2;
        m_Agent.speed = speed;
        SkinnedMeshRenderer[] smr = this.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (SkinnedMeshRenderer sss in smr)
        {
            Debug.Log(sss.material.color);
            sss.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
    }
}
