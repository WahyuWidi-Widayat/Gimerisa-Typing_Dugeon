using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public SpawnState spawnState = SpawnState.Counting;

    [SerializeField] public float spawnInterval = 2f;
    [SerializeField] public int totalEnemies = 10;
    [SerializeField] public int maxEnemiesOnField = 3;
    [SerializeField] public GameObject[] enemyPrefab;
    [SerializeField] public Transform[] spawnerPoints;



    private float nextSpawnTime = 0f;
    private int enemiesSpawned = 0;

    void Update()
    {
        switch (spawnState)
        {
            case SpawnState.Counting:
                if (Time.time >= nextSpawnTime)
                {
                    spawnState = SpawnState.Spawning;
                }
                break;

            case SpawnState.Spawning:
                if (enemiesSpawned < totalEnemies)
                {
                    SpawnEnemy();
                    spawnState = SpawnState.Waiting;
                    nextSpawnTime = Time.time + spawnInterval;
                }
                else
                {
                    Debug.Log("Semua musuh sudah muncul.");
                }
                break;

            case SpawnState.Waiting:
                // Bisa kamu isi logika menunggu musuh mati sebelum lanjut spawn berikutnya
                if (Time.time >= nextSpawnTime)
                {
                    spawnState = SpawnState.Spawning;
                }
                break;
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefab.Length == 0 || spawnerPoints.Length == 0)
        {
            Debug.LogWarning("Prefab atau spawn point belum diisi!");
            return;
        }

        GameObject randomEnemy = enemyPrefab[Random.Range(0, enemyPrefab.Length)];
        Transform randomPoint = spawnerPoints[Random.Range(0, spawnerPoints.Length)];

        Instantiate(randomEnemy, randomPoint.position, randomPoint.rotation);
        enemiesSpawned++;

        Debug.Log("Spawn musuh ke-" + enemiesSpawned);
    }
}
