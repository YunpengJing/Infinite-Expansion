using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float damageFromTurret;
    public float damageFromHero;

    public float hero1AttackBase;
    public int hero1DeathCnt;

    // 单例
    private static EnemyManager instance;

    public static EnemyManager Instance
    {
        get
        {
            return instance;
        }

        set
        {
            instance = value;
        }
    }

    public void Awake()
    {
        Instance = this;
        damageFromTurret = 0;
        damageFromHero = 0;

        hero1AttackBase = 0;
        hero1DeathCnt = 0;
    }

    public void AddDamageFromTurret(float damage)
    {
        damageFromTurret += damage;
    }

    public void AddDamageFromHero(float damage)
    {
        damageFromHero += damage;
    }

    public void AddHero1AttackBase(float damage)
    {
        hero1AttackBase += damage;
    }

    public void IncrHero1DeathCnt()
    {
        hero1DeathCnt += 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
