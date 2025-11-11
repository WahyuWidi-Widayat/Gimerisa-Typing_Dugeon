using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Status")]
    public float speed;

    public int health;

    void Start()
    {
        
    }

    // Update is called once per frame
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
            if (health <= 0)
            {
                Debug.Log("Player telah mati!");
               Time.timeScale = 0; // Hentikan permainan
            }
        }
    }
}
