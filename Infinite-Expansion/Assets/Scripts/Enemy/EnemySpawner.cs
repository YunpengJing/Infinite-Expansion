using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
using UnityEngine.Analytics;
public class EnemySpawner : MonoBehaviour
{
    public Wave[] waves;
    public Transform START;
    public float waveRate = 2;
    public static int CountEnemyAlive = 0;
    private int waveCount = 0;

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private void Update()
    {
        if (waves.Length == waveCount && CountEnemyAlive == 0)
        {
            GameOverManager.Instance.Win();
        }
    }
    IEnumerator SpawnEnemy()
    {
        foreach (Wave wave in waves)
        {
            for (int i = 0; i < wave.count; i++)
            {
                GameObject.Instantiate(wave.enemyPrefab, START.position, Quaternion.identity);
                CountEnemyAlive++;
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
        }
    }
}
