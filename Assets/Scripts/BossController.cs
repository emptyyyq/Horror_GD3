using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    public float health = 100f;
    public Slider healthSlider;
    public float damage = 10f; // ����, ���� ��� �������� ��� ������
    public float speed = 3f;
    public float detectionRadius = 10f;
    private bool isDead = false;

    private int keysCollected = 0;
    public int keysNeeded = 4; // ʳ������ ������ ��� ��������
    public float damagePerKey = 25f; // ������ HP ������ ��� �� ���� ����

    private GameObject player;
    private UIHealthBarHelper playerHealth; // ��������� �� ������� ������'� ������
    private Vector3 randomTarget;
    private float randomMoveCooldown = 3f;
    private float randomMoveTimer = 0f;
    private Rigidbody rb;
    public GameObject panel;



    private void Start()
    {
        playerHealth = FindObjectOfType<UIHealthBarHelper>(); // ��������� ������� ������'� ������
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

        Debug.Log($"������� ������ ����! ({keysCollected}/{keysNeeded}), HP �����: {health}");

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
            Debug.Log("��� �����������!");
            Invoke("ShowWinPanel", 2f);
            Destroy(gameObject, 2f);
        }
    }
    private void ShowWinPanel()
    {
        panel.SetActive(true);
        Time.timeScale = 0f; 
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player) // ���� ��� ����������� �� ������
        {
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage); // ��������� ������� � ������
                Debug.Log($"��� ������ ������! ���������� HP: {playerHealth.healthSlider.value}");
            }
        }
    }
    
}
