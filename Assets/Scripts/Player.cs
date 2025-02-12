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
    [SerializeField] private Transform leftHand; // ������ ��������� �� ����� ����

    private Rigidbody rb;
    private bool isGrounded;

    private GameObject currentWeapon; // ������� ������ � �����

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // �������� �� ��, ��������� �� �������� �� �����
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);

        // ���������� ���������
        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        if (moveInput.magnitude > 0)
        {
            Vector3 moveDirection = transform.TransformDirection(moveInput);
            rb.MovePosition(transform.position + moveDirection * moveSpeed * Time.deltaTime);
            animator.SetBool("IsWalking", true);
        }
        else { animator.SetBool("IsWalking", false); }

        // ������ ������
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    // ����� ��� ���������� ������ � ����� ����
    public void EquipWeapon(GameObject weapon)
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon); // ������� ������� ������, ���� ��� ����
        }

        currentWeapon = Instantiate(weapon, ItemHolder); // ������� ������ � ����� ����
        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.localRotation = Quaternion.identity;
    }
}
