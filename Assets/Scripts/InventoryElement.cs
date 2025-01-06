using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryElement : MonoBehaviour
{
    public BlockType BlockType;  // Тип блока (например, ключ или нож)
    public Sprite BlockSprite;   // Спрайт блока (изображение предмета)
    public int count;            // Количество блоков (предметов) в инвентаре

    public Image Image;          // UI элемент изображения
    public Text Text;            // UI элемент текста (количество предметов)
}
