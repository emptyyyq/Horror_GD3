using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
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

    }

}
