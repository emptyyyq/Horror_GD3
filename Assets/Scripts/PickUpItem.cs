using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public string itemName;
    public Sprite itemSprite;
    public GameObject weaponPrefab; // ������ ������ (��������, ��� ��� �����)
    private bool isMouseOver = false;
    private Player player;
    public Inventory inventory; // ��������� ������ �� ���������

    private BossController boss; // ��������� �� �����

    void Start()
    {
        player = FindObjectOfType<Player>(); // ������� ������ ������

        if (inventory == null) // ���� ��������� �� �������� � ����������
        {
            inventory = Inventory.inventory; // �������� ������ �� ���������� ���������
        }
        boss = FindObjectOfType<BossController>(); // ��������� ����� � ����
    }

    void OnMouseOver()
    {
        isMouseOver = true; // ���� �������� �� ������
    }

    void OnMouseExit()
    {
        isMouseOver = false; // ���� �������� ������
    }

    void Update()
    {
        if (isMouseOver && Input.GetMouseButtonDown(0)) // ���������, ���� ���� ��� �������� � ������ ����� ������
        {
            Pickup();
        }
    }

    void Pickup()
    {
        if (string.IsNullOrEmpty(itemName) || itemSprite == null)
        {
            Debug.LogError("��� ��� ������ �������� �� ������!");
            return;
        }

        // ������ ������� � ��������
        inventory.AddItem(itemName, itemSprite);

        // ���� �� �� ��� ����� � �������
        if (weaponPrefab != null && player != null)
        {
            player.EquipWeapon(weaponPrefab);
        }

        // ���� �� ���� � �������� ����� ����
        if (itemName == "Key" && boss != null)
        {
            boss.TakeDamageFromKey();
        }
        Destroy(gameObject);  // ��������� ���� ���� ������
    }
    
}
