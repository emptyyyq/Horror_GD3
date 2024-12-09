using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class NPCController : MonoBehaviour
{


    public Transform player; // Посилання на об'єкт гравця
    public float detectionRange = 10f; // Радіус, у якому NPC "бачить" гравця
    public float wanderRadius = 20f; // Радіус для випадкових переміщень
    public float wanderInterval = 5f; // Інтервал між переміщеннями, якщо гравця не видно

    private NavMeshAgent agent;
    private float wanderTimer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // Отримуємо компонент NavMeshAgent
        wanderTimer = wanderInterval;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            // Якщо гравець у зоні видимості, переслідуємо його
            agent.SetDestination(player.position);
        }
        else
        {
            // Якщо гравець поза зоною видимості, NPC переміщається випадково
            wanderTimer -= Time.deltaTime;

            if (wanderTimer <= 0f)
            {
                Wander();
                wanderTimer = wanderInterval;
            }
        }
    }

    void Wander()
    {
        // Вибір випадкової точки в межах wanderRadius
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += transform.position;

        NavMeshHit navHit;
        if (NavMesh.SamplePosition(randomDirection, out navHit, wanderRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(navHit.position);
        }
    }

    void OnDrawGizmosSelected()
    {
        // Візуалізація радіусу видимості у редакторі
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}


