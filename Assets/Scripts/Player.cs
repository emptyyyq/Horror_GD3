using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5f;             // Скорость движения
    [SerializeField] private float rotationSpeed = 10f;     // Скорость вращения
    [SerializeField] private float jumpForce = 5f;          // Сила прыжка
    [SerializeField] private float groundCheckDistance = 0.3f; // Для проверки земли

    private Rigidbody rb;
    private bool isGrounded;  // Флаг для проверки, на земле ли игрок

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Проверяем, на земле ли игрок
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);

        // Получаем ввод движения
        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        // Если есть движение, обрабатываем вращение и перемещение
        if (moveInput.sqrMagnitude > 0.01f)  // Проверка на ненулевой вектор
        {
            // Рассчитываем направление движения
            Vector3 moveDirection = moveInput.normalized;

            // Рассчитываем вращение по направлению вектора движения
            Quaternion rotation = Quaternion.LookRotation(moveDirection);
            rotation.x = 0; // Оставляем только вращение по оси Y
            rotation.z = 0;

            // Плавно поворачиваем объект
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

            // Применяем скорость движения
            rb.velocity = new Vector3(moveDirection.x * speed, rb.velocity.y, moveDirection.z * speed);
        }
        else
        {
            // Если игрок не двигается, сохраняем скорость по Y (гравитация)
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }

        // Обработка прыжка
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

}
