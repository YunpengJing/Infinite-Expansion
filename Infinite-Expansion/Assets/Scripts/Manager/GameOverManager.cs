using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

namespace Manager
{
    public class GameOverManager : MonoBehaviour
    {
        public Text WinText;
        public Text FailText;
        public bool hasWin;
        public bool hasFail;
        public float damageFromTurret;

        // 单例
        private static GameOverManager instance;

        public static GameOverManager Instance
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
            damageFromTurret = 0;
            hasWin = false;
            hasFail = false;
        }

        public void AddDamageFromTurret(float damage)
        {
            damageFromTurret += damage;
        }

        // Start is called before the first frame update
        void Start()
        {
            WinText.gameObject.SetActive(false);
            FailText.gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Win()
        {
            if (hasWin) return;
            hasWin = true;

            WinText.gameObject.SetActive(true);
            Invoke("ReturnToMainMenu", 3);

            AnalyticsResult res = Analytics.CustomEvent("win", new Dictionary<string, object>
            {
                { "leftMoney", MoneyManager.Instance.getCurrentMoney() },
            });

            print("win analytics res: " + res);
        }

        public void Fail()
        {
            if (hasFail) return;
            hasFail = true;

            FailText.gameObject.SetActive(true);
            Invoke("ReturnToMainMenu", 3);

            AnalyticsResult res = Analytics.CustomEvent("fail", new Dictionary<string, object>
            {
                { "leftMoney", MoneyManager.Instance.getCurrentMoney() },
            });

            print("fail analytics res: " + res);
        }

        // 回到主菜单
        private void ReturnToMainMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}

