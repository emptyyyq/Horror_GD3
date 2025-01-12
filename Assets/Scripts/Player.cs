using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // Скорость движения
    [SerializeField] private float jumpForce = 5f; // Сила прыжка
    [SerializeField] private float groundCheckDistance = 0.3f; // Расстояние проверки на землю

    private Rigidbody rb;
    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Проверка на то, находится ли персонаж на земле
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);

        // Получение входных данных для движения (клавиши W, A, S, D или стрелки)
        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

        // Применение движения персонажа
        if (moveInput.magnitude > 0)
        {
            Vector3 moveDirection = transform.TransformDirection(moveInput); // Преобразуем в мировые координаты
            rb.MovePosition(transform.position + moveDirection * moveSpeed * Time.deltaTime); // Перемещаем с учетом скорости
        }

        // Логика прыжка
        if (isGrounded && Input.GetKeyDown(KeyCode.Space)) // Прыжок по нажатию пробела
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Применяем импульс для прыжка
        }

    }
}
