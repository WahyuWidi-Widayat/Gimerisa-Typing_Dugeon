using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Parameter Deteksi")]
    public float detectionRange = 5f;
    public float moveSpeed = 2f;

    private Transform player;
    private bool isPlayerDetected = false;

    public static event Action OnEnemyDied;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("Tidak ditemukan GameObject dengan tag 'Player' di scene!");
        }
    }

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
        isPlayerDetected = (distance <= detectionRange);
    }

    void MoveTowardPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("ENEMY BERTABRAKAN DENGAN: " + collision.gameObject.name);

        // Hancur hanya jika menabrak Player
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Enemy menabrak Player — hancur!");
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Satu musuh mati.");
        OnEnemyDied?.Invoke();
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
