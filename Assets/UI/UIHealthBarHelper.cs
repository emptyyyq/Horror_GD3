using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBarHelper : MonoBehaviour
{
    public Slider healthSlider;
    public float maxHealth = 100f;
    private float currentHealth;

    [SerializeField]public GameObject losePanel;
    [SerializeField]public GameObject winPanel;
    [SerializeField] public GameObject stopPanel;

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;


        if (losePanel != null) losePanel.SetActive(false);
        if (winPanel != null) winPanel.SetActive(false);
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("�������� �����!");
        Time.timeScale = 0;
        if (losePanel != null) losePanel.SetActive(true); 
    }


    public void WinGame()
    {
        Debug.Log("�������� ������!");
        Time.timeScale = 0;
        if (winPanel != null) winPanel.SetActive(true); 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Syringe"))
        {
            TakeDamage(10f);
        }
        else if (collision.gameObject.CompareTag("Knife"))
        {
            TakeDamage(20f);
        }
    }
    public void StopGame()
    {
        Time.timeScale = 0;
        if (stopPanel != null) stopPanel.SetActive(true);
    }
}

