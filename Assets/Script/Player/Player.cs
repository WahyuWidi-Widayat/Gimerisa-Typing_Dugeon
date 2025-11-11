using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Settings")]
    public float speed = 5f;

    [Header("Health")]
    public int maxHealth = 5;
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        // Contoh: gerak kanan
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Player terkena serangan! Health: " + currentHealth);

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        Debug.Log("Player telah mati!");
        Time.timeScale = 0;
    }
}
