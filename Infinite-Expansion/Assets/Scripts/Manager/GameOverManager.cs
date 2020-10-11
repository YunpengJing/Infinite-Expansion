using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Manager
{
    public class GameOverManager : MonoBehaviour
    {
        public Text WinText;
        public Text FailText;

        private float damageFromTurret;

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
            WinText.gameObject.SetActive(true);
            Invoke("ReturnToMainMenu", 3);
        }

        public void Fail()
        {
            FailText.gameObject.SetActive(true);
            Invoke("ReturnToMainMenu", 3);
        }

        // 回到主菜单
        private void ReturnToMainMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}

