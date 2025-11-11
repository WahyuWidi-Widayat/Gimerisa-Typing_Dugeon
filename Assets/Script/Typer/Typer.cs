using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Typer : MonoBehaviour
{
    // Referensi ke Spawner, bukan 1 musuh
    public SpawnerController spawnerController; 
    public TextMeshProUGUI wordOutput; // Ini UI utama di bawah layar

    // Dihapus: public GameObject enemy;
    // Dihapus: public WordBank wordBank;
    
    private Enemy targetEnemy; // Musuh yang sedang diketik
    private string remainingWord = string.Empty;
    private string currentWord = string.Empty;

    void Start()
    {
        targetEnemy = null;
        wordOutput.text = ""; // Kosongkan UI
    }

    void Update()
    {
        CheckInput();

        // Jika target hancur (misal, tabrak player) saat sedang diketik, reset
        if (targetEnemy != null && targetEnemy.gameObject == null)
        {
            ResetTyper();
        }
    }

    private void ResetTyper()
    {
        targetEnemy = null;
        remainingWord = string.Empty;
        currentWord = string.Empty;
        wordOutput.text = "";
    }

    private void CheckInput()
    {
        if (Input.anyKeyDown)
        {
            string keyPressed = Input.inputString.ToLower(); // Selalu lowercase
            if (keyPressed.Length == 1)
            {
                EnterLetter(keyPressed);
            }
        }
    }

    private void EnterLetter(string typedLetter)
    {
        // KASUS 1: BELUM ADA TARGET
        if (targetEnemy == null)
        {
            if (spawnerController == null)
            {
                Debug.LogError("SpawnerController belum di-assign ke Typer!");
                return;
            }

            // Cari musuh yang katanya dimulai dengan huruf yang diketik
            foreach (GameObject enemyObj in spawnerController.GetActiveEnemies())
            {
                if (enemyObj == null) continue; // Lewati jika musuh null

                Enemy enemyScript = enemyObj.GetComponent<Enemy>();
                if (enemyScript != null && enemyScript.word.StartsWith(typedLetter))
                {
                    // KUNCI TARGET!
                    targetEnemy = enemyScript;
                    currentWord = enemyScript.word;
                    remainingWord = currentWord;
                    
                    // Optional: Beri efek visual pada target
                    // targetEnemy.GetComponent<SpriteRenderer>().color = Color.yellow;

                    RemoveLetter(); // Proses huruf pertama
                    break; // Berhenti mencari setelah dapat target
                }
            }
        }
        // KASUS 2: SUDAH ADA TARGET
        else
        {
            if (IsCorrectLetter(typedLetter))
            {
                RemoveLetter();

                if (IsWordComplete())
                {
                    // Musuh mati!
                    targetEnemy.Die();
                    
                    // Reset typer untuk siap cari target baru
                    ResetTyper();
                }
            }
        }
    }

    private bool IsCorrectLetter(string letter)
    {
        // Cek apakah huruf tersisa dimulai dengan huruf yang diketik
        return remainingWord.IndexOf(letter) == 0;
    }

    private bool IsWordComplete()
    {
        return remainingWord.Length == 0;
    }

    private void RemoveLetter()
    {
        remainingWord = remainingWord.Remove(0, 1);
        
        // Update UI di bawah layar
        wordOutput.text = remainingWord;

        // Update juga text di atas kepala musuh
        if (targetEnemy != null)
        {
            targetEnemy.UpdateWordDisplay(remainingWord);
        }
    }
}