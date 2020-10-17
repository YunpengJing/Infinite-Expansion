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
            hasWin = false;
            hasFail = false;
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

            BuildManager bm = BuildManager.Instance;

            AnalyticsResult res1 = Analytics.CustomEvent("winTurret", new Dictionary<string, object>
            {
                { "leftMoney", MoneyManager.Instance.getCurrentMoney() },
                { "totalBuildCnt", bm.mapCubeCnt + bm.standardTurretCnt + bm.missileTurretCnt + bm.laserTurretCnt },
                { "mapCubeCnt", bm.mapCubeCnt },
                { "standardTurretCnt", bm.standardTurretCnt },
                { "missileTurretCnt", bm.standardTurretCnt },
                { "laserTurretCnt", bm.laserTurretCnt }
            });

            print("win turret analytics res: " + res1);

            AnalyticsResult res2 = Analytics.CustomEvent("winEnemy", new Dictionary<string, object>
            {
                { "damageFromTurret", EnemyManager.Instance.damageFromTurret },
                { "damageFromHero", EnemyManager.Instance.damageFromHero }
            });

            print("win enemy analytics res: " + res2);
        }

        public void Fail()
        {
            if (hasFail) return;
            hasFail = true;

            FailText.gameObject.SetActive(true);
            Invoke("ReturnToMainMenu", 3);

            BuildManager bm = BuildManager.Instance;

            AnalyticsResult res1 = Analytics.CustomEvent("failTurret", new Dictionary<string, object>
            {
                { "leftMoney", MoneyManager.Instance.getCurrentMoney() },
                { "totalBuildCnt", bm.mapCubeCnt + bm.standardTurretCnt + bm.missileTurretCnt + bm.laserTurretCnt },
                { "mapCubeCnt", bm.mapCubeCnt },
                { "standardTurretCnt", bm.standardTurretCnt },
                { "missileTurretCnt", bm.standardTurretCnt },
                { "laserTurretCnt", bm.laserTurretCnt }
            });

            print("fail turret analytics res: " + res1);

            AnalyticsResult res2 = Analytics.CustomEvent("failEnemy", new Dictionary<string, object>
            {
                { "damageFromTurret", EnemyManager.Instance.damageFromTurret },
                { "damageFromHero", EnemyManager.Instance.damageFromHero }
            });

            print("fail enemy analytics res: " + res2);
        }

        // 回到主菜单
        private void ReturnToMainMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}

