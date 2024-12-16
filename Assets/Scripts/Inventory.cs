using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory inventory; // Статическая ссылка на инвентарь
    public Image[] inventorySlots; // Массив слотов инвентаря (UI)
    private string[] itemNames; // Массив для хранения имен предметов в слоте
    private Sprite[] itemSprites; // Массив для хранения спрайтов предметов в слоте
       void Awake()
       {
        void Awake()
        {
            if (inventory == null)
            {
                inventory = this;
                Debug.Log("Инвентарь инициализирован: " + gameObject.name);
                DontDestroyOnLoad(gameObject);
            }
            else if (inventory != this)
            {
                Debug.LogError("Инвентарь уже инициализирован! - " + gameObject.name);
                Destroy(gameObject);
            }
        }
    }
    

    private void Start()
    {
        itemNames = new string[inventorySlots.Length];
        itemSprites = new Sprite[inventorySlots.Length];
        if (inventory == null)
        {
            inventory = this; // Устанавливаем текущий объект как инвентарь
            DontDestroyOnLoad(gameObject); // Инвентарь не будет уничтожен при смене сцены
        }
        else if (inventory != this)
        {
            // Если инвентарь уже существует, уничтожаем текущий объект
            Debug.LogError("Инвентарь уже инициализирован!");
            Destroy(gameObject); // Удаляем второй объект инвентаря
        }
    }

    // Метод для добавления предмета в инвентарь
    public void AddItem(string itemName, Sprite itemSprite)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (itemNames[i] == null || itemNames[i] == "")
            {
                inventorySlots[i].sprite = itemSprite; // Помещаем спрайт в слот
                itemNames[i] = itemName; // Сохраняем имя предмета
                return; // Прерываем цикл, так как предмет добавлен
            }
        }
    }
}
