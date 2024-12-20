using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public Transform player; // Посилання на гравця
    public float detectionRange = 10f; // Радіус "зору" NPC
    public float attackRange = 2f; // Дистанція атаки
    public float attackInterval = 2f; // Інтервал між атаками
    public int damage = 15; // Шкода, яку завдає NPC

    private NavMeshAgent agent; // Компонент NavMeshAgent
    private float attackTimer; // Таймер для атак

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError($"{gameObject.name} не має NavMeshAgent! Додайте компонент.");
            enabled = false;
            return;
        }

        attackTimer = attackInterval;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            agent.SetDestination(player.position); // NPC переслідує гравця

            // Якщо гравець в зоні атаки, завдаємо шкоди
            if (distanceToPlayer <= attackRange)
            {
                AttackPlayer();
            }
        }
    }

    void AttackPlayer()
    {
        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0f)
        {
            UIHealthBarHelper healthSlider = player.GetComponent<UIHealthBarHelper>();

            if (healthSlider != null)
            {
                healthSlider.TakeDamage(damage); // Завдаємо шкоди гравцю
                Debug.Log($"NPC завдає {damage} шкоди гравцю.");
            }

            attackTimer = attackInterval; // Скидаємо таймер атаки
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange); // Радіус "зору"
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange); // Радіус атаки
    }
}




