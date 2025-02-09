using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    [SerializeField] private float spawnDistance = 0.5f;

    [SerializeField] private List<WaveSO> waveList = new List<WaveSO>();

    private void Awake()
    {
        instance = this;

        GameManager.OnGameStarted += StartGame;
        GameManager.OnGameOverState += EndGame;
    }

    private void StartGame()
    {
        // Start spawning the first wave
        StartCoroutine(spawnWaveCoroutine());

        GameManager.SetGameState(GameState.InGame);
    }

    public void EndGame()
    {
        StopAllCoroutines();
    }

    private void OnDestroy()
    {
        GameManager.OnGameStarted -= StartGame;
        StopAllCoroutines();
    }

    /// <summary>
    /// Replace an enemy that get too far from the player.
    /// </summary>
    /// <param name="_enemy"></param>
    public void RespawnEnemy(Transform _enemyTransform)
    {
        _enemyTransform.position = Camera.main.ViewportToWorldPoint(GetRandomSpawnPoint());
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


        foreach(EnemyWaveParametter enemy in _waveContent)
        {
            cumulativeWeight += enemy.weight;

            if (randomValue <= cumulativeWeight)
                return enemy.prefab;
        }

        Debug.Log("no ennemy");
        return null;
    }

    /// <summary>
    /// Spawn of respawn all elements of the wave.
    /// </summary>
    /// <param name="_wave"></param>
    private void SpawnWave(WaveSO _wave)
    {
        for(int spawnAmountIndex = 0; spawnAmountIndex < _wave.spawnAmount; spawnAmountIndex++)
        {
            // Randomly get all enemies
            GameObject enemyPrefab = GetRandomEnemyFromWave(_wave.waveContent);

            // Try to get an available enemy in the pool manager
            GameObject enemy = PoolManager.GetAvailableObjectFromPool(enemyPrefab);

            if(!enemy) // If none is available, create a new one
                SpawnEnemy(enemyPrefab);
            else // Else, respawn it
                RespawnEnemy(enemy);
        }
    }

    /// <summary>
    /// Instantiate a new enemy and add it to the pool.
    /// </summary>
    /// <param name="_enemy"></param>
    private void SpawnEnemy(GameObject _enemy)
    {
        Vector3 relativePos = Camera.main.ViewportToWorldPoint(GetRandomSpawnPoint());
        BaseEnemy enemy = Instantiate(_enemy, relativePos, Quaternion.identity).GetComponent<BaseEnemy>();
        
        // Add the gameObject to the pool
        PoolManager.CreateObject(_enemy, enemy.gameObject);
        
        enemy.ResetEntity(enemy.enemyData);
    }

    /// <summary>
    /// Reset an enemy outside of the screen and reset its stats.
    /// </summary>
    /// <param name="_enemy"></param>
    private void RespawnEnemy(GameObject _enemy)
    {
        Vector3 relativePos = Camera.main.ViewportToWorldPoint(GetRandomSpawnPoint());
        _enemy.SetActive(true);
        _enemy.transform.position = relativePos;
        
        BaseEnemy enemy = _enemy.GetComponent<BaseEnemy>();

        enemy.ResetEntity(enemy.enemyData);
    }

    /// <summary>
    /// Return a valid spawn point outside of the camera.
    /// </summary>
    /// <returns></returns>
    private Vector3 GetRandomSpawnPoint()
    {
        int spawnSide = Random.Range(0, 4);

        float positivePos = 1 + spawnDistance;
        float negativePos = 0 - spawnDistance;

        switch (spawnSide)
        {
            case 0:
                return new Vector3(
                    positivePos,
                    Random.Range(negativePos, positivePos),
                    10);

            case 1:
                return new Vector3(
                    negativePos,
                    Random.Range(negativePos, positivePos),
                    10);

            case 2:
                return new Vector3(
                    Random.Range(negativePos, positivePos),
                    positivePos,
                    10);

            case 3:
                return new Vector3(
                    Random.Range(negativePos, positivePos),
                    negativePos,
                    10);
            default:
            case 4:
                Debug.Log("Impossible");
                return Vector3.zero;
        }
    }

    /// <summary>
    /// Spawn all waves in chain, taking their duration into account.
    /// </summary>
    /// <returns></returns>
    IEnumerator spawnWaveCoroutine()
    {
        foreach(WaveSO wave in waveList)
        {
            // get how many time the wave should be spawned
            float waveAmount = wave.waveDuration / wave.spawnInterval;
            
            for (int spawnIndex = 0;  spawnIndex < waveAmount ; spawnIndex++)
            {
                SpawnWave(wave);

                // Wait for the interval before respawning the wave
                yield return new WaitForSeconds(wave.spawnInterval);
            }
        }
    }
}
