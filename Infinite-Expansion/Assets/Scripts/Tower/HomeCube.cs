using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Manager;

public class HomeCube : MonoBehaviour
{
    public int hp = 1000;
    private int totalHp;
    public Slider hpSlider;
    public static Transform homeTransform;

    // Start is called before the first frame update
    void Start()
    {
        totalHp = hp;
        homeTransform = transform;
    }

    public void TakeDamage(int damage)
    {
        if (hp <= 0) return;

        hp -= damage;
        hpSlider.value = (float)hp / totalHp;

        if (hp <= 0)
        {
            GameOverManager.Instance.Fail();
        }
    }
}
