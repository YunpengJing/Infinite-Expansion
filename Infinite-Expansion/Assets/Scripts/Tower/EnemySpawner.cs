using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static int CountEnemyAlive = 0;
    public Wave[] waves;
    public Transform start;
    public float waveRate = 0.2f;

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        foreach (Wave wave in waves)
        {
            for (int i = 0; i < wave.count; i++)
            {
                GameObject.Instantiate(wave.enemyPrefab, start.position, Quaternion.identity);
                CountEnemyAlive++;
                if (i == wave.count) break;
                yield return new WaitForSeconds(wave.rate);
            }

            while (CountEnemyAlive > 0)
            {
                yield return 0;
            }

            yield return new WaitForSeconds(waveRate);
        }
    }
}
