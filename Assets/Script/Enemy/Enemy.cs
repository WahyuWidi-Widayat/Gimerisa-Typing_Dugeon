using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Parameter Deteksi")]
    public float detectionRange = 5f; // jarak pandang musuh
    public float moveSpeed = 2f;      // kecepatan bergerak

    private Transform player; // tidak perlu diisi manual
    private bool isPlayerDetected = false;

    void Start()
    {
        // Cari object yang memiliki tag "Player"
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
        if (player == null) return; // kalau belum ketemu player, hentikan

        DetectPlayer();

        if (isPlayerDetected)
        {
            MoveTowardPlayer();
        }
    }

    void DetectPlayer()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            isPlayerDetected = true;
            Debug.Log("Player terdeteksi!");
        }
        else
        {
            isPlayerDetected = false;
        }
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
            Debug.Log("Enemy menabrak Player — hancur!");
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
