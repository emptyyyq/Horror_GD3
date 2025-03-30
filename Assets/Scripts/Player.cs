using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Image;

public class Player : Sounds
{
     [SerializeField] private Transform ItemHolder;
    [SerializeField] private Animator animator;
    [SerializeField] private float moveSpeed = 100f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float groundCheckDistance = 0.3f;
    [SerializeField] private Transform leftHand; // ������ ��������� �� ����� ����
    [SerializeField] private UIHealthBarHelper healthBarHelper;
    [SerializeField] private float stepInterval = 0.5f; // �������� ����� ������

    private Rigidbody rb;
    private bool isGrounded;

    private GameObject currentWeapon; // ������� ������ � �����

    //
    private Vector2 velocity;

    public Transform playerCamera;
 

    private Rigidbody rigidb;
    public AudioSource myFx;
    public AudioClip ClipFx;

    public Text txt;
    public int Energy;
    public GameObject Light;
    public bool onLight;
    private float scet;
    public GameObject Button_up;
    private RaycastHit hit;
    public AudioClip Upfx;
    private float stepTimer = 0f;

    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();
    private bool wasInAir = false; // ����, ��� �� ����� � �������



    public void PlaySound(AudioClip clip)
    {
        myFx.PlayOneShot(clip);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // ��������, ��������� �� �������� �� �����
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);

        // ���������� ���������
        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        if (moveInput.magnitude > 0)
        {
            Vector3 moveDirection = transform.TransformDirection(moveInput);
            rb.MovePosition(transform.position + moveDirection * moveSpeed * Time.deltaTime);
            animator.SetBool("IsWalking", true);

            // ��������������� ����� �����
            if (isGrounded && stepTimer <= 0f)
            {
                myFx.PlayOneShot(Upfx); // ����������� ���� ����
                PlaySound(Upfx);
                stepTimer = stepInterval; // ���������� ������
                  
            }
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }

        // ������ �����
        if (stepTimer > 0f)
        {
            stepTimer -= Time.deltaTime;
        }
        if (isGrounded && stepTimer <= 0f)
        {
            Debug.Log("���!"); // ��������, ���������� �� ���
            myFx.PlayOneShot(Upfx); // ����������� ���� ����
            stepTimer = stepInterval; // ���������� ������
        }

        // ������ ������ + ���� ������
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            PlaySound(ClipFx); // ��������������� ����� ������
        }
        // �������� �� ��, ��������� �� �������� �� �����
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);

        // ���������� ���������
        Vector3 MoveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        if (moveInput.magnitude > 0)
        {
            Vector3 moveDirection = transform.TransformDirection(MoveInput);
            rb.MovePosition(transform.position + moveDirection * moveSpeed * Time.deltaTime);
            animator.SetBool("IsWalking", true);
        }
        else { animator.SetBool("IsWalking", false); }

        // ������ ������
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            PlaySound(ClipFx);
        }

        txt.text = Energy + "%";

        if (onLight)
        {
            scet += Time.deltaTime;
            if (scet >= 2)
            {
                Energy -= 1;
                scet = 0;
            }
        }

        Energy = Mathf.Clamp(Energy, 0, 50);

        if (Energy <= 0)
        {
            onLight = false;
            Light.SetActive(false);
        }
        // ��������, ��������� �� �������� �� �����
        bool previouslyGrounded = isGrounded;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);

        // ���� ����� ��� � ������� � ������ ��� ����������� � ������������� ����
        if (!previouslyGrounded && isGrounded)
        {
            myFx.Stop(); // ��������� ����� ��� �����������
        }

        // ���������� ���������
        Vector3 MOveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        if (MoveInput.magnitude > 0)
        {
            Vector3 moveDirection = transform.TransformDirection(MOveInput);
            rb.MovePosition(transform.position + moveDirection * moveSpeed * Time.deltaTime);
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }

        // ������ ������ + ���� ������
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            PlaySound(ClipFx); // ��������������� ����� ������
        }

        txt.text = Energy + "%";

        if (onLight)
        {
            scet += Time.deltaTime;
            if (scet >= 2)
            {
                Energy -= 1;
                scet = 0;
            }
        }

        Energy = Mathf.Clamp(Energy, 0, 100);

        if (Energy <= 0)
        {
            onLight = false;
            Light.SetActive(false);
        }


    }
    public UIHealthBarHelper HealthBarHelper => healthBarHelper;

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

  
    void FixedUpdate()
    {
        var forw = transform.TransformDirection(Vector3.forward);
            Debug.DrawRay(transform.position, forw * 4.5f, Color.red, 2f);
        if (Physics.Raycast(transform.position, forw, out hit, 4.5f))
        {
            Debug.Log(hit.collider.ToString());
            if (hit.collider.tag == "Battery")
            {
                Button_up.SetActive(true);
            }
        }
        else
        {
            Button_up.SetActive(false);
        }
        
    }
    public void FlashLightButton()
    {
        Debug.Log("������ �������? " + onLight + ", Energy: " + Energy);
        myFx.PlayOneShot(ClipFx);

        if (!onLight && Energy > 0)
        {
            Light.SetActive(true);
            onLight = true;
        }
        else
        {
            Light.SetActive(false);
            onLight = false;
        }
    }
    public void ButtonUse()
    {
        if(hit.collider.tag == "Battery")
        {
            Energy += 25;
            myFx.PlayOneShot(Upfx);
            Destroy(hit.collider.gameObject);
        }
    }
    
}


