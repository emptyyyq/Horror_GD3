using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class NPCController : MonoBehaviour
{
    public Transform player; // ��������� �� ������
    public float detectionRange = 10f; // ����� "����" NPC
    public float attackRange = 2f; // ��������� �����
    public float attackInterval = 2f; // �������� �� �������
    public int damage = 15; // �����, ��� ����� NPC

    private NavMeshAgent agent; // ��������� NavMeshAgent
    private float attackTimer; // ������ ��� ����

    public float wanderRadius = 10f; // ����� ����������� ����
    public float wanderTimer = 5f; // ������ �� ������ �����
    private float wanderTimeCounter;

    // ������'� NPC
    public float maxHealth = 150f;
    private float currentHealth;

    public Slider healthSlider; // ������� ��� ����������� HP ��� NPC
    public GameObject keyPrefab; // ������ �����, ���� ����� NPC ��� �����

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
        wanderTimeCounter = wanderTimer;

        // ����������� ������'�
        currentHealth = maxHealth;
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            agent.SetDestination(player.position); // NPC �������� ������

            if (distanceToPlayer <= attackRange)
            {
                AttackPlayer();
            }
        }
        else
        {
            Wander();
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
                healthSlider.TakeDamage(damage);
                Debug.Log($"NPC ����� {damage} ����� ������.");
            }

            attackTimer = attackInterval;
        }
    }

    void Wander()
    {
        wanderTimeCounter -= Time.deltaTime;

        if (wanderTimeCounter <= 0f)
        {
            Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
            randomDirection += transform.position;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
            }

            wanderTimeCounter = wanderTimer;
        }
    }

    // ����� ��� ��������� ����������
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("NPC �������!");

        // ������� ����
        if (keyPrefab != null)
        {
            Instantiate(keyPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject); // ��������� NPC
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange); // ����� "����"
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange); // ����� �����
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, wanderRadius); // ����� ����������� ����
    }
}

