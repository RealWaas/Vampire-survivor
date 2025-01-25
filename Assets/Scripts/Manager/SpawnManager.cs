using UnityEngine;
using UnityEngine.UIElements;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    [SerializeField] private GameObject spawnPrefab;
    [SerializeField] private float spawnDistance = 0.5f;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SpawnObject();
        }
    }

    public void SpawnObject()
    {
        Vector3 relativePos = Camera.main.ViewportToWorldPoint(GetRandomSpawnPoint());
        GameObject mob = Instantiate(spawnPrefab, relativePos, Quaternion.identity);
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
}
