using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public string itemName;
    public Sprite itemSprite;
    public GameObject weaponPrefab; // Префаб оружия (например, нож или шприц)
    private bool isMouseOver = false;
    private Player player;
    public Inventory inventory; // Добавлена ссылка на инвентарь

    private BossController boss; // Посилання на босса

    void Start()
    {
        player = FindObjectOfType<Player>(); // Находим объект игрока

        if (inventory == null) // Если инвентарь не присвоен в инспекторе
        {
            inventory = Inventory.inventory; // Получаем ссылку на глобальный инвентарь
        }
        boss = FindObjectOfType<BossController>(); // Знаходимо босса у сцені
    }

    void OnMouseOver()
    {
        isMouseOver = true; // Мышь наведена на объект
    }

    void OnMouseExit()
    {
        isMouseOver = false; // Мышь покинула объект
    }

    void Update()
    {
        if (isMouseOver && Input.GetMouseButtonDown(0)) // Проверяем, если мышь над объектом и нажата левая кнопка
        {
            Pickup();
        }
    }

    void Pickup()
    {
        if (string.IsNullOrEmpty(itemName) || itemSprite == null)
        {
            Debug.LogError("Имя или спрайт предмета не заданы!");
            return;
        }

        // Додаємо предмет в інвентар
        inventory.AddItem(itemName, itemSprite);

        // Якщо це ніж або шприц — екіпіруємо
        if (weaponPrefab != null && player != null)
        {
            player.EquipWeapon(weaponPrefab);
        }

        // Якщо це ключ — наносимо шкоду босу
        if (itemName == "Key" && boss != null)
        {
            boss.TakeDamageFromKey();
        }
        Destroy(gameObject);  // Видаляємо ключ після підбору
    }
    
}
