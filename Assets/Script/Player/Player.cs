using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; // 1. TAMBAHKAN INI untuk Action (Event)

public class Player : MonoBehaviour
{
    [Header("Player Status")]
    public float speed;
    public int health;

    // 2. TAMBAHKAN EVENT INI
    // Event ini akan "berteriak" dan mengirimkan nilai health baru
    public event Action<int> OnHealthChanged;

    void Start()
    {
        // Mengirimkan nilai health awal saat game dimulai
        OnHealthChanged?.Invoke(health);
    }

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            health -= 1;
            Debug.Log("Player terkena serangan musuh! Health: " + health);

            // 3. PICU EVENT SETELAH HEALTH BERUBAH
            // Beri tahu GameController (dan HUDManager) bahwa health berubah
            OnHealthChanged?.Invoke(health);

            if (health <= 0)
            {
                Debug.Log("Player telah mati!");
                // Kita HAPUS 'Time.timeScale = 0' dari sini.
                // Biarkan GameController yang mengurus Game Over.
            }
        }
    }
}