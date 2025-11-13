using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System; // Diperlukan untuk Action

public class HUDManager : MonoBehaviour
{
    public TextMeshProUGUI mustKillText;
    public TextMeshProUGUI PlayerHaelthText; // Tetap 'Haelth' sesuai nama variabel Anda

    public SpawnerController spawnerController;
    public Player player;

    private int totalEnemies;
    public int SpawnRemaining;

    void Start()
    {
        // 1. Pengecekan Error
        if (spawnerController == null)
        {
            Debug.LogError("Referen SpawnerController belum di-assign di HUDManager!");
            return; // Hentikan jika error
        }
        if (player == null)
        {
            Debug.LogError("Referen Player belum di-assign di HUDManager!");
            return; // Hentikan jika error
        }

        // 2. Simpan nilai total dan set UI untuk pertama kali
        totalEnemies = spawnerController.totalEnemies;
        
        // Memanggil fungsi helper untuk mengatur teks awal
        UpdatePlayerHealthText(player.health);
        UpdateEnemyCountText(spawnerController.EnemiesSpawned);

        // 3. BERLANGGANAN (SUBSCRIBE) EVENT
        // Saat 'player' memicu 'OnHealthChanged', panggil fungsi 'UpdatePlayerHealthText'
        player.OnHealthChanged += UpdatePlayerHealthText;

        // Saat 'spawnerController' memicu 'OnEnemySpawned', panggil 'UpdateEnemyCountText'
        spawnerController.OnEnemySpawned += UpdateEnemyCountText;
    }

    // Fungsi Update() SEKARANG KOSONG DAN TIDAK DIPERLUKAN
    void Update()
    {
        // Tidak ada kode di sini! :)
    }

    // Fungsi ini dipanggil OTOMATIS oleh event Player
    private void UpdatePlayerHealthText(int currentHealth)
    {
        if (PlayerHaelthText != null)
        {
            PlayerHaelthText.text = ": " +currentHealth;
        }
    }

    // Fungsi ini dipanggil OTOMATIS oleh event Spawner
    private void UpdateEnemyCountText(int spawnedCount)
    {
        if (mustKillText != null)
        {
            int SpawnRemaining = totalEnemies - spawnedCount;
            
            mustKillText.text = ": " + SpawnRemaining;
        }
    }

    // PENTING: Selalu berhenti berlangganan saat objek hancur
    // untuk menghindari error (memory leaks)
    void OnDestroy()
    {
        // Pastikan referensi masih ada sebelum berhenti berlangganan
        if (player != null)
        {
            player.OnHealthChanged -= UpdatePlayerHealthText;
        }
        if (spawnerController != null)
        {
            spawnerController.OnEnemySpawned -= UpdateEnemyCountText;
        }
    }
}