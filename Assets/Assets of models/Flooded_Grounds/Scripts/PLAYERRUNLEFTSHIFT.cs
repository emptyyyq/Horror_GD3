using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 0.3f; // �������� ������
    public float runSpeed = 5f; // �������� ���
    private float currentSpeed; // ������� ��������

    private Rigidbody rb; // Rigidbody ��� ����
    private Vector3 movement; // �������� ����

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = walkSpeed; // �������� ������� ������
    }

    void Update()
    {
        // ��������� ����� � ���������
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // ��������� ������� ����
        movement = new Vector3(horizontal, 0f, vertical).normalized;

        // ��������, �� ���������� ���� Shift
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = runSpeed; // ���
        }
        else
        {
            currentSpeed = walkSpeed; // ������
        }
    }

    void FixedUpdate()
    {
        // ��� ������
        if (movement.magnitude > 0f)
        {
            rb.MovePosition(rb.position + movement * currentSpeed * Time.fixedDeltaTime);
        }
    }
}

