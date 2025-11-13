using System.Collections;
using UnityEngine;
using System; // Untuk Action (Events)
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header("Referensi Scene")]
    public Player player;
    public SpawnerController spawnerController;

    [Header("UI Canvases")]
    public GameObject gameOverScreen;
    public GameObject victoryScreen;

    // Variabel internal
    private bool spawnerFinished = false;
    private bool gameIsOver = false; // Flag untuk mencegah event ganda

    void Start()
    {
        // 1. Matikan semua UI di awal
        if (gameOverScreen != null) gameOverScreen.SetActive(false);
        if (victoryScreen != null) victoryScreen.SetActive(false);

        // 2. Pastikan game berjalan
        Time.timeScale = 1f;

        // 3. Validasi referensi
        if (player == null)
        {
            Debug.LogError("❌ Player belum di-assign di GameController!");
            return;
        }
        if (spawnerController == null)
        {
            Debug.LogError("❌ SpawnerController belum di-assign di GameController!");
            return;
        }

        // 4. Berlangganan ke event
        player.OnHealthChanged += HandlePlayerHealthChanged;
        spawnerController.OnEnemySpawned += HandleEnemySpawned;
        Enemy.OnEnemyDied += HandleEnemyDied;
    }

    // === Event dari Player ===
    private void HandlePlayerHealthChanged(int currentHealth)
    {
        if (currentHealth <= 0 && !gameIsOver)
        {
            ShowGameOver();
        }
    }

    // === Event dari Spawner ===
    private void HandleEnemySpawned(int totalSpawnedSoFar)
    {
        // Cek apakah semua musuh sudah selesai di-spawn
        if (totalSpawnedSoFar == spawnerController.totalEnemies)
        {
            spawnerFinished = true;
            Debug.Log("Spawner selesai spawn semua musuh.");
            CheckForVictory(); // Mungkin langsung menang kalau musuhnya 0
        }
    }

    // === Event dari Enemy ===
    private void HandleEnemyDied()
    {
        if (gameIsOver) return;

        Debug.Log($"Enemy mati | Tersisa: {spawnerController.EnemiesAlive}");

        CheckForVictory();
    }

    // === Logika menang ===
    private void CheckForVictory()
    {
        if (spawnerController == null) return;

        // Hanya jalankan kalau semua musuh sudah di-spawn DAN tidak ada musuh hidup
        if  ( spawnerController.EnemiesSpawned == spawnerController.totalEnemies)
        {
            Debug.Log("Semua musuh sudah mati, mulai countdown 2 detik untuk menang...");
            StartCoroutine(VictoryDelayCoroutine());
        }
    }

    // === Coroutine tunggu 2 detik ===
    private IEnumerator VictoryDelayCoroutine()
    {
        gameIsOver = true; // Pastikan tidak dipanggil dua kali
        yield return new WaitForSeconds(3f);
        ShowVictory();
    }

    private void ShowGameOver()
    {
        if (gameIsOver) return;
        gameIsOver = true;

        Debug.Log("💀 GAME OVER!");
        if (gameOverScreen != null)
            gameOverScreen.SetActive(true);
        else
            Debug.LogWarning("⚠️ GameOverScreen belum di-assign!");

        Time.timeScale = 0f;
        UnsubscribeEvents();
    }

    private void ShowVictory()
    {
        Debug.Log("🏆 VICTORY!");
        if (victoryScreen != null)
            victoryScreen.SetActive(true);
        else
            Debug.LogWarning("⚠️ VictoryScreen belum di-assign!");

        Time.timeScale = 0f;
        UnsubscribeEvents();
    }

    // === Unsubscribe untuk mencegah memory leak ===
    private void UnsubscribeEvents()
    {
        if (player != null) player.OnHealthChanged -= HandlePlayerHealthChanged;
        if (spawnerController != null) spawnerController.OnEnemySpawned -= HandleEnemySpawned;
        Enemy.OnEnemyDied -= HandleEnemyDied;
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }

    public void RetryGame()
    {
        SceneManager.LoadScene("Gameplay"); // Ganti dengan nama scene kamu
    }

    // Dipanggil saat tombol Main Menu ditekan
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Ganti dengan nama scene kamu
    }
    
}
