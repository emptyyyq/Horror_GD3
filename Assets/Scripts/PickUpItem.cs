using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public string itemName;  // Имя предмета (например, Knife, Key, Syringe)
    public Sprite itemSprite; // Спрайт предмета для отображения в инвентаре
    public Inventory inventory; // Ссылка на инвентарь для добавления предмета

    private bool isMouseOver = false; // Флаг для отслеживания наведения мыши на предмет

    void Start()
    {
        inventory = Inventory.inventory; // Получаем ссылку на инвентарь
    }

    void OnMouseOver()
    {
        // Когда мышь наведена на объект
        isMouseOver = true;
    }

    void OnMouseExit()
    {
        // Когда мышь покидает объект
        isMouseOver = false;
    }

    void Update()
    {
        // Проверяем, если мышь над объектом и нажата левая кнопка мыши
        if (isMouseOver && Input.GetMouseButtonDown(0)) // 0 - левая кнопка мыши
        {
            Pickup(); // Взаимодействуем с объектом
        }
    }

    void Pickup()
    {
        // Добавляем предмет в инвентарь
        inventory.AddItem(itemName, itemSprite);
    }
}
