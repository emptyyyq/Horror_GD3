using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory inventory; // ����������� ������ �� ���������
    public Image[] inventorySlots; // ������ ������ ��������� (UI)
    private string[] itemNames; // ������ ��� �������� ���� ��������� � �����
    private Sprite[] itemSprites; // ������ ��� �������� �������� ��������� � �����
        void Awake()
        {
        itemNames = new string[inventorySlots.Length];
        itemSprites = new Sprite[inventorySlots.Length];
        if (inventory == null)
            {
                inventory = this;
                Debug.Log("��������� ���������������: " + gameObject.name);
                DontDestroyOnLoad(gameObject);
            }
            else if (inventory != this)
            {
                Debug.LogError("��������� ��� ���������������! - " + gameObject.name);
                Destroy(gameObject);
            }
        }

    // ����� ��� ���������� �������� � ���������
    public void AddItem(string itemName, Sprite itemSprite)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (itemNames[i] == null || itemNames[i] == "")
            {
                inventorySlots[i].sprite = itemSprite; // �������� ������ � ����
                itemNames[i] = itemName; // ��������� ��� ��������
                return; // ��������� ����, ��� ��� ������� ��������
            }
        }
    }
}
