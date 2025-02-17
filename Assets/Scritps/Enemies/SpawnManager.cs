using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    [SerializeField] private List<WaveSO> waveList = new List<WaveSO>();

    //private int currentWaveIndex = 0;
    [SerializeField] private IntVariableSO waveIndex;

    [SerializeField] private bool isSpawning = false;
    [SerializeField] private int enemiesCount = 0;

    private void Awake()
    {
        instance = this;
        GameManager.OnInitGame += InitGame;
        GameManager.OnWaveStart += SpawnWave;
    }
    private void OnDestroy()
    {
        GameManager.OnInitGame -= InitGame;
        GameManager.OnWaveStart -= SpawnWave;
        StopAllCoroutines();
    }

    public void InitGame()
    {
        waveIndex.Value = 0;
    }

    private void SpawnWave()
    {
        StartCoroutine(spawnWaveCoroutine(waveList[waveIndex.Value]));
    }



    /// <summary>
    /// Return a single random enemy from the given wave by its weight
    /// </summary>
    /// <param name="_waveContent"></param>
    /// <returns></returns>
    private GameObject GetRandomEnemyFromWave(List<EnemyWaveParametter> _waveContent)
    {
        float totalWeight = 0f;

        // Get the total weight of all enemies availables
        foreach (EnemyWaveParametter enemy in _waveContent)
            totalWeight += enemy.weight;

        // Get a random weight
        float randomValue = Random.Range(0, totalWeight);
        float cumulativeWeight = 0f;


        foreach (EnemyWaveParametter enemy in _waveContent)
        {
            cumulativeWeight += enemy.weight;

            if (randomValue <= cumulativeWeight)
                return enemy.prefab;
        }

        Debug.Log("no ennemy");
        return null;
    }

    /// <summary>
    /// Instantiate a new enemy and add it to the pool.
    /// </summary>
    /// <param name="_enemy"></param>
    private void SpawnEnemy(GameObject _enemy)
    {
        GameObject enemy = Instantiate(_enemy, GridManager.mainPath[0], Quaternion.identity);
    }

    /// <summary>
    /// Reduce the enemy count and ensure that enemies had finished spawning before ending the wave.
    /// </summary>
    public void OnEnemyDeath()
    {
        enemiesCount--;
        if(enemiesCount <= 0 || !isSpawning)
            EndWave();
    }

    private void EndWave()
    {
        if (waveIndex.Value + 1 >= waveList.Count)
        {
            Debug.LogError("Victory !");
            return;
        }
        waveIndex.Value++;
        GameManager.WaveEnd();
    }


    /// <summary>
    /// Spawn all waves in chain, taking their duration into account.
    /// </summary>
    /// <returns></returns>
    IEnumerator spawnWaveCoroutine(WaveSO _wave)
    {
        isSpawning = true;
        enemiesCount = 0;
        for (int spawnIndex = 0; spawnIndex < _wave.spawnAmount; spawnIndex++)
        {
            yield return new WaitForSeconds(_wave.spawnInterval);

            GameObject randomEnemy = GetRandomEnemyFromWave(_wave.waveContent);
            SpawnEnemy(randomEnemy);
            enemiesCount++;
        }
        isSpawning = false;
    }
}
