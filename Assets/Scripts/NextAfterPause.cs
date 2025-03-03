using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextAfterPause : MonoBehaviour
{
    public GameObject panel;
    private bool isGamePaused = false;

    public void TogglePause()
    {
        isGamePaused = !isGamePaused;

        if (isGamePaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    public void PauseGame()
    {
        panel.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void ResumeGame()
    {
        panel.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }
}
