

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySpawnData
{
    public GameObject Prefab;
    public int Count;
}

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<EnemySpawnData> enemiesToSpawn;
    private List<Transform> spawnPoints = new();
    [SerializeField] private float spawnInterval = 2f;

    private List<GameObject> spawnQueue = new();

    void Start()
    {
        foreach (Transform child in transform)
        {
            spawnPoints.Add(child);
        }
    }

    public void SpawnEnemies()
    {
        foreach (var data in enemiesToSpawn)
        {
            for (int i = 0; i < data.Count; i++)
            {
                spawnQueue.Add(data.Prefab);
            }
        }

        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (spawnQueue.Count > 0)
        {
            SpawnNext();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnNext()
    {
        if (spawnQueue.Count == 0 || spawnPoints.Count == 0) return;

        int spawnIndex = Random.Range(0, spawnPoints.Count);
        Transform spawnPoint = spawnPoints[spawnIndex];

        int enemyIndex = Random.Range(0, spawnQueue.Count);
        GameObject enemyPrefab = spawnQueue[enemyIndex];
        spawnQueue.RemoveAt(enemyIndex);

        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}