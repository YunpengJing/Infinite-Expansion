  using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    private Transform[] positions;
    private int index = 0;
    private Animator anim;
    private float timer = 0;
    public int money = 10;

    // Start is called before the first frame update
    void Start()
    {
        positions = Waypoints.positions;
        hpSlider = GetComponentInChildren<Slider>();
        totalHp = hp;
        anim = GetComponent<Animator>();
        timer = attackRate;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(anim.SetTrigger("attack"));
        if (status == "forward")
        {
            Move();
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
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
                transform.forward = target.transform.position - transform.position;
            }
            else
            {
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

    private void Move()
    {
        if (index > positions.Length - 1)
        {
            return;
        }
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        anim.Play("WalkFWD");
        transform.forward = positions[index].position - transform.position;
        if(Time.deltaTime * speed >= Vector3.Distance (transform.position, positions[index].position)){
            index++;
        }
        else if (Vector3.Distance(positions[index].position, transform.position) < 0.02f)
        {
            index++;
        }
        if (index > positions.Length - 1)
        {
            ReachDestination();
        }
    }

    void ReachDestination()
    {
        Destroy(this.gameObject);
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

    public void TrackDamage(string tag, float damage)
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
        TrackDamage(source.tag, damage);
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
        transform.Translate(new Vector3(0, 0, 0));
        if (target.tag == "Turret")
        {
            anim.Play("Attack01");
            target.GetComponent<MapCube>().TakeDamage(attackPower);
        }
        else if (target.tag == "Home")
        {
            anim.Play("Attack01");
            anim.Play("IdleBattle");
            target.GetComponent<HomeCube>().TakeDamage(attackPower);
        }
    }
}
