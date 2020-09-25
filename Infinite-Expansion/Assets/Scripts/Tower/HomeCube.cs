using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Manager;

public class HomeCube : MonoBehaviour
{
    public int hp = 1000;
    public int totalHp;
    public Slider hpSlider;

    // Start is called before the first frame update
    void Start()
    {
        totalHp = hp;
    }

    public void TakeDamage(int damage)
    {
        if (hp <= 0) return;

        hp -= damage;
        hpSlider.value = (float)hp / totalHp;

        if (hp <= 0)
        {
            //GameOverManager.Fail();
            GameOverManager.Instance.Fail();
        }
    }
}
