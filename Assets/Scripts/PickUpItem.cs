using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public string itemName;  // ��� �������� (��������, Knife, Key, Syringe)
    public Sprite itemSprite; // ������ �������� ��� ����������� � ���������
    public Inventory inventory; // ������ �� ��������� ��� ���������� ��������

    private bool isMouseOver = false; // ���� ��� ������������ ��������� ���� �� �������
    private BossController boss; // ��������� �� �����

    void Start()
    {
        inventory = Inventory.inventory; // �������� ������ �� ���������
        boss = FindObjectOfType<BossController>(); // ��������� ����� � ����
    }

    void OnMouseOver()
    {
        // ����� ���� �������� �� ������
        isMouseOver = true;
    }

    void OnMouseExit()
    {
        // ����� ���� �������� ������
        isMouseOver = false;
    }

    void Update()
    {
        // ���������, ���� ���� ��� �������� � ������ ����� ������ ����
        if (isMouseOver && Input.GetMouseButtonDown(0)) // 0 - ����� ������ ����
        {
            Pickup(); // ��������������� � ��������
        }
    }

    void Pickup()
    {
        if (string.IsNullOrEmpty(itemName) || itemSprite == null)
        {
            Debug.LogError("��� ��� ������ �������� �� ������!");
            return;
        }

        inventory.AddItem(itemName, itemSprite); // ��������� ������� � ���������


        if (itemName == "Key" && boss != null)
        {
            boss.TakeDamageFromKey(); // ��������� ������� � �����
        }

        Destroy(gameObject); // ��������� ���� ���� ������
    }

}
