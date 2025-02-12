using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    public float health = 100f;
    public Slider healthSlider;
    public float damage = 10f; // Урон, який бос наносить при дотику
    public float speed = 3f;
    public float detectionRadius = 10f;
    private bool isDead = false;

    private int keysCollected = 0;
    public int keysNeeded = 4; // Кількість ключів для перемоги
    public float damagePerKey = 25f; // Скільки HP втрачає бос за один ключ

    private GameObject player;
    private UIHealthBarHelper playerHealth; // Посилання на систему здоров'я гравця
    private Vector3 randomTarget;
    private float randomMoveCooldown = 3f;
    private float randomMoveTimer = 0f;
    private Rigidbody rb;

    private void Start()
    {
        playerHealth = FindObjectOfType<UIHealthBarHelper>(); // Знаходимо систему здоров'я гравця
        if (playerHealth != null)
        {
            player = playerHealth.gameObject;
        }

        healthSlider.maxValue = health;
        healthSlider.value = health;

        rb = GetComponent<Rigidbody>();
        SetRandomTarget();
    }

    private void Update()
    {
        if (isDead) return;

        randomMoveTimer += Time.deltaTime;

        if (PlayerInSight())
        {
            MoveTo(player.transform.position);
        }
        else
        {
            if (randomMoveTimer >= randomMoveCooldown)
            {
                SetRandomTarget();
                randomMoveTimer = 0f;
            }
            MoveTo(randomTarget);
        }

        if (keysCollected >= keysNeeded)
        {
            Die();
        }
    }

    private bool PlayerInSight()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            return distanceToPlayer <= detectionRadius;
        }
        return false;
    }

    private void MoveTo(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
    }

    private void SetRandomTarget()
    {
        randomTarget = new Vector3(
            transform.position.x + Random.Range(-detectionRadius, detectionRadius),
            transform.position.y,
            transform.position.z + Random.Range(-detectionRadius, detectionRadius)
        );
    }

    public void TakeDamageFromKey()
    {
        if (isDead) return;

        keysCollected++;
        health -= damagePerKey;
        healthSlider.value = health;

        Debug.Log($"Гравець підібрав ключ! ({keysCollected}/{keysNeeded}), HP босса: {health}");

        if (keysCollected >= keysNeeded)
        {
            Die();
        }
    }

    private void Die()
    {
        if (!isDead)
        {
            isDead = true;
            Debug.Log("Бос переможений!");
            Destroy(gameObject, 2f);
        }
    }

    // БОС НАНОСИТЬ УРОН ГРАВЦЮ ПРИ ДОТИКУ
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player) // Якщо бос доторкнувся до гравця
        {
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage); // Викликаємо функцію у гравця
                Debug.Log($"Бос вдарив гравця! Залишилося HP: {playerHealth.healthSlider.value}");
            }
        }
    }
}
