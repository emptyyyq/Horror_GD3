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
    [SerializeField] private Transform leftHand; // Теперь ссылаемся на левую руку
    [SerializeField] private UIHealthBarHelper healthBarHelper;

    private Rigidbody rb;
    private bool isGrounded;

    private GameObject currentWeapon; // Текущее оружие в руках

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

    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();

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
        Debug.Log("Фонарь включен? " + onLight + ", Energy: " + Energy);
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


