using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject panel;
    private bool isGamePaused = false;

    public void GamePause()
    {
        panel.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }
    
}
