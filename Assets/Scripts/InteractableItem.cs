using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableItem : MonoBehaviour
{
    public string itemName;
    public Sprite itemSprite;
    public GameObject weaponPrefab;

    private Inventory inventory;
    private Player player;
    private BossController boss;

    void Start()
    {
        player = FindObjectOfType<Player>();

        if (inventory == null)
        {
            inventory = Inventory.inventory;
        }

        boss = FindObjectOfType<BossController>();
    }

    public void Pickup()
    {
        if (string.IsNullOrEmpty(itemName) || itemSprite == null)
        {
            Debug.LogError("Item name or sprite is not set!");
            return;
        }

        inventory.AddItem(itemName, itemSprite);

        if (weaponPrefab != null && player != null)
        {
            player.EquipWeapon(weaponPrefab);
        }

        if (itemName == "Key" && boss != null)
        {
            boss.TakeDamageFromKey();
        }

        Destroy(gameObject);
    }
}