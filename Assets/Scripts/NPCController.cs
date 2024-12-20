using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public Transform player; // ��������� �� ������
    public float detectionRange = 10f; // ����� "����" NPC
    public float attackRange = 2f; // ��������� �����
    public float attackInterval = 2f; // �������� �� �������
    public int damage = 15; // �����, ��� ����� NPC

    private NavMeshAgent agent; // ��������� NavMeshAgent
    private float attackTimer; // ������ ��� ����

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError($"{gameObject.name} �� �� NavMeshAgent! ������� ���������.");
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
            agent.SetDestination(player.position); // NPC �������� ������

            // ���� ������� � ��� �����, ������� �����
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
                healthSlider.TakeDamage(damage); // ������� ����� ������
                Debug.Log($"NPC ����� {damage} ����� ������.");
            }

            attackTimer = attackInterval; // ������� ������ �����
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange); // ����� "����"
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange); // ����� �����
    }
}




