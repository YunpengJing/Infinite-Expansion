using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{

    public Text moneyText;
    public int initialMoney;
    private int currentMoney;
    public Animator moneyAnimator;

    // 单例
    private static MoneyManager instance;

    public static MoneyManager Instance
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

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentMoney = initialMoney;
        moneyText.text = "$ " + currentMoney;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
     * update the current money.
     * 
     * @param:
     *  offset: positive offset means make money, negative offset means use money
     *  
     * @return:
     *  true: update successfully
     *  false: update unsuccessfully
     */
    public bool UpdateMoney(int offset)
    {
        print("currentMoney:" + currentMoney);
        if (currentMoney + offset < 0)
        {
            moneyAnimator.SetTrigger("Flicker");
            return false;
        }

        currentMoney += offset;
        moneyText.text = "$ " + currentMoney;
        return true;
    }
}
