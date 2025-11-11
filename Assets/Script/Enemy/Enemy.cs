using UnityEngine;
using TMPro; // <- TAMBAHKAN INI

public class Enemy : MonoBehaviour
{
    [Header("Parameter Deteksi")]
    public float detectionRange = 5f;
    public float moveSpeed = 2f;
    public int health = 1;

    [Header("Word Typing")]
    public string word; // <- TAMBAHKAN INI (untuk menyimpan kata)
    public TextMeshProUGUI wordOutput; // <- TAMBAHKAN INI (untuk text di atas kepala)

    [HideInInspector] public SpawnerController spawnerController;
    
    private Transform player;
    private bool isPlayerDetected = false;
    private WordBank wordBank; // <- TAMBAHKAN INI

    void Start()
    {
        // Cari player
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("Tidak ditemukan GameObject dengan tag 'Player' di scene!");
        }

        // --- TAMBAHAN BARU DIMULAI DI SINI ---
        
        // 1. Cari WordBank di scene
        wordBank = FindObjectOfType<WordBank>();
        if (wordBank == null)
        {
            Debug.LogError("Tidak ada WordBank di scene!");
            return;
        }

        // 2. Ambil kata baru
        word = wordBank.GetWord();

        // 3. Tampilkan kata di atas kepala
        if (wordOutput != null)
        {
            wordOutput.text = word;
        }
        else
        {
            Debug.LogWarning("wordOutput (Text di atas kepala) belum di-assign di prefab Enemy!");
        }
        // --- TAMBAHAN BARU BERAKHIR DI SINI ---
    }

    // ... (Fungsi Update, DetectPlayer, MoveTowardPlayer, OnCollisionEnter2D, TakeDamage tidak berubah) ...
    void Update()
    {
        if (player == null) return;
        DetectPlayer();
        if (isPlayerDetected)
        {
            MoveTowardPlayer();
        }
    }
    void DetectPlayer()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= detectionRange) isPlayerDetected = true;
        else isPlayerDetected = false;
    }
    void MoveTowardPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Player playerScript = collision.collider.GetComponent<Player>();
            if (playerScript != null)
            {
                playerScript.TakeDamage(1);
            }
            Die();
        }
    }
    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
            Die();
    }
    
    // Fungsi Die() tidak berubah
    public void Die() // <- Ubah dari 'void' menjadi 'public void'
    {
        if (spawnerController != null)
        {
            spawnerController.OnEnemyKilled(gameObject);
        }
        Destroy(gameObject);
    }

    // --- FUNGSI BARU UNTUK DIPANGGIL TYPER ---
    public void UpdateWordDisplay(string remaining)
    {
        if (wordOutput != null)
        {
            wordOutput.text = remaining;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}