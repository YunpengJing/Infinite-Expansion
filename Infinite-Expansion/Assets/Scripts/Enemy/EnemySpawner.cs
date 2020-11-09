using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
using UnityEngine.Analytics;
public class EnemySpawner : MonoBehaviour
{
    public Wave[] enemies;
    public Wave[] bosses;
    public Transform[] START;
    public float waveRate = 2;
    public static int CountEnemyAlive = 0;
    private int waveCount = 1;

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private void Update()
    {
        /*if (enemies.Length == waveCount && CountEnemyAlive == 0)
        {
            GameOverManager.Instance.Win();
        }*/
    }
    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            Wave wave;
            if (waveCount % 10 == 0)
            {
                int index = Random.Range(0, bosses.Length);
                //int index=0;
                wave = bosses[index];
            }
            else
            {
                int index = Random.Range(0, enemies.Length);
                //int index=0;
                wave = enemies[index];
            }
            for (int i = 0; i < waveCount / 2; i++)
            {
                for (int j = 0; j < START.Length; j++)
                {
                    GameObject.Instantiate(wave.enemyPrefab, START[j].position, Quaternion.identity);
                    CountEnemyAlive++;
                }
                yield return new WaitForSeconds(wave.rate);
            }
            waveCount++;
            Analytics.CustomEvent("AliveWaveNumber", new Dictionary<string, object>
            {
                { "AliveWaveNumber", waveCount}
            });
            while (CountEnemyAlive > 0)
            {
                yield return 0;
            }
            yield return new WaitForSeconds(waveRate);
        }
        /*foreach (Wave wave in enemies)
        {
            for (int i = 0; i < wave.count; i++)
            {
                for (int j = 0; j < START.Length; j ++)
                {
                    GameObject.Instantiate(wave.enemyPrefab, START[j].position, Quaternion.identity);
                    CountEnemyAlive++;
                }
                if (i!=wave.count - 1)
                {
                    yield return new WaitForSeconds(wave.rate);
                }
            }
            waveCount ++;
            Analytics.CustomEvent("AliveWaveNumber", new Dictionary<string, object>
            {
                { "AliveWaveNumber", waveCount}
            });
            while (CountEnemyAlive > 0)
            {
                yield return 0;
            }
            yield return new WaitForSeconds(waveRate);
        }*/
    }
}
