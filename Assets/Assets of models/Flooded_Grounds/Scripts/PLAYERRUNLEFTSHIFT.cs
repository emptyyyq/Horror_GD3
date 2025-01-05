using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 0.3f; // Швидкість ходьби
    public float runSpeed = 5f; // Швидкість бігу
    private float currentSpeed; // Поточна швидкість

    private Rigidbody rb; // Rigidbody для руху
    private Vector3 movement; // Напрямок руху

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = walkSpeed; // Спочатку гравець ходить
    }

    void Update()
    {
        // Отримання вводу з клавіатури
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Створення вектора руху
        movement = new Vector3(horizontal, 0f, vertical).normalized;

        // Перевірка, чи затиснутий лівий Shift
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = runSpeed; // Біг
        }
        else
        {
            currentSpeed = walkSpeed; // Ходьба
        }
    }

    void FixedUpdate()
    {
        // Рух гравця
        if (movement.magnitude > 0f)
        {
            rb.MovePosition(rb.position + movement * currentSpeed * Time.fixedDeltaTime);
        }
    }
}

