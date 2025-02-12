using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform ItemHolder;
    [SerializeField] private Animator animator;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float groundCheckDistance = 0.3f;
    [SerializeField] private Transform leftHand; // Теперь ссылаемся на левую руку

    private Rigidbody rb;
    private bool isGrounded;

    private GameObject currentWeapon; // Текущее оружие в руках

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Проверка на то, находится ли персонаж на земле
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);

        // Управление движением
        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        if (moveInput.magnitude > 0)
        {
            Vector3 moveDirection = transform.TransformDirection(moveInput);
            rb.MovePosition(transform.position + moveDirection * moveSpeed * Time.deltaTime);
            animator.SetBool("IsWalking", true);
        }
        else { animator.SetBool("IsWalking", false); }

        // Логика прыжка
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    // Метод для добавления оружия в левую руку
    public void EquipWeapon(GameObject weapon)
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon); // Удаляем текущее оружие, если оно есть
        }

        currentWeapon = Instantiate(weapon, ItemHolder); // Создаем оружие в левой руке
        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.localRotation = Quaternion.identity;
    }
}
