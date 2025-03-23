using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform MALE2; // ������ �� ������ ������ (������������ ������ ������)
    public float mouseSensitivity = 350f; // ���������������� ����
    public float movementSpeed = 3f; // �������� ��������

    private float xRotation = 0f; // ������� ���� �������� ������ �� ��� X (�����/����)
    private Health playerHealth; // ������ �� ��������� Health

    void Start()
    {
        // ���������� �������� ������, ��� ��� �������� ������ 0
        Cursor.lockState = CursorLockMode.Locked;  // ��������� ������ � ������ ������
        Cursor.visible = false;  // �������� ������

        // �������������� ������ �� ��������� �������� ������
        playerHealth = MALE2.GetComponent<Health>();
    }

    void Update()
    {
        // ��������� ������� �������� ������
        if (playerHealth != null && playerHealth.currentHealth <= 0)
        {
            // ���� �������� 0 ��� ������, ���������� ������
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            // ���� �������� ������ 0, �������� ������
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        // �������� ���� ���� ��� �������� ������
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // ��������� ���� �������� ������ �����/����
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // ������������ ������� ����� � ����

        // ��������� �������� ������ �� ��� X
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // ������� ������ (MALE2) �� ��� Y
        MALE2.Rotate(Vector3.up * mouseX);

        // �������� ���� ��� �������� ������
        float moveX = Input.GetAxis("Horizontal"); // A/D (Left/Right)
        float moveZ = Input.GetAxis("Vertical");   // W/S (Forward/Back)

        // ������� ������ �� ���� X � Z
        Vector3 move = (MALE2.right * moveX + MALE2.forward * moveZ) * movementSpeed * Time.deltaTime;
        MALE2.position += move;
    }
}



