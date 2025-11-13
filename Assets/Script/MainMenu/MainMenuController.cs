using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject HowtoPlayPanel;
    public GameObject CreditsPanel;


    public void PlayGame()
    {
        SceneManager.LoadScene("Gameplay"); // Ganti dengan nama scene gameplay kamu
    }

    // Fungsi untuk tombol "How to Play"
    public void OpenHowToPlay()
    {
          HowtoPlayPanel.SetActive(true);
        CreditsPanel.SetActive(false);
    }

    // Fungsi untuk tombol "Credits"
    public void OpenCredits()
    {
        CreditsPanel.SetActive(true);
        HowtoPlayPanel.SetActive(false);
    }

    // Fungsi untuk tombol "Quit"
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game closed."); // Supaya terlihat di editor Unity
    }

    public void ExitButton()
    {
        HowtoPlayPanel.SetActive(false);
        CreditsPanel.SetActive(false);
    }
}
