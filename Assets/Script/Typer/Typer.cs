using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Playables;
using UnityEngine.UI;
using TMPro;

public class Typer : MonoBehaviour
{
     public TextMeshProUGUI wordOutput;

    private string remaingWord = string.Empty;
    private string currentWord = "kontol";
    void Start()
    {
        SetCurrentWord();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    private void SetCurrentWord()
    {
        SetRemainingWord(currentWord);
    }

    private void SetRemainingWord(string newString)
    {
        remaingWord = newString;
        wordOutput.text = remaingWord;
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
                  SetCurrentWord();
              }
              
            }
    }

    private bool IsCorrectLetter(string letter)
    {

        return remaingWord.IndexOf(letter) == 0;
    }

    private bool IsWordComplete()
    {
        return remaingWord.Length == 0;
    }

    private void RemoveLetter()
    {
        remaingWord = remaingWord.Remove(0, 1);
        wordOutput.text = remaingWord;
    }
    

}
