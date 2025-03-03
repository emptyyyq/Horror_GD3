using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public Button myButton; // ���� ������
    public Text buttonText; // ����� �� ������
    private bool isClicked = false; // �������� ����������

    private string originalText = "³���, ����� �����!\n �����, �� ������ � �� ��� � ���� � � �� ������ �������,\n ����� ����������� �� �����" +
        "\n ��� ����������� � ���� �� �� �'��������� � ��,\n �� ���� �������� ���� � � ������� ��������.\n � ������ ������� �� ����� ��������� � ����� ����� ����������." +
        "\n ����� ��������� ���� 6 � � ������� �� ��� ���� ��� ��,\n ������� ����� �� ������� ������� ����� �� ������������ �� ����.\n ��� �������� ���� ������� ��� ��, ��� �����" +
        "\n ϳ��� ���� �� �� ����������� ���� � ����� ������ ���� ���� �������� ���� ����,\n ��� ���� ���� �'�������� ��� ���� ����,\n �� �� ���� ��� ���� � ���� �� ������ ���� " +
        "����� �� �������,\n �� �� ���� ������ ����. ���� ��� - ��������� �����"; // ���������� �����

    private void Start()
    {
        // ������������ ���������� �����
        buttonText.text = originalText;

        // ������ ������ ���� ��� ������
        myButton.onClick.AddListener(ToggleText);
    }

    private void ToggleText()
    {
        if (isClicked)
        {
            buttonText.text = originalText; // ��������� ������ �����
        }


        isClicked = !isClicked; // ������� ����
    }
}
