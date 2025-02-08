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
    }

    private void StartGame()
    {
        StartCoroutine(spawnVaveCoroutine());
        GameManager.SetGameState(GameState.InGame);
    }

    private void OnDestroy()
    {
        GameManager.OnGameStarted -= StartGame;
        StopAllCoroutines();
    }

    public void RespawnEnemy(BaseEnemy _enemy)
    {
        _enemy.transform.position = Camera.main.ViewportToWorldPoint(GetRandomSpawnPoint());
    }

    private void SpawnWave(List<EnemyWaveParametter> _waveContent)
    {
        foreach(EnemyWaveParametter wave in _waveContent)
        {
            for(int spawnAmountIndex = 0; spawnAmountIndex < wave.spawnAmount; spawnAmountIndex++)
            {
                GameObject enemy = PoolManager.GetAvailableObjectFromPool(wave.enemy);

                if(!enemy)
                    SpawnObject(wave.enemy);
                else
                    ReSpawnObject(enemy);
            }
        }
    }
    private void SpawnObject(GameObject _enemy)
    {
        Vector3 relativePos = Camera.main.ViewportToWorldPoint(GetRandomSpawnPoint());
        BaseEnemy enemy = Instantiate(_enemy, relativePos, Quaternion.identity).GetComponent<BaseEnemy>();
        
        // Add the gameObject to the pool
        PoolManager.CreateObject(_enemy, enemy.gameObject);
        
        enemy.ResetEntity(enemy.enemyData);
    }

    private void ReSpawnObject(GameObject _enemy)
    {
        Vector3 relativePos = Camera.main.ViewportToWorldPoint(GetRandomSpawnPoint());
        _enemy.SetActive(true);
        _enemy.transform.position = relativePos;
        
        BaseEnemy enemy = _enemy.GetComponent<BaseEnemy>();

        enemy.ResetEntity(enemy.enemyData);
    }

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

    IEnumerator spawnVaveCoroutine()
    {
        for(int waveIndex = 0; waveIndex <= waveList.Count - 1; waveIndex++)
        {
            for(int spawnIndex = 0;  spawnIndex < waveList[waveIndex].spawnCount; spawnIndex++)
            {
                SpawnWave(waveList[waveIndex].waveContent);
                yield return new WaitForSeconds(waveList[waveIndex].spawnInterval);
            }
        }
    }
}
