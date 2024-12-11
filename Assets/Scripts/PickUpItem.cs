using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public string itemName;  // ��� �������� (��������, Knife, Key, Syringe)
    public Sprite itemSprite; // ������ �������� ��� ����������� � ���������
    public Inventory inventory; // ������ �� ��������� ��� ���������� ��������

    private bool isMouseOver = false; // ���� ��� ������������ ��������� ���� �� �������

    void Start()
    {
        inventory = Inventory.inventory; // �������� ������ �� ���������
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
        // ��������� ������� � ���������
        inventory.AddItem(itemName, itemSprite);
    }
}
