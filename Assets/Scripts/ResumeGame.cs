using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResumeGame : MonoBehaviour
{
    public Button toggleButton; 
    private bool isGamePaused = false; 

    void Start()
    {
        if (toggleButton != null)
        {
            toggleButton.onClick.AddListener(ToggleGame);
        }
    }

    public void ToggleGame()
    {
        if (isGamePaused)
        {
            ResumeGameplay();
        }
        else
        {
            StopGame();
        }
    }

    private void StopGame()
    {
        Time.timeScale = 0; 
        isGamePaused = true; 
        Debug.Log("��� ��������");
    }


    private void ResumeGameplay()
    {
        Time.timeScale = 1; 
        isGamePaused = false; 
        Debug.Log("��� ����������");
    }
}
