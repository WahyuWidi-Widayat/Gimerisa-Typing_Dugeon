using UnityEngine;
using System.Collections.Generic;



public class SpawnerController : MonoBehaviour
{
    public SpawnState spawnState = SpawnState.Counting;

    [SerializeField] public float spawnInterval = 2f;
    [SerializeField] public int totalEnemies = 10;
    [SerializeField] public int maxEnemiesOnField = 3;
    [SerializeField] public GameObject[] enemyPrefab;
    [SerializeField] public Transform[] spawnerPoints;
    [SerializeField] public HUDManager hudManager;

    private float nextSpawnTime = 0f;
    private int enemiesSpawned = 0;
    private int enemiesKilled = 0;

    private List<GameObject> activeEnemies = new List<GameObject>();

    void Start()
    {
        // update awal ke HUD
        if (hudManager != null)
            hudManager.UpdateMustKillText(totalEnemies - enemiesKilled);
        
        nextSpawnTime = Time.time; // Mulai hitung spawn
    }

    void Update()
    {
        activeEnemies.RemoveAll(enemy => enemy == null);

        switch (spawnState)
        {
            case SpawnState.Counting:
                if (Time.time >= nextSpawnTime)
                    spawnState = SpawnState.Spawning;
                break;

            case SpawnState.Spawning:
                if (enemiesSpawned < totalEnemies)
                {
                    if (activeEnemies.Count < maxEnemiesOnField)
                    {
                        SpawnEnemy();
                        nextSpawnTime = Time.time + spawnInterval;
                        spawnState = SpawnState.Waiting;
                    }
                    else
                    {
                        spawnState = SpawnState.Waiting;
                    }
                }
                else
                {
                    if (activeEnemies.Count == 0)
                    {
                        Debug.Log("Semua musuh sudah dikalahkan!");
                        // Di sini Anda bisa tambahkan logika "Level Selesai"
                        spawnState = SpawnState.Counting; // Berhenti spawn
                    }
                }
                break;

            case SpawnState.Waiting:
                if (Time.time >= nextSpawnTime &&
                    activeEnemies.Count < maxEnemiesOnField &&
                    enemiesSpawned < totalEnemies)
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

        GameObject newEnemy = Instantiate(randomEnemy, randomPoint.position, randomPoint.rotation);
        Enemy enemyScript = newEnemy.GetComponent<Enemy>();
        if (enemyScript != null)
        {
            enemyScript.spawnerController = this; // koneksi balik
        }

        activeEnemies.Add(newEnemy);
        enemiesSpawned++;

        if (hudManager != null)
            hudManager.UpdateMustKillText(totalEnemies - enemiesKilled);

        Debug.Log($"Spawn musuh ke-{enemiesSpawned} (aktif: {activeEnemies.Count})");
    }

    public void OnEnemyKilled(GameObject enemy)
    {
        if (activeEnemies.Contains(enemy))
            activeEnemies.Remove(enemy);

        enemiesKilled++;

        if (hudManager != null)
            hudManager.UpdateMustKillText(totalEnemies - enemiesKilled);

        Debug.Log($"Musuh mati. Sisa musuh: {totalEnemies - enemiesKilled}");
    }

    // Fungsi ini yang dipanggil oleh Typer
    public List<GameObject> GetActiveEnemies()
    {
        // Hapus musuh yang sudah hancur (null) dari daftar
        activeEnemies.RemoveAll(enemy => enemy == null);
        return activeEnemies;
    }
}