using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // �������� ��������
    [SerializeField] private float jumpForce = 5f; // ���� ������
    [SerializeField] private float groundCheckDistance = 0.3f; // ���������� �������� �� �����

    private Rigidbody rb;
    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // �������� �� ��, ��������� �� �������� �� �����
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);

        // ��������� ������� ������ ��� �������� (������� W, A, S, D ��� �������)
        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

        // ���������� �������� ���������
        if (moveInput.magnitude > 0)
        {
            Vector3 moveDirection = transform.TransformDirection(moveInput); // ����������� � ������� ����������
            rb.MovePosition(transform.position + moveDirection * moveSpeed * Time.deltaTime); // ���������� � ������ ��������
        }

        // ������ ������
        if (isGrounded && Input.GetKeyDown(KeyCode.Space)) // ������ �� ������� �������
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // ��������� ������� ��� ������
        }

    }
}
