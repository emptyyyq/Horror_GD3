using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryElement : MonoBehaviour
{
    public BlockType BlockType;  // ��� ����� (��������, ���� ��� ���)
    public Sprite BlockSprite;   // ������ ����� (����������� ��������)
    public int count;            // ���������� ������ (���������) � ���������

    public Image Image;          // UI ������� �����������
    public Text Text;            // UI ������� ������ (���������� ���������)
}
