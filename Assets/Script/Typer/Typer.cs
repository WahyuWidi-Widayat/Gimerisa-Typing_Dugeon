using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Typer : MonoBehaviour
{
    public GameObject enemy;              // referensi ke prefab atau musuh yang aktif
    public WordBank wordBank;
    public TextMeshProUGUI wordOutput;

    private string remainingWord = string.Empty;
    private string currentWord = string.Empty;

    void Start()
    {
        SetCurrentWord();
    }

    void Update()
    {
        CheckInput();
    }

    private void SetCurrentWord()
    {
        currentWord = wordBank.GetWord();
        SetRemainingWord(currentWord);
    }

    private void SetRemainingWord(string newString)
    {
        remainingWord = newString;
        wordOutput.text = remainingWord;
    }

    private void CheckInput()
    {
        if (Input.anyKeyDown)
        {
            string keyPressed = Input.inputString;
            if (keyPressed.Length == 1)
            {
                EnterLetter(keyPressed);
            }
        }
    }

    private void EnterLetter(string typedLetter)
    {
        if (IsCorrectLetter(typedLetter))
        {
            RemoveLetter();

            if (IsWordComplete())
            {
                // 🔥 Jika kata selesai, hancurkan musuh
                if (enemy != null)
                {
                    Destroy(enemy);
                    Debug.Log("Enemy destroyed!");
                }

                // 🔁 Ganti ke kata baru
                SetCurrentWord();
            }
        }
    }

    private bool IsCorrectLetter(string letter)
    {
        return remainingWord.IndexOf(letter) == 0;
    }

    private bool IsWordComplete()
    {
        return remainingWord.Length == 0;
    }

    private void RemoveLetter()
    {
        remainingWord = remainingWord.Remove(0, 1);
        wordOutput.text = remainingWord;
    }
}
