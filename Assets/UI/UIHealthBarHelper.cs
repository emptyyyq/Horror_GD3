using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBarHelper : MonoBehaviour
{
    public Slider healthSlider;
    public float maxHealth = 100f; 
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
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
        Debug.Log("Персонаж погиб!");
        Time.timeScale = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) 
        {
            TakeDamage(10f);
        }
    }
}
