using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpItem : MonoBehaviour
{
    public string itemName; // The name of the item
    public Sprite itemSprite; // The sprite representing the item
    public GameObject weaponPrefab; // The weapon prefab (e.g., knife or syringe)
    private Player player; // Reference to the player
    public Inventory inventory; // Reference to the inventory
    bool isCursorOver = false; // Flag to check if the cursor is over the object
    private BossController boss; // Reference to the boss
    public Image cursorImage; // The Image component for the custom cursor (assigned in the inspector)

    void Start()
    {
        // Find the player object in the scene
        player = FindObjectOfType<Player>();

        // If the inventory is not assigned in the Inspector, use the global inventory
        if (inventory == null)
        {
            inventory = Inventory.inventory; // Get the global inventory reference
        }

        // Find the boss object in the scene
        boss = FindObjectOfType<BossController>();

        // Ensure the custom cursor is enabled (check if the cursorImage is assigned)
        if (cursorImage != null)
        {
            cursorImage.enabled = true; // Show the custom cursor
        }
        else
        {
            Debug.LogError("Cursor Image not assigned!"); // Log an error if the cursor image is not assigned in the Inspector
        }
    }

    void Update()
    {
        // Check if the custom cursor is over the object and if the left mouse button is pressed
        if (IsCursorOverObject() && Input.GetKeyDown(KeyCode.E))
        {
            Pickup(); // Call the Pickup method if conditions are met
        }
        // Ensure the custom cursor is enabled (check if the cursorImage is assigned)
        if (cursorImage != null)
        {
            cursorImage.enabled = true; // Show the custom cursor
        }
        else
        {
            Debug.LogError("Cursor Image not assigned!"); // Log an error if the cursor image is not assigned in the Inspector
        }
    }

    // This method checks if the cursor is currently over the object
    bool IsCursorOverObject()
    {
        // Get the RectTransform of the current object
        RectTransform rectTransform = GetComponent<RectTransform>();

        // If the RectTransform exists, check if the cursor is within the bounds of the object
        if (rectTransform != null)
        {
            Vector2 localPoint;
            // Convert the mouse position from screen space to the object's local space
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, null, out localPoint);
            // Return true if the cursor is within the object's bounds
            return rectTransform.rect.Contains(localPoint);
        }

        return false; // Return false if the cursor is not over the object
    }

    // This method handles picking up the item
    void Pickup()
    {
        // Ensure that the item has a name and sprite
        if (string.IsNullOrEmpty(itemName) || itemSprite == null)
        {
            Debug.LogError("Item name or sprite is not set!"); // Log an error if item details are missing
            return;
        }

        // Add the item to the inventory
        inventory.AddItem(itemName, itemSprite);

        // If the item is a weapon (e.g., knife or syringe), equip it
        if (weaponPrefab != null && player != null)
        {
            player.EquipWeapon(weaponPrefab); // Equip the weapon
        }

        // If the item is a key, deal damage to the boss
        if (itemName == "Key" && boss != null)
        {
            boss.TakeDamageFromKey(); // Deal damage to the boss using the key
        }

        // Destroy the object after picking it up (e.g., the key or item)
        Destroy(gameObject);
    }

}
