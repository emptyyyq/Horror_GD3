using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class NPCController : MonoBehaviour
{
    [SerializeField] private UIHealthBarHelper healthBarHelper;
    public Player player; // Посилання на гравця
    public float detectionRange = 10f; // Радіус "зору" NPC
    public float attackRange = 2f; // Дистанція атакияяя
    public float attackInterval = 2f; // Інтервал між атаками
    public int damage = 15; // Шкода, яку завдає NPC

    private NavMeshAgent agent; // Компонент NavMeshAgent
    private float attackTimer; // Таймер для атак

    public float wanderRadius = 10f; // Радіус випадкового руху
    public float wanderTimer = 5f; // Таймер між змінами цілей
    private float wanderTimeCounter;

    // Здоров'я NPC
    public float maxHealth = 150f;
    private float currentHealth;

    public Slider healthSlider; // Слайдер для відображення HP над NPC
    public GameObject keyPrefab; // Префаб ключа, який скидає NPC при смерті

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
        wanderTimeCounter = wanderTimer;

        // Ініціалізація здоров'я
        currentHealth = maxHealth;
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    void Update()
    {
        //return;
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= detectionRange)
        {
            agent.SetDestination(player.transform.position); // NPC переслідує гравця

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
        //return;
        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0f)
        {
            UIHealthBarHelper healthSlider = healthBarHelper;

            if (healthSlider != null)
            {
                healthSlider.TakeDamage(damage);
                Debug.Log($"NPC завдає {damage} шкоди гравцю.");
            }

            attackTimer = attackInterval;
        }

    }

    void Wander()
    {
        //return;
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

    // Метод для отримання пошкоджень
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
        Debug.Log("NPC загинув!");

        // Скидаємо ключ
        if (keyPrefab != null)
        {
            Instantiate(keyPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject); // Видаляємо NPC
    }

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log(collider.gameObject.name);
        if (collider.gameObject.CompareTag("Syringe"))
        {
            TakeDamage(10f);
        }
        else if (collider.gameObject.CompareTag("Knife"))
        {
            TakeDamage(20f);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange); // Радіус "зору"
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange); // Радіус атаки
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, wanderRadius); // Радіус випадкового руху
    }
}

