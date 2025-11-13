using UnityEngine;
using System; // Untuk Action (Event)

// Enum ini bisa kamu simpan di file terpisah juga
public enum SpawnState
{
    Counting, // Status: Menunggu timer spawn berikutnya
    Spawning, // Status: Ingin spawn, tapi lapangan penuh (tertahan)
    Idle      // Status: Semua musuh sudah di-spawn
}

public class SpawnerController : MonoBehaviour
{
    [Header("Spawner Settings")]
    public SpawnState spawnState = SpawnState.Counting;

    [SerializeField] public float spawnInterval = 2f;    // Jeda antar spawn
    [SerializeField] public int totalEnemies = 10;     // Total musuh sepanjang game
    [SerializeField] public int maxEnemiesOnField = 3;   // Batas maksimum musuh aktif
    [SerializeField] public GameObject[] enemyPrefab;    // Prefab musuh
    [SerializeField] public Transform[] spawnerPoints;  // Titik spawn

    private float nextSpawnTime = 0f;
    private int enemiesSpawned = 0; // Total yang sudah pernah di-spawn
    private int enemiesAlive = 0;   // Musuh aktif di lapangan

    // Event untuk GameController / HUD
    public event Action<int> OnEnemySpawned;

    // Property agar bisa dibaca dari luar
    public int EnemiesSpawned => enemiesSpawned;
    public int EnemiesAlive => enemiesAlive;

    // Daftarkan listener saat aktif
    private void OnEnable()
    {
        Enemy.OnEnemyDied += HandleEnemyDied;
    }

    // Lepas listener saat nonaktif untuk mencegah error
    private void OnDisable()
    {
        Enemy.OnEnemyDied -= HandleEnemyDied;
    }

    private void Start()
    {
        spawnState = SpawnState.Counting; // Mulai dengan menunggu
        nextSpawnTime = Time.time + spawnInterval; // Set timer pertama
    }

    // --- FUNGSI UPDATE YANG DIPERBAIKI ---
    private void Update()
    {
        // 1. Jika state Idle (selesai), jangan lakukan apa-apa
        if (spawnState == SpawnState.Idle)
        {
            return;
        }

        // 2. Cek apakah semua musuh sudah di-spawn
        if (enemiesSpawned >= totalEnemies)
        {
            spawnState = SpawnState.Idle;
            Debug.Log("✅ Semua musuh telah di-spawn!");
            return;
        }

        // 3. Cek apakah sudah waktunya spawn (State Counting ATAU Spawning)
        if (Time.time >= nextSpawnTime)
        {
            // 4. Cek apakah lapangan masih ada tempat
            if (enemiesAlive < maxEnemiesOnField)
            {
                // --- BISA SPAWN ---
                SpawnEnemy();
                
                // Set timer untuk spawn berikutnya
                nextSpawnTime = Time.time + spawnInterval;
                
                // KEMBALI ke state Counting (menunggu timer)
                spawnState = SpawnState.Counting;
            }
            else
            {
                // --- TIDAK BISA SPAWN (Lapangan Penuh) ---
                
                // Tetap di state Spawning. Ini menandakan
                // spawner "tertahan" menunggu slot kosong.
                spawnState = SpawnState.Spawning;
                
                // Kita TIDAK reset 'nextSpawnTime'.
                // Ini berarti frame berikutnya 'Time.time >= nextSpawnTime' akan
                // true lagi, dan spawner akan cek lagi (setiap frame)
                // sampai ada slot kosong.
            }
        }
    }
    // --- AKHIR FUNGSI UPDATE ---

    private void SpawnEnemy()
    {
        if (enemyPrefab.Length == 0 || spawnerPoints.Length == 0)
        {
            Debug.LogWarning("⚠️ Prefab musuh atau spawn point belum diisi di Inspector!");
            return;
        }

        // Pilih musuh dan titik spawn secara acak
        GameObject randomEnemy = enemyPrefab[UnityEngine.Random.Range(0, enemyPrefab.Length)];
        Transform randomPoint = spawnerPoints[UnityEngine.Random.Range(0, spawnerPoints.Length)];

        // Spawn musuh di dunia
        Instantiate(randomEnemy, randomPoint.position, randomPoint.rotation);

        // Update counter
        enemiesSpawned++;
        enemiesAlive++;

        // Kirim event ke GameController / HUD
        OnEnemySpawned?.Invoke(enemiesSpawned);

        Debug.Log($"🟢 Spawn musuh ke-{enemiesSpawned} | Aktif di lapangan: {enemiesAlive}");
    }

    // Fungsi ini sekarang sudah benar.
    // Tidak perlu mengubah state di sini.
    private void HandleEnemyDied()
    {
        enemiesAlive = Mathf.Max(0, enemiesAlive - 1);
        Debug.Log($"🔴 Musuh mati! Sisa musuh aktif: {enemiesAlive}");

        // Saat musuh mati, 'enemiesAlive' berkurang.
        // Jika spawner sedang "tertahan" di state 'Spawning',
        // di frame Update berikutnya, 'if (enemiesAlive < maxEnemiesOnField)'
        // akan lolos, memanggil SpawnEnemy(), dan kembali ke 'Counting'.
        
        // Kode Anda sebelumnya: if (enemiesSpawned < totalEnemies) { spawnState = SpawnState.Spawning; }
        // Sebenarnya tidak diperlukan lagi dengan logika Update yang baru ini.
        // Tapi jika dibiarkan pun tidak akan merusak.
    }

    // 🔹 Optional: visualisasi di Scene View
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        foreach (Transform point in spawnerPoints)
        {
            if (point != null)
                Gizmos.DrawWireSphere(point.position, 0.5f);
        }
    }
}