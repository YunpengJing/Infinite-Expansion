  using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.Analytics;
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

    // Start is called before the first frame update
    void Start()
    {
        hpSlider = GetComponentInChildren<Slider>();
        totalHp = hp;
        anim = GetComponent<Animator>();
        timer = attackRate;
        destination = HomeCube.homeTransform;
        m_Agent = GetComponent<NavMeshAgent>();
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
                m_Agent.destination = target.transform.position;
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
            Analytics.CustomEvent("EnemyDamageSource", new Dictionary<string, object>
        {
            { "DamageFromTurret", damage}
        });
        }
        else if (tag == "Hero")
        {
            Analytics.CustomEvent("EnemyDamageSource", new Dictionary<string, object>
        {
            { "DamageFromHero", damage}
        });
        }
        else
        {
            Analytics.CustomEvent("EnemyDamageSource", new Dictionary<string, object>
        {
            { "DamageFromOther", damage}
        });
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
    void Die()
    {
        //call die animation and destroy the object
        transform.Translate(new Vector3(0, 0, 0));
        anim.Play("Die");
        MoneyManager.Instance.UpdateMoney(this.money);
        status = "die";
        float dieTime = 1.0f;
        Destroy(this.gameObject, dieTime);
        Analytics.CustomEvent("EnemyDeath", new Dictionary<string, object>
        {
            {"EnemyName", this.name}
        });
    }
    void Fight()
    {
        //stop and call attack animation
        if (target.tag == "Turret")
        {
            anim.Play("Attack01");
            target.GetComponent<MapCube>().TakeDamage(attackPower);
            TrackTakingDamage("Turret", attackPower);
        }
        else if (target.tag == "Home")
        {
            anim.Play("Attack01");
            anim.Play("IdleBattle");
            target.GetComponent<HomeCube>().TakeDamage(attackPower);
            TrackTakingDamage("Home", attackPower);
        }
    }

    private void TrackTakingDamage(string target, int damage)
    {
        Analytics.CustomEvent("EnemyDamageTarget", new Dictionary<string, object>
        {
            {target, damage}
        });
    }
}
