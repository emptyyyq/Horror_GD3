using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Inventory inventory; // Ссылка на инвентарь
    public Image[] inventorySlots; // Массив для слотов инвентаря
    private string[] itemNames;    // Массив для хранения имен предметов в слоте
    private Sprite[] itemSprites;  // Массив для хранения изображений предметов в слоте
    public InventoryElement[] _calls;
    public BlockData[] _blockdatas;

    private void Start()
    {
        itemNames = new string[inventorySlots.Length];
        itemSprites = new Sprite[inventorySlots.Length];

        _calls[0].Image.sprite = _blockdatas[0].BlockSprite;
        _calls[0].Text.text = _blockdatas[0].count.ToString();

        _calls[1].Image.sprite = _blockdatas[1].BlockSprite;
        _calls[1].Text.text = _blockdatas[1].count.ToString();

        _calls[2].Image.sprite = _blockdatas[2].BlockSprite;
        _calls[2].Text.text = _blockdatas[2].count.ToString();

        _calls[3].Image.sprite = _blockdatas[3].BlockSprite;
        _calls[3].Text.text = _blockdatas[3].count.ToString();

        //_calls[1].sprite = _blockdatas[1].BlockSprite;


    }
    // Метод для добавления предмета в инвентарь
    public void AddItem(string itemName, Sprite itemSprite)
    {
        // Ищем первый пустой слот для добавления предмета
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].sprite == null) // Если слот пуст
            {
                inventorySlots[i].sprite = itemSprite; // Помещаем спрайт в слот
                itemNames[i] = itemName; // Сохраняем имя предмета
                break; // Прерываем цикл, так как предмет добавлен
            }
        }
    }

    // Для теста можно создать метод для очистки инвентаря
    public void ClearInventory()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i].sprite = null;
            itemNames[i] = null;
        }
    }
}
