using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5f;             // �������� ��������
    [SerializeField] private float rotationSpeed = 10f;     // �������� ��������
    [SerializeField] private float jumpForce = 5f;          // ���� ������
    [SerializeField] private float groundCheckDistance = 0.3f; // ��� �������� �����

    private Rigidbody rb;
    private bool isGrounded;  // ���� ��� ��������, �� ����� �� �����

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // ���������, �� ����� �� �����
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);

        // �������� ���� ��������
        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        // ���� ���� ��������, ������������ �������� � �����������
        if (moveInput.sqrMagnitude > 0.01f)  // �������� �� ��������� ������
        {
            // ������������ ����������� ��������
            Vector3 moveDirection = moveInput.normalized;

            // ������������ �������� �� ����������� ������� ��������
            Quaternion rotation = Quaternion.LookRotation(moveDirection);
            rotation.x = 0; // ��������� ������ �������� �� ��� Y
            rotation.z = 0;

            // ������ ������������ ������
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

            // ��������� �������� ��������
            rb.velocity = new Vector3(moveDirection.x * speed, rb.velocity.y, moveDirection.z * speed);
        }
        else
        {
            // ���� ����� �� ���������, ��������� �������� �� Y (����������)
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }

        // ��������� ������
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

}
