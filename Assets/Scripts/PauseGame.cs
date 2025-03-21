using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    public GameObject panel;
    private bool isGamePaused = false;

    public Button pauseButton;

    private void Start()
    {
        pauseButton.onClick.AddListener(TogglePause);
        panel.SetActive(false); // √арантовано ховаЇмо панель при старт≥ гри
    }

    public void TogglePause()
    {
        if (!isGamePaused)
        {
            PauseeGame();
        }
        else
        {
            ResumeGame();
        }
    }

    private void PauseeGame()
    {
        panel.SetActive(true); // ”¬≤ћ Ќ”“» панель
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    private void ResumeGame()
    {
        panel.SetActive(false); // ¬»ћ Ќ”“» панель
        Time.timeScale = 1f;
        isGamePaused = false;
    }
}

