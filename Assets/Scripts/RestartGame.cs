using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public void RestartLevel()
    {
        Time.timeScale = 1f; // Відновлюємо нормальний хід часу
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Завантажуємо поточну сцену
    }
}
