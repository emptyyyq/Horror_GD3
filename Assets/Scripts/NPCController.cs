using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class NPCController : MonoBehaviour
{


    public Transform player; // ��������� �� ��'��� ������
    public float detectionRange = 10f; // �����, � ����� NPC "������" ������
    public float wanderRadius = 20f; // ����� ��� ���������� ���������
    public float wanderInterval = 5f; // �������� �� ������������, ���� ������ �� �����

    private NavMeshAgent agent;
    private float wanderTimer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // �������� ��������� NavMeshAgent
        wanderTimer = wanderInterval;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            // ���� ������� � ��� ��������, ���������� ����
            agent.SetDestination(player.position);
        }
        else
        {
            // ���� ������� ���� ����� ��������, NPC ����������� ���������
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
        // ���� ��������� ����� � ����� wanderRadius
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
        // ³��������� ������ �������� � ��������
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}


